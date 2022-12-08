using Shared.Common.Classes;

namespace Shared.Common.ValueObjects
{
    public sealed class RecurringPayment : ValueObject
    {
        public int TrialPeriod { get; set; }
        public double RecurringPrice { get; set; }
        public int RebillFrequency { get; set; }
        public int TimeFrameBetweenRebill { get; set; }
        public int SubscriptionDuration { get;  set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TrialPeriod;
            yield return RecurringPrice;
            yield return RebillFrequency;
            yield return TimeFrameBetweenRebill;
            yield return SubscriptionDuration;
        }
    }
}
