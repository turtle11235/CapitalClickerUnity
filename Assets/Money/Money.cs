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
    /// Expects input to already be in cents.
    /// </summary>
    private Money(int money)
    {
        this.cents = money;
    }

    public Money(Money money)
    {
        this.cents = money.cents;
    }

    public Money(double money)
    {
        this.cents = DoubleToCents(money);
    }

    public void SetValue<T>(T money)
    {
        if (money is Money m)
        {
            this.cents = m.cents;
        }
        else if (money is int i)
        {
            this.cents = i * 100;
        }
        else if (money is double d)
        {
            this.cents = DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for SetValue: '{money}'");
        }
    }

    public void SetValue(double money)
    {
        this.cents = DoubleToCents(money);
    }

    public override string ToString() => $"${this.cents / 100}.{this.cents % 100:00}";

    public static string Format<T>(T money)
    {
        if (money is Money m)
        {
            return m.ToString();
        }
        else if (money is int i)
        {
            return $"${i}.00";
        }
        else if (money is double d)
        {
            int cents = (int)Math.Round(d * 100) % 100;
            return $"${Math.Floor(d)}.{cents:00}";
        }
        else
        {
            throw new ArgumentException($"Invalid input for ToString: '{money}'");
        }
    }

    private static int DoubleToCents(double money)
    {
        return (int)Math.Round(money * 100);
    }

    public static Money operator +(Money a, object b)
    {
        int newCents;
        if (b is Money m)
        {
            newCents = a.cents + m.cents;
        }
        else if (b is int i)
        {
            newCents = a.cents + i * 100;
        }
        else if (b is double d)
        {
            newCents = a.cents + DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for + operator: '{b}'");
        }
        return new Money(newCents);
    }

    public static Money operator -(Money a, object b)
    {
        int newCents;
        if (b is Money m)
        {
            newCents = a.cents - m.cents;
        }
        else if (b is int i)
        {
            newCents = a.cents + i * 100;
        }
        else if (b is double d)
        {
            newCents = a.cents - DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for - operator: '{b}'");
        }
        return new Money(newCents);
    }

    public static Money operator *(Money a, int b)
        => new Money(a.cents * b);

    public static bool operator <(Money a, object b)
    {
        if (b is Money m)
        {
            return a.cents < m.cents;
        }
        else if (b is int i)
        {
            return a.cents < i * 100;
        }
        else if (b is double d)
        {
            return a.cents < DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for < operator: '{b}'");
        }
    }

    public static bool operator <=(Money a, object b)
    {
        if (b is Money m)
        {
            return a.cents <= m.cents;
        }
        else if (b is int i)
        {
            return a.cents <= i * 100;
        }
        else if (b is double d)
        {
            return a.cents <= DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for <= operator: '{b}'");
        }
    }

    public static bool operator >(Money a, object b)
    {
        if (b is Money m)
        {
            return a.cents > m.cents;
        }
        else if (b is int i)
        {
            return a.cents > i * 100;
        }
        else if (b is double d)
        {
            return a.cents > DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for > operator: '{b}'");
        }
    }

    public static bool operator >=(Money a, object b)
    {
        if (b is Money m)
        {
            return a.cents >= m.cents;
        }
        else if (b is int i)
        {
            return a.cents >= i * 100;
        }
        else if (b is double d)
        {
            return a.cents >= DoubleToCents(d);
        }
        else
        {
            throw new ArgumentException($"Invalid input for >= operator: '{b}'");
        }
    }
}
