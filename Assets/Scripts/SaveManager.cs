using System;
using System.Collections.Generic;
using BreakInfinity;
using UnityEngine;

public static class SaveManager
{
    private static char separator = ';';
    private static char secondSeparator = '#';

    private static String playerDataId = "PlayerData";
    private static String buisnessDataId = "BuisnessData";
    private static String bonusDataId = "BonusData";

    public static void SaveGame( GameManagaer gm)
    {
        //save player Data
        String moneyStr = gm.monney.ToString();
        PlayerPrefs.SetString(playerDataId, moneyStr);

        //save buisness Data
        List<Buisness> purchasedBusiness = FindPurchasedBusiness(gm.BuinsessList);
        String buisnessStr = "";
        foreach (Buisness b in purchasedBusiness)
        {
            buisnessStr += b.name.ToString() + separator + b.BranchCounter.ToString() + secondSeparator;
        }
        PlayerPrefs.SetString(buisnessDataId, buisnessStr);

        //Save Bonus Data
        List<Bonus> obtainedBonus = FindObtainedBonus(gm.BonusList);
        String bonusStr = "";
        foreach (Bonus b in obtainedBonus)
        {
            bonusStr +=  b.id.ToString() + secondSeparator;
        }
        PlayerPrefs.SetString(bonusDataId, bonusStr);
    }

    public static void LoadGame(GameManagaer gm)
    {
        String playerDataStr = PlayerPrefs.GetString(playerDataId, "null");
        String buisnessDataStr = PlayerPrefs.GetString(buisnessDataId, "null");
        String bonusDataStr = PlayerPrefs.GetString(bonusDataId, "null");

        if(playerDataStr == null || playerDataStr == "")
        {
            return;
        }
        LoadPlayerData(playerDataStr, gm);

        if(!(buisnessDataStr == null) && !(buisnessDataStr == ""))
        {
            LoadBuisnessData(buisnessDataStr, gm);
        }

        if(!(bonusDataStr == null) && !(bonusDataStr == ""))
        {
            LoadBonusData(bonusDataStr, gm);
        }
        

        foreach (Buisness b in gm.BuinsessList)
        {
            b.calculatedIncomeProduced();
            b.calculatePriceNextBranch();
        }
    }

    private static void LoadPlayerData(String playerDataStr, GameManagaer gm)
    {
        gm.monney = BigDouble.Parse(playerDataStr);
    }

    private static void LoadBuisnessData(String buisnessDataStr, GameManagaer gm)
    {
        buisnessDataStr = buisnessDataStr.Remove(buisnessDataStr.Length -1);
        String[] data = buisnessDataStr.Split(secondSeparator);
        foreach (String b in data)
        {
            String[] buisnessData = b.Split(separator);
            Buisness founded = gm.BuinsessList.Find(buisness => buisness.name.ToString() == buisnessData[0]);
            founded.BranchCounter = BigDouble.Parse(buisnessData[1]);
        }
    }

    private static void LoadBonusData(String bonusDataStr, GameManagaer gm)
    {
        bonusDataStr = bonusDataStr.Remove(bonusDataStr.Length -1);
        String[] data = bonusDataStr.Split(secondSeparator);

        foreach (String b in data)
        {
            Bonus founded = gm.BonusList.Find(bonus => bonus.id.ToString() == b);
            founded.isObtained = true;
            if(founded.type == BonusTypeEnum.Manager)
            {
                gm.FindBuisnessByName(founded.Buisness).isManager = true;
            }
        }
    }

    private  static List<Buisness> FindPurchasedBusiness(List<Buisness> BuinsessList)
    {
        return BuinsessList.FindAll(b => b.BranchCounter > 0);
    }

    private  static List<Bonus> FindObtainedBonus(List<Bonus> bonusList)
    {
        return bonusList.FindAll(b => b.isObtained == true);
    }
}
