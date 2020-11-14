using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;

namespace Sample_50
{
    public class ExtenalEndPointHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings settings;

        public ExtenalEndPointHealthCheck(IOptions<ServiceSettings> options)
        {
            this.settings = options.Value;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            Ping ping = new();
            var reply = await ping.SendPingAsync(settings.OpenWeatherhost);
            if(reply.Status != IPStatus.Success)
            {
                return HealthCheckResult.Unhealthy();
            }
            return HealthCheckResult.Healthy();
        }
    }
}
