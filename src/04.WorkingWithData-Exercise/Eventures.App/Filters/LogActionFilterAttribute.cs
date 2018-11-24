using Eventures.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Eventures.App.Filters
{
    public class LogActionFilterAttribute : ActionFilterAttribute
    {
        private readonly EventuresDbContext db;
        private readonly ILogger logger;

        public LogActionFilterAttribute(EventuresDbContext db, ILogger<LogActionFilterAttribute> logger)
        {
            this.db = db;
            this.logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var @event = this.db.Events.FirstOrDefault();

            var message = $"[{DateTime.Now}] Administrator {context.HttpContext.User.Identity.Name} create event {@event.Name} ({@event.Start} / {@event.End})";

            this.logger.LogInformation(message);
        }
    }
}
