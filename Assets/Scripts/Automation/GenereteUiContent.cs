using System.Collections.Generic;
using UnityEngine;

public class GenereteUiContent : MonoBehaviour
{
    public GameObject prefab;
    public EnumPrefab prefabEnum;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        generatePrefab();
    }

    private void generatePrefab()
    {
        switch (prefabEnum)
        {
            case EnumPrefab.Buisness:

                GameManagaer.Instance.BuinsessList.ForEach(buisness =>
                {
                    GameObject instance = Instantiate(prefab, transform);
                    BuisnessCardUI buisnessCardUI = instance.GetComponent<BuisnessCardUI>();
                    if (buisnessCardUI != null) { buisnessCardUI.Init(buisness); }
                });
            break;

            case EnumPrefab.Manager:

                List<Bonus> ManagerList = GameManagaer.Instance.GetBonusesByType(BonusTypeEnum.Manager);
                ManagerList.ForEach(manager =>
                {
                    GameObject instance = Instantiate(prefab, transform);
                    BonusCardUI managerCardUi = instance.GetComponent<BonusCardUI>();
                    if (managerCardUi != null) { managerCardUi.Init(manager); }
                });
            break;

            case EnumPrefab.Upgrade:

                List<Bonus> UpgradeList = GameManagaer.Instance.GetBonusesByType(BonusTypeEnum.Upgrade);
                UpgradeList.ForEach(upgrade =>
                {
                    GameObject instance = Instantiate(prefab, transform);
                    BonusCardUI upgradeCardUi = instance.GetComponent<BonusCardUI>();
                    if (upgradeCardUi != null) { upgradeCardUi.Init(upgrade); }
                });
            break;
        }
    }
}

public enum EnumPrefab
{
    Buisness,
    Unlock,
    Upgrade,
    Manager,
}