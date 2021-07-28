# GraphQLDemo

Project showing Demo of GraphQL APIs.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
.Net Core 5.0
Microsoft SQL LocalDB
```

## Built With

* .Net Core 5.0
* ASP.Net Core Web API using C#
* HotChocolate GraphQL
* Entity Framework Core with Repository Pattern (Opticient.EFCore.Repository)
* Microsoft SQL Server (localdb)
* GraphQL Server UI Voyager

## Authors

* **Pankaj Kansodariya** - [Opticient Ltd.](http://opticient.co.uk)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details

## Acknowledgments

* Please make necessary changes as per your requirements.

## Content Type

* Content Type is JSON

## Database Design

* **Department**
	* Id (PK - Identity)		
	* Name

* **Employee**
	* Id (PK - Identity)		
	* Name
	* DepartmentId -> ([Department].[Id])
    * ManagerId -> ([Employee].[Id])
	* Salary
	* Bonus

# End points
* GraphQL Endpoint
	https://localhost:5001/graphql

* GraphQL Voyager Endpoint
	https://localhost:5001/graphql-voyager

## Postman Script
  * PostmanCollection.json
    * Contains GraphQL APIs

** **Notes**
* Change appsettings.json for database connection string.
