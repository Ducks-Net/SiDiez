using DucksNet.Domain.Model;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class CagesOverview : ComponentBase
{
    [Inject]
    public ICageDataService CageDataService { get; set; }
    public List<Cage> Cages { get; set; } = default!;
    protected async override Task OnInitializedAsync()
    {
        Cages = (await CageDataService.GetAllCages()).ToList();
    }
}
