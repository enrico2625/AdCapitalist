using System;
using System.Collections.Generic;
using BreakInfinity;
using Unity.VisualScripting;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    [SerializeField] 
    private bool loadSaveInEditor = false;

    [SerializeField]
    public List<BuinsnessData> BuisnessDataList;
    public List<Bonus> BuisnessBonusList;
    public GameObject prefabUi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BuisnessBonusList = new List<Bonus> ();
        GameManagaer.Instance.BuinsessList = createBuisnessList(BuisnessDataList);
        GameManagaer.Instance.BonusList = BuisnessBonusList;//createBonusList();
        Instantiate(prefabUi);
        if(loadSaveInEditor)SaveManager.LoadGame(GameManagaer.Instance);
        
    }

    private void OnDestroy()
    {
        SaveManager.SaveGame(GameManagaer.Instance);
    }
    public List<Buisness> createBuisnessList(List<BuinsnessData> dataList)
    {
        List<Buisness> buisnessList = new List<Buisness>();

        dataList.ForEach(data => { 
            buisnessList.Add(new Buisness(data));

            if(data.buisnessBonus.upgrades != null)
                BuisnessBonusList.AddRange(GenerateBonus.GenerateBonusByGeneretionData(data.buisnessBonus.upgrades));
            if(data.buisnessBonus.unloks != null)
                BuisnessBonusList.AddRange(GenerateBonus.GenerateBonusByGeneretionData(data.buisnessBonus.unloks));
            if(data.buisnessBonus.managers != null)
                BuisnessBonusList.AddRange(GenerateBonus.GenerateBonusByGeneretionData(data.buisnessBonus.managers));

            BuisnessBonusList.Sort((a, b) => a.Price.CompareTo(b.Price));
        });

        return buisnessList;
    }

    /*
    public List<Bonus> createBonusList()
    {
        List<Bonus> bonusList = new List<Bonus>();
        int price = 50;
        int upgradeIncrement = 2;
        BuisnessDataList.ForEach(b => {
            price = (int)Math.Round(b.basePrice * b.Coefficient);
            Bonus bounus = new Bonus(
                BonusTypeEnum.Manager,
                "ManagerName",
                b.name,
                TargetParameterEnum.None,
                price*2,
                0
            );
            bonusList.Add(bounus);

            bounus = new Bonus(
                BonusTypeEnum.Upgrade,
                "UpgradeName",
                b.name,
                TargetParameterEnum.Income,
                price+2,
                upgradeIncrement
            );
            bonusList.Add(bounus);

            upgradeIncrement++;
        });

        return bonusList;
    }
    */
}
