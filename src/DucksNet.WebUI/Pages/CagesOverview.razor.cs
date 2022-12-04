using DucksNet.Domain.Model;
using DucksNet.WebUI.Models;
using DucksNet.WebUI.Pages.Services;
using Microsoft.AspNetCore.Components;

namespace DucksNet.WebUI.Pages;

public partial class CagesOverview : ComponentBase
{
    [Inject]
    public ICageDataService CageDataService { get; set; } = default!;
    public List<Cage> Cages { get; set; } = default!;
    protected async Task ReloadAllCages()
    {
        Cages = (await CageDataService.GetAllCages()).ToList();
        if(Cages.Count == 0)
        {
            Cages = default!;
        }
    }

    protected async Task CreateCage(CageCreateModel cageCreateModel)
    {
        await CageDataService.CreateCage(cageCreateModel);
    }

    protected async Task DeleteCage(string cageId)
    {
        await CageDataService.DeleteCage(cageId);
    }
}
