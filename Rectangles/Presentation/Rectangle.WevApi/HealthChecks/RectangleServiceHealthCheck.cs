using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Rectangle.WebApi.HealthChecks
{
    public class RectangleServiceHealthCheck : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            return await Task.FromResult(HealthCheckResult.Healthy("Rectangle service is working"));
        }
    }
}
