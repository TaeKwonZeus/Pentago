using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Pentago.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [IgnoreAntiforgeryToken]
    public class ErrorModel : PageModel
    {
        public string RequestId => Activity.Current?.Id ?? HttpContext.TraceIdentifier;

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}