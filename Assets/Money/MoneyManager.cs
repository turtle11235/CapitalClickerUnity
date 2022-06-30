public class MoneyManager
{
    public static MoneyManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = new MoneyManager();
            }
            return _Instance;
        }
    }
    private static MoneyManager _Instance;

    public Money CurrentMoney { get; private set; }
    public Money AccumulatedMoney { get; private set; }
    public Money MaxMoneySoFar { get; private set; }
    public Money UserClickVal { get; private set; }
    public Money WorkerClickVal { get; private set; }

    private MoneyManager()
    {
        this.CurrentMoney = new Money(0);
        this.AccumulatedMoney = new Money(0);
        this.MaxMoneySoFar = new Money(0);
        this.UserClickVal = new Money(0.01);
        this.WorkerClickVal = new Money(0.01);
    }

    public void SpendMoney(Money amount)
    {
        this.CurrentMoney -= amount;
    }

    public void EarnMoney(Money amount)
    {
        this.CurrentMoney += amount;
        this.AccumulatedMoney += amount;
        if (this.CurrentMoney > this.MaxMoneySoFar)
        {
            this.MaxMoneySoFar.SetValue(this.CurrentMoney);
        }
    }

    public void UserClick()
    {
        EarnMoney(UserClickVal);
    }

    public void WorkerClick(int numWorkers = 1)
    {
        EarnMoney(WorkerClickVal * numWorkers);
    }

    public void SetUserClickVal(Money amount)
    {
        this.UserClickVal.SetValue(amount);
    }

    public void SetWorkerClickVal(Money amount)
    {
        this.WorkerClickVal.SetValue(amount);
    }
}
