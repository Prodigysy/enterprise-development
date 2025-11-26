var builder = DistributedApplication.CreateBuilder(args);

var realEstateAgencyDb = builder
    .AddPostgres("real-estate-agency-db")
    .AddDatabase("real-estate-agency");

builder.AddProject<Projects.RealEstateAgency_Api_Host>("realestateagency-api-host")
    .WithReference(realEstateAgencyDb, "DatabaseConnection")
    .WaitFor(realEstateAgencyDb);

builder.Build().Run();
