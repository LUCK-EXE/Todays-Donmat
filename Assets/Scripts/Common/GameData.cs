[System.Serializable]
public class GameData
{
    // 현재 상태
    public int Money;
    public int Debt;
    public int Day;
    public int PlaysLeft;

    // 설정값
    public int MaxPlaysPerDay; 
    public float InterestRate;

    public GameData(int initialMoney, int initialDebt, int maxPlaysPerDay, float interestRate)
    {
        Money = initialMoney;
        Debt = initialDebt;
        MaxPlaysPerDay = maxPlaysPerDay;
        InterestRate = interestRate;

        Day = 1;
        PlaysLeft = maxPlaysPerDay;
    }

    public bool SpendMoney(int amount)
    {
        if (amount < 0) return false;
        if (Money < amount) return false;

        Money -= amount;
        return true;
    }
    
    public void AddMoney(int amount)
    {
        Money += amount;
    }
}
