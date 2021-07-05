namespace Shared.Enums
{
    public enum TenantStatusEnum
    {
        Active = 1,
        // (when they can't take next payment but hasn't ran out of the current month's payment)
        OnHold = 2,
        // (when they can't take next payment and has ran out of the current month's payment)
        Suspended = 3,
        Pending = 4,
        Cancelled = 5
    }
}