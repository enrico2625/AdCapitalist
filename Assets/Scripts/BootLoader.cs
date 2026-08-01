using System;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : MonoBehaviour
{
    [SerializeField]
    public List<BuinsnessData> BuisnessDataList;
    public GameObject prefabUi;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManagaer.Instance.BuinsessList = createBuisnessList(BuisnessDataList);
        GameManagaer.Instance.BonusList = createBonusList();
        Instantiate(prefabUi);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Buisness> createBuisnessList(List<BuinsnessData> dataList)
    {
        List<Buisness> buisnessList = new List<Buisness>();

        dataList.ForEach(data => { buisnessList.Add(new Buisness(data)); });

        return buisnessList;
    }

    public List<Bonus> createBonusList()
    {
        List<Bonus> bonusList = new List<Bonus>();
        int price = 50;
        int upgradeIncrement = 1;
        BuisnessDataList.ForEach(b => {
            price = (int)Math.Round(b.CurrentPrice * b.Coefficient);
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
}
