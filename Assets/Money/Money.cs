using System;

/// <summary>
/// Provides operations and formatting for money.
/// Avoids floating point / rounding issues by storing money in cents form.
/// </summary>
public class Money
{
    /// <summary>
    /// This property stores money in cents form.
    /// E.g. $40.52 is stored as 4052.
    /// </summary>
    private long cents { get; set; }

    /// <summary>
    /// Expects input to already be in cents.
    /// </summary>
    private Money(long money)
    {
        this.cents = money;
    }

    public Money(Money money)
    {
        this.cents = money.cents;
    }

    public Money(double money)
    {
        this.cents = (int)Math.Round(money * 100);
    }

    public void SetValue(Money money)
    {
        this.cents = money.cents;
    }

    public static Money operator +(Money a, Money b)
        => new Money(a.cents + b.cents);

    public static Money operator -(Money a, Money b)
        => new Money(a.cents - b.cents);

    public static Money operator *(Money a, int b)
        => new Money(a.cents * b);

    public static bool operator <(Money a, Money b)
        => a.cents < b.cents;

    public static bool operator >(Money a, Money b)
        => a.cents > b.cents;

    public override string ToString() => $"${this.cents / 100}.{this.cents % 100:00}";
}
