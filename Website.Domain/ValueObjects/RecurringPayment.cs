using Website.Domain.Common;

namespace Website.Domain.ValueObjects
{
    public sealed class RecurringPayment : ValueObject
    {
        public int TrialPeriod { get; private set; }
        public double RecurringPrice { get; private set; }
        public int RebillFrequency { get; private set; }
        public int TimeFrameBetweenRebill { get; private set; }
        public int SubscriptionDuration { get; private set; }

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
