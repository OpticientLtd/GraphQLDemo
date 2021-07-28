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
    public class DepartmentMutations
    {
        [UseApplicationDbContext]
        public async Task<DepartmentPayload> AddDepartmentAsync(AddDepartmentInput input,
            [Service] IUnitOfWork unitOfWork,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var department = new Department()
            {
                Name = input.Name,
            };
            await unitOfWork.Departments.AddAsync(department, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            await eventSender.SendAsync(nameof(DepartmentSubscriptions.OnDepartmentAddedAsync), department.Id);
            return new DepartmentPayload(department.Id, department);
        }

        [UseApplicationDbContext]
        public async Task<DepartmentPayload> EditDepartmentAsync(EditDepartmentInput input,
            [Service] IUnitOfWork unitOfWork,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            var department = await unitOfWork.Departments.GetAsync(input.Id, cancellationToken);
            if (department == null)
                return new DepartmentPayload(new UserError($"Department doesn't exist with Id = {input.Id}", "INVALID_ID"));

            var errors = new List<UserError>();
            if (input.Name.HasValue)
            {
                if (string.IsNullOrEmpty(input.Name.Value))
                    errors.Add(new UserError("Department Name must be provided", "MISSING_DEPARTMENT_NAME"));
                else
                    department.Name = input.Name.Value;
            }
            if (errors.Count > 0)
                return new DepartmentPayload(errors);

            await unitOfWork.CompleteAsync(cancellationToken);

            await eventSender.SendAsync(nameof(DepartmentSubscriptions.OnDepartmentEditedAsync), department.Id);
            return new DepartmentPayload(department.Id, department);
        }
        [UseApplicationDbContext]
        public async Task<DepartmentPayload> DeleteDepartmentAsync(int id,
            [Service] IUnitOfWork unitOfWork,
            [Service] ITopicEventSender eventSender,
            CancellationToken cancellationToken)
        {
            DepartmentPayload payload = null;
            var department = await unitOfWork.Departments.GetAsync(id, cancellationToken);
            if (department == null)
                return new DepartmentPayload(new UserError($"Department doesn't exist with Id = {id}", "INVALID_ID"));

            try
            {
                unitOfWork.Departments.Remove(department);
                await unitOfWork.CompleteAsync(cancellationToken);
                payload = new DepartmentPayload(department.Id, null);
                await eventSender.SendAsync(nameof(DepartmentSubscriptions.OnDepartmentDeletedAsync), payload);
            }
            catch (Exception)
            {
                payload = new DepartmentPayload(new UserError("Department can not be deleted", "CAN NOT DELETE"));
            }

            return payload;
        }
    }
}
