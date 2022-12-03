using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DucksNet.WebUI;
using DucksNet.WebUI.Pages.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>
    (
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    );
builder.Services.AddHttpClient<IMedicalRecordDataService, MedicalRecordDataService>
    (
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    );
builder.Services.AddHttpClient<ICageDataService, CageDataService>
    (
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    );
builder.Services.AddHttpClient<IPetDataService, PetDataService>
    (
        client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    );
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
