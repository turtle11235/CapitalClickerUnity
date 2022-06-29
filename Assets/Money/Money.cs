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
    private int cents { get; set; }

    /// <summary>
    /// Expects input to already be in cents form.
    /// </summary>
    private Money(int money)
    {
        this.cents = money;
    }

    public Money(double money)
    {
        this.cents = (int)Math.Round(money * 100);
    }

    public Money(string money)
    {
        string[] moneyParts = money.Split('.');

        int dollars;
        bool success = int.TryParse(moneyParts[0], out dollars);
        if (success)
        {
            this.cents = dollars * 100;
        }
        else
        {
            this.cents = 0;
        }

        if (moneyParts.Length == 2)
        {
            int cents;
            success = int.TryParse(moneyParts[1], out cents);
            if (success)
            {
                this.cents += cents * 100;
            }
        }
    }

    public static Money operator +(Money a, Money b)
        => new Money(a.cents + b.cents);

    public static Money operator -(Money a, Money b)
        => new Money(a.cents - b.cents);

    public override string ToString() => $"${this.cents / 100}.{this.cents % 100}";
}
