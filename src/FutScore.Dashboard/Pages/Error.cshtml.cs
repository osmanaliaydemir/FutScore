using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace FutScore.Dashboard.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
public class ErrorModel : PageModel
{
    private readonly ILogger<ErrorModel> _logger;

    public string ErrorCode { get; set; } = "404";
    public string ErrorMessage { get; set; } = "Page Not Found";
    public string ErrorDescription { get; set; } = "It looks like you found a glitch in the matrix...";

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet(int? statusCode = null)
    {
        if (statusCode.HasValue)
        {
            ErrorCode = statusCode.ToString();
            switch (statusCode)
            {
                case 404:
                    ErrorMessage = "Page Not Found";
                    ErrorDescription = "It looks like you found a glitch in the matrix...";
                    break;
                case 403:
                    ErrorMessage = "Access Denied";
                    ErrorDescription = "Sorry, you don't have permission to access this page.";
                    break;
                case 500:
                    ErrorMessage = "Server Error";
                    ErrorDescription = "Oops! Something went wrong on our end.";
                    break;
                default:
                    ErrorMessage = "An Error Occurred";
                    ErrorDescription = "We're working on fixing this issue.";
                    break;
            }
        }

        _logger.LogError($"Error {ErrorCode}: {ErrorMessage}");
    }
}
