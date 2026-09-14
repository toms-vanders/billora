namespace Billora.Subscriptions.Domain.Plans
{
    public enum BillingStrategy
    {
        Flat = 1, // Fixed amount per cycle, regardless of quantity or usage. $19/mo.
        PerUnit = 2, // Unit price multiplied by the subscription's quantity. 20 seats at $5 = $100.
        Tiered = 3, // Graduated brackets: each bracket is priced separately and the charge is their sum. With 1-10 at $5 and 11-50 at $4, 20 seats = (10 x $5) + (10 x $4) = $90.
        Usage = 4, // Metered consumption counted during the period and billed in arrears. $1 per 1,000 verifications.
    }
}
