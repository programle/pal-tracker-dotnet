using System.Linq;
using Steeltoe.Common.HealthChecks;
using static Steeltoe.Common.HealthChecks.HealthStatus;

namespace PalTracker
{
    public class TimeEntryHealthContributor : IHealthContributor
    {
        private readonly ITimeEntryRepository _timeEntryRepo;
        public const int MaxTimeEntries = 5;

        public TimeEntryHealthContributor(ITimeEntryRepository repo)
        {
            this._timeEntryRepo = repo;
        }

        public string Id { get; } = "timeEntry";

        public HealthCheckResult Health()
        {
            var count = _timeEntryRepo.List().Count();
            var status = count < MaxTimeEntries ? UP : DOWN;

            var health = new HealthCheckResult {Status = status};

            health.Details.Add("threshold", MaxTimeEntries);
            health.Details.Add("count", count);
            health.Details.Add("status", status.ToString());

            return health;
        }
    }
}