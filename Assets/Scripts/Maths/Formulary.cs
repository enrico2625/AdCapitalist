using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

public static class Formulary
{

    public static BigDouble calculateCurrentPrice(BigDouble BasePrice, BigDouble GrowthRate, BigDouble BranchCounter )
    {
        return BasePrice * BigDouble.Pow(GrowthRate, BranchCounter);
    }

    public static BigDouble CalcolaNextPurchesCost(BigDouble currentPrice, BigDouble growthRate, int amount)
    {
        BigDouble totalCost =
            currentPrice * 
            (BigDouble.Pow(growthRate, amount) - 1) /
            (growthRate - 1);

        Debug.Log("totalCost: " + totalCost);
        return totalCost;
    }

    public static BigDouble calcolateProductionIncome(BigDouble baseIncome, BigDouble BranchCounter, List<Bonus> bonusLit)
    {
        int multiplier = calculateBonusMultiplier(bonusLit);
        if(multiplier == 0) multiplier= 1;
        BigDouble income = baseIncome * BranchCounter * multiplier;

        Debug.Log("income: " + income);
        return income;
    }

    private static int calculateBonusMultiplier(List<Bonus> bonusLit)
    {
        int total = 0;
        foreach (var bonus in bonusLit)
        {
            if(bonus.isObtained)
                total += bonus.multiplier;
        }

        return total;
    }
}
