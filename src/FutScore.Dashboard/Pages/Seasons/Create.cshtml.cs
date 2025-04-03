using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Season;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutScore.Dashboard.Pages.Seasons
{
    public class CreateModel : PageModel
    {
        private readonly ISeasonService _seasonService;
        private readonly ILeagueService _leagueService;

        public CreateModel(ISeasonService seasonService, ILeagueService leagueService)
        {
            _seasonService = seasonService;
            _leagueService = leagueService;
        }

        [BindProperty]
        public CreateSeasonDto Season { get; set; } = new();

        public SelectList Leagues { get; set; }

        public async Task OnGetAsync()
        {
            var leagues = await _leagueService.GetAllLeaguesAsync();
            Leagues = new SelectList(leagues, nameof(LeagueDto.Id), nameof(LeagueDto.Name));
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var getLeagues = await _leagueService.GetAllLeaguesAsync();
                Leagues = new SelectList(getLeagues, nameof(LeagueDto.Id), nameof(LeagueDto.Name));
                return Page();
            }

            var result = await _seasonService.CreateSeasonAsync(Season);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Sezon başarıyla oluşturuldu.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            var leagues = await _leagueService.GetAllLeaguesAsync();
            Leagues = new SelectList(leagues, nameof(LeagueDto.Id), nameof(LeagueDto.Name));
            return Page();
        }
    }
} 