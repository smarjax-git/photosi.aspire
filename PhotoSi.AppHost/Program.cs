var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.PhotoSi_Prodotti>("photosi-catalog");

builder.AddProject<Projects.PhotoSi_PickupPoint>("photosi-pickuppoint");

builder.AddProject<Projects.PhotoSi_Utenti>("photosi-utenti");

builder.AddProject<Projects.PhotoSi_Ordini>("photosi-ordini");

builder.AddProject<Projects.PhotoSi_API>("photosi-api");

builder.Build().Run();
