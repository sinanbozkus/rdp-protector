using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WindowsFirewallHelper;

namespace RdpProtector.Controllers
{
    [Authorize]
    public class RuleController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            var rules = FirewallManager.Instance.Rules.ToList();

            var remotes = rules.Where(x => x.Name.Contains("Remote")).ToList();

            return View(rules);
        }
    }
}
