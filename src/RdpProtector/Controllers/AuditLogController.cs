using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;

namespace RdpProtector.Controllers
{
    [Authorize]
    public class AuditLogController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            var eventLog = new EventLog("Security");
            var entries = eventLog.Entries
                    .Cast<EventLogEntry>()
                    .Where(x => x.CategoryNumber == 12544)
                    .OrderByDescending(x => x.Index)
                    .Take(100)
                    .ToList();

            return View(entries);
        }
    }
}
