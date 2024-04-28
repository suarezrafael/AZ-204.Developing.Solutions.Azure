using Microsoft.ApplicationInsights.AspNetCore.TelemetryInitializers;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;

namespace sqlapp.Services
{
    public class TelemetryService : TelemetryInitializerBase

    {
        public TelemetryService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        protected override void OnInitializeTelemetry(HttpContext platformContext, RequestTelemetry requestTelemetry, ITelemetry telemetry)
        {
            telemetry.Context.User.AuthenticatedUserId=
                platformContext.User?.Identity.Name ?? string.Empty;
        }
    }
}
