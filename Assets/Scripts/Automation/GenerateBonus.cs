using System.Collections.Generic;
using BreakInfinity;
using JetBrains.Annotations;
using UnityEngine;

public static class GenerateBonus
{
    public static List<Bonus> GenerateBonusByGeneretionData(BonusGenerationData bonusData)
    {
        if(bonusData == null)
        {
            Debug.Log("BonusData is null");
            return null;
        }

        List<Bonus> bonusList = new List<Bonus>();
        BigDouble currentCost = bonusData.baseCost;
        BonusTypeEnum bonusType = bonusData.type;
        BuisnessEnum bonusName = bonusData.name;
        List<bonusCurveSegment> bonusSegmentList = bonusData.curveSegmentList;

        if(bonusData.type == BonusTypeEnum.Manager)
        {
            Bonus bonus = new Bonus (bonusType, "",bonusName, TargetParameterEnum.None, currentCost, 1);
            bonusList.Add(bonus);
            return bonusList;
        }
        
        int bonusCount = 1;
        foreach (bonusCurveSegment segment in bonusSegmentList)
        {
            List<float> multiplaierList =  GenerateBonusMultipierList(segment);
            BigDouble growthRate = segment.Coefficient;
            foreach (float multiplaier in multiplaierList)
            {
                BigDouble price = Formulary.calculateCurrentPrice(bonusData.baseCost, growthRate, bonusCount);
                    
                Bonus bonus = new Bonus (bonusType, "",bonusName, segment.TargetParameter, price, multiplaier);
                bonusList.Add(bonus);
                bonusCount++;
            }
        }

        return bonusList;
    }

    private static bonusCurveSegment CheckAndFixSegmentData(bonusCurveSegment segment)
    {
        CurvePoint low = segment.baseBonus;
        CurvePoint mid = segment.midBonus;
        CurvePoint top = segment.topBonus;

        if(low.count == 0 && mid.count == 0 && top.count == 0)
        {
            Debug.LogWarning("All segment caunt is 0");
            return null;
        }
        checkData(low, mid, top);

        if(low.count == 0)
        {
            low.count++;
            low.multiplayer = 0;         
        }


        if(mid.count == 0)
        {
            mid.count++;
            mid.multiplayer = 0;
        }

        if(top.count == 0)
        {
            top.count++;
            top.multiplayer = 0;
        }

        segment.baseBonus = low;
        segment.midBonus = mid;
        segment.topBonus = top;

        return segment;
    }

    public static void checkData(CurvePoint low, CurvePoint mid, CurvePoint top)
    {
        if(
            (low.multiplayer > 0 && low.count == 0) ||
            (mid.multiplayer > 0 && mid.count == 0) ||
            (top.multiplayer > 0 && top.count == 0)
            )
        {
            Debug.LogWarning("Attention! segment have multplaier value but any count");
        }
    }

