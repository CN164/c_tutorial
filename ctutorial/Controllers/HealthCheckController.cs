using ctutorial.BusinessFlow;
using ctutorial.Models;
using Microsoft.AspNetCore.Mvc;

namespace ctutorial.Controllers
{
    public class HealthCheckController
    {
        private readonly HealthCheckBusinessFlow _healthCheckBusinessFlow;
        public HealthCheckController(HealthCheckBusinessFlow healthCheckBusinessFlow)
        {
            _healthCheckBusinessFlow = healthCheckBusinessFlow;
        }
        [HttpGet("/healthcheck")]
        public HealthCheckResponse HealthCheck()
        {
            return _healthCheckBusinessFlow.HealthCheckFlow();
        }
        [HttpPost("/loopTest")]
        public string LoopTest([FromBody] CreateTransactionRequest request)
        {
            return _healthCheckBusinessFlow.ProcessCreateTransaction(request);
        }
    }
}
