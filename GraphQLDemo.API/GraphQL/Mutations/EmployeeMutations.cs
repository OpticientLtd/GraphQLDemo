using GraphQLDemo.API.GraphQL.Common;
using GraphQLDemo.API.GraphQL.Extensions;
using GraphQLDemo.API.GraphQL.Inputs;
using GraphQLDemo.API.GraphQL.Payloads;
using GraphQLDemo.API.GraphQL.Subscriptions;
using GraphQLDemo.DataRepository;
using GraphQLDemo.DataRepository.Data.Models;
using HotChocolate;
using HotChocolate.Subscriptions;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace GraphQLDemo.API.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class EmployeeMutations
    {
        [UseApplicationDbContext]
        public async Task<EmployeePayload> AddEmployeeAsync(AddEmployeeInput input,
            [Service] IUnitOfWork unitOfWork,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            if (!await unitOfWork.Departments.AnyAsync(predicate: x => x.Id == input.DepartmentId, cancellationToken: cancellationToken))
                return new EmployeePayload(new UserError($"Department doesn't exist with Id = {input.DepartmentId}", "INVALID_DEPARTMENTID"));
            if (input.ManagerId.HasValue && input.ManagerId.Value != null)
                if (!await unitOfWork.Employees.AnyAsync(predicate: x => x.Id == input.ManagerId.Value.Value, cancellationToken: cancellationToken))
                    return new EmployeePayload(new UserError($"Employee doesn't exist with Id = {input.ManagerId.Value.Value}", "INVALID_MANAGERID"));
            var employee = new Employee()
            {
                Name = input.Name,
                DepartmentId = input.DepartmentId,
                ManagerId = input.ManagerId.Value,
                Salary = input.Salary,
                Bonus = input.Bonus.Value,
            };
            await unitOfWork.Employees.AddAsync(employee, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            await eventSender.SendAsync(nameof(EmployeeSubscriptions.OnEmployeeAddedAsync), employee.Id);
            return new EmployeePayload(employee.Id, employee);
        }

        [UseApplicationDbContext]
        public async Task<EmployeePayload> EditEmployeeAsync(EditEmployeeInput input,
            [Service] IUnitOfWork unitOfWork,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var employee = await unitOfWork.Employees.GetAsync(input.Id, cancellationToken);
            if (employee == null)
                return new EmployeePayload(new UserError($"Employee doesn't exist with Id = {input.Id}", "INVALID_ID"));

            var errors = new List<UserError>();
            if (input.Name.HasValue)
            {
                if (string.IsNullOrEmpty(input.Name.Value))
                    errors.Add(new UserError("Employee Name must be provided", "MISSING_EMPLOYEE_NAME"));
                else
                    employee.Name = input.Name.Value;
            }
            if (input.DepartmentId.HasValue)
            {
                if (input.DepartmentId.Value != null)
                {
                    if (!await unitOfWork.Departments.AnyAsync(predicate: x => x.Id == input.DepartmentId.Value.Value, cancellationToken: cancellationToken))
                        errors.Add(new UserError($"Department doesn't exist with DepartmentId = {input.DepartmentId.Value.Value}", "INVALID_DEPARTMENTID"));
                    else
                        employee.DepartmentId = input.DepartmentId.Value.Value;
                }
                else
                    errors.Add(new UserError("Employee DepartmentId must be provided", "MISSING_EMPLOYEE_DEPARTMENTID"));
            }
            if (input.ManagerId.HasValue)
            {
                if (input.ManagerId.Value != null)
                {
                    if (!await unitOfWork.Employees.AnyAsync(predicate: x => x.Id == input.ManagerId.Value.Value, cancellationToken: cancellationToken))
                        errors.Add(new UserError($"Employee doesn't exist with EmployeeId = {input.ManagerId.Value.Value}", "INVALID_MANAGERID"));
                    else
                        employee.ManagerId = input.ManagerId.Value.Value;
                }
            }
            if (input.Salary.HasValue)
            {
                if (!input.Salary.Value.HasValue)
                    errors.Add(new UserError("Employee Salary must be provided", "MISSING_EMPLOYEE_SALARY"));
                else
                    employee.Salary = input.Salary.Value.Value;
            }
            if (input.Bonus.HasValue)
                employee.Bonus = input.Bonus.Value.Value;
            if (errors.Count > 0)
                return new EmployeePayload(errors);

            await unitOfWork.CompleteAsync(cancellationToken);

            await eventSender.SendAsync(nameof(EmployeeSubscriptions.OnEmployeeEditedAsync), employee.Id);
            return new EmployeePayload(employee.Id, employee);
        }
        [UseApplicationDbContext]
        public async Task<EmployeePayload> DeleteEmployeeAsync(int id,
            [Service] IUnitOfWork unitOfWork,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            EmployeePayload payload = null;
            var employee = await unitOfWork.Employees.GetAsync(id, cancellationToken);
            if (employee == null)
                return new EmployeePayload(new UserError($"Employee doesn't exist with Id = {id}", "INVALID_ID"));

            try
            {
                unitOfWork.Employees.Remove(employee);
                await unitOfWork.CompleteAsync(cancellationToken);
                payload = new EmployeePayload(employee.Id, null);
                await eventSender.SendAsync(nameof(EmployeeSubscriptions.OnEmployeeDeletedAsync), payload);
            }
            catch (Exception)
            {
                payload = new EmployeePayload(new UserError("Employee can not be deleted", "CAN NOT DELETE"));
            }

            return payload;
        }
    }
}