    public static List<float> GenerateBonusMultipierList(bonusCurveSegment segment)
    {
        segment = CheckAndFixSegmentData(segment);
        if(segment == null)
        {
            Debug.Log("segment is Null");
            return null;
        }

        int baseNumber = segment.baseBonus.count;
        float baseValue = segment.baseBonus.multiplayer;
        int midNumber = segment.midBonus.count;
        float midValue = segment.midBonus.multiplayer;
        int topNumber = segment.topBonus.count;
        float topValue = segment.topBonus.multiplayer; 

        List<float> finalSequence = new List<float>();

        // ============================
        // BASE / MID DISTRIBUTION
        // ============================
        int baseFrequency = 0;
        int baseRest = 0;
        int midFrequency = 0;
        int midRest = 0;

        if (baseNumber >= midNumber && midNumber != 0)
        {
            baseFrequency =baseNumber / midNumber; // after each X base place 1 mid
            baseRest = baseNumber % midNumber;
        }
        else if (baseNumber < midNumber && baseNumber != 0)
        {
            midFrequency = midNumber / baseNumber; // after 1 base place X mid
            midRest = midNumber % baseNumber;
        }

        int topFrequency = 0;
        int skipMidSequence = 0;
        int topRest = 0;

        if (midNumber >= topNumber && topNumber != 0)
        {
            topFrequency = midNumber / topNumber;// after X mid place 1 top
            skipMidSequence = midNumber % topNumber;
        }
        else if (midNumber < topNumber && midNumber != 0)
        {
            topFrequency = topNumber / midNumber; // after 1 mid place X top
            topRest = topNumber % midNumber;
        }

        // ============================
        // MID > TOP
        // ============================

        if (midNumber >= topNumber)
        {
            if(skipMidSequence == 0)skipMidSequence = topFrequency;

            // ============================
            // BASE > MID
            // ============================

            if (baseNumber >= midNumber)
            {
                int i = 0;

                while (baseNumber > 0)
                {
                    int count = 0;

                    while (
                        (count < baseFrequency || baseNumber-baseRest < baseFrequency)
                        && baseNumber > 0)
                    {
                        finalSequence.Add(baseValue);
                        count++;
                        baseNumber--;
                    }

                    if (midNumber > 0)
                    {
                        finalSequence.Add(midValue);
                        midNumber--;

                        if (skipMidSequence <= 0 &&
                            topNumber > 0)
                        {
                            finalSequence.Add(topValue);
                            topNumber--;
                            continue;
                        }

                        skipMidSequence--;
                    }

                    i++;
                }
            }

            // ============================
            // BASE < MID
            // ============================

            else
            {
                int i = 0;

                while (midNumber > 0)
                {
                    if (baseNumber > 0)
                    {
                        finalSequence.Add(baseValue);
                        baseNumber--;
                    }

                    int count = 0;

                    while (
                        (count < midFrequency || midNumber-midRest < midFrequency)
                        && midNumber > 0)
                    {
                        finalSequence.Add(midValue);
                        midNumber--;
                        count++;
                    }

                    if(topNumber == midFrequency)skipMidSequence = 0;

                    if ((skipMidSequence <= 0 )&& topNumber > 0)
                    {
                        finalSequence.Add(topValue);
                        topNumber--;
                    }
                    else
                    {
                        skipMidSequence--;
                    }

                    i++;
                }
            }
        }

        // ============================
        // TOP > MID
        // ============================

        else
        {
            // ============================
            // BASE > MID
            // ============================

            if (baseNumber >= midNumber)
            {
                int i = 0;

                while (baseNumber > 0)
                {
                    int count = 0;

                    while (
                        (count < baseFrequency ||
                        baseNumber-baseRest < baseFrequency)
                        && baseNumber > 0)
                    {
                        finalSequence.Add(baseValue);
                        count++;
                        baseNumber--;
                    }

                    if (midNumber > 0)
                    {
                        finalSequence.Add(midValue);
                        midNumber--;
                    }

                    count = 0;

                    while (
                        (count < topFrequency ||
                        topNumber -topRest< topFrequency)
                        && topNumber > 0)
                    {
                        finalSequence.Add(topValue);
                        count++;
                        topNumber--;
                    }

                    i++;
                }
            }

            // ============================
            // BASE < MID
            // ============================

            else
            {
                int i = 0;

                while (midNumber > 0)
                {

                    if (baseNumber > 0)
                    {
                        finalSequence.Add(baseValue);
                        baseNumber--;
                    }

                    int count = 0;

                    while (
                        (count < midFrequency || midNumber-midRest < midFrequency)
                        && midNumber > 0)
                    {
                        finalSequence.Add(midValue);
                        midNumber--;
                        count++;

                        int topCount = 0;

                        while (
                            (topCount < topFrequency || topNumber-topRest < topFrequency)
                            && topNumber > 0)
                        {
                            finalSequence.Add(topValue);
                            topCount++;
                            topNumber--;
                        }
                    }

                    i++;
                }
            }
        }

        finalSequence.RemoveAll(x => x == 0);
        return finalSequence;
    }
}