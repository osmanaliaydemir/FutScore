using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Stadiums
{
    public class IndexModel : PageModel
    {
        private readonly IStadiumService _stadiumService;

        public IndexModel(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        public IEnumerable<StadiumDto> Stadiums { get; set; } = new List<StadiumDto>();

        public async Task OnGetAsync()
        {
            Stadiums = await _stadiumService.GetAllStadiumsAsync();
        }
    }
} 