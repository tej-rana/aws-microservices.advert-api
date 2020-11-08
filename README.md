# About aws-microservices
This is a simple project that uses CQRS to demonstrate how microservices work in AWS. This also uses HttpClientFactory Pattern with CircuitBreaker and Retry Policies. Click here to learn more on HttpClientFactory https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1

# Stack
The tech stack is Dynamo DB, API Gateway, Cloud Map, ECS + Docker, CloudWatch and S3. The API is written in .NET Core with CQRS and DDD pattern.

# NuGet
Nuget packages for the DTO layer is found in https://www.nuget.org/packages/TinySnippt.AdvertApi.Models/
