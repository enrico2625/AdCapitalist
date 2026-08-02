using System;
using System.Collections.Generic;
using System.Linq;
using BreakInfinity;

[System.Serializable]
public class Buisness
{
    public BuisnessEnum name;
    public BigDouble startIncome;
    public BigDouble IncomeProduced ;
    public int DelayProduceAction;
    public BigDouble Coefficient;
    public BigDouble basePrice;
    public BigDouble BranchCounter;
    public BigDouble PriceNextBranche;
    public BigDouble bonusIncome;
    public bool isManager;
    private BigDouble CurrentPrice;


    public Buisness(BuinsnessData data)
    {
        IncomeProduced = data.IncomeProduced;
        startIncome = data.IncomeProduced;
        DelayProduceAction = data.DelayProduceAction;
        basePrice = data.basePrice;
        CurrentPrice = data.basePrice;
        Coefficient = data.Coefficient;
        name = data.name;
        BranchCounter = 0;
        bonusIncome = 1;
        isManager = false;
        calculatePriceNextBranch();
    }

    private void calculatePriceNextBranch()
    {
        CurrentPrice = Formulary.calculateCurrentPrice(basePrice, Coefficient, BranchCounter);
        PriceNextBranche = Formulary.CalcolaNextPurchesCost(CurrentPrice, Coefficient, 1);
    }

    public void branchPurched()
    {
        BranchCounter++;
        CurrentPrice = PriceNextBranche;
        calculatePriceNextBranch();
        calculatedIncomeProduced();
    }

    public void calculatedIncomeProduced()
    {
        List<Bonus> bonusList = GameManagaer.Instance.GetBonusesByType(BonusTypeEnum.Upgrade);
        IncomeProduced = Formulary.calcolateProductionIncome(startIncome, BranchCounter, bonusList);
    }

}


[System.Serializable]
public class BuinsnessData
{
    public BuisnessEnum name;
    public int IncomeProduced;
    public int DelayProduceAction;
    public float Coefficient;
    public int basePrice;
    public BuisnessBonusData buisnessBonus;
}

public static class BuisnessNameStringMapper
{
    public static string ToReadableString(this BuisnessEnum value) =>
        value switch
        {
            BuisnessEnum.Lemonade => "lemon",
            BuisnessEnum.News => "news",
            BuisnessEnum.Car => "car",
            BuisnessEnum.Pizza => "pizza",
            BuisnessEnum.Donut => "donout",
            BuisnessEnum.Shirimp => "shrimp",
            BuisnessEnum.Hokey => "hockey",
            BuisnessEnum.Cinema => "cinema",
            BuisnessEnum.Bank => "bank",
            BuisnessEnum.Oil => "oil",
            _ => value.ToString()
        };
}
public enum BuisnessEnum
{
    Lemonade,
    News,
    Car,     
    Pizza,
    Donut,
    Shirimp,
    Hokey,
    Cinema,
    Bank,
    Oil,
}