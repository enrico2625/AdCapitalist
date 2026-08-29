using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using BreakInfinity;
using UnityEngine;

public class GameManagaer : MonoBehaviour
{
    public static GameManagaer Instance { get; private set; }
    public GameEvent gameEvent;

    public List<Buisness> BuinsessList = new();
    public List<Bonus> BonusList = new();

    public List<Bonus> Unlocks;
    public List<Bonus> Upgrades;
    public List<Bonus> Managers;

    public BigDouble monney = 100;

    [SerializeField]
    private int[] buyModeValue = {1, 10, 100, 0};
    private int buyModeIndex = 0;
    private int currentBuyMode = 1;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void setBuyMode()
    {
        buyModeIndex++;
        if(buyModeIndex >= buyModeValue.Length)buyModeIndex = 0;
        currentBuyMode = buyModeValue[buyModeIndex];

        foreach (Buisness b in BuinsessList)
        {
            b.calculatePriceNextBranch();
        }

    }

    public int getBuyMode()
    {
        return buyModeValue[buyModeIndex];
    }

    public List<Bonus> GetBonusesByType(BonusTypeEnum type)
    {
        return BonusList.Where(b => b.type == type).ToList();
    }

    public List<Bonus> GetBonusesByTarget(TargetParameterEnum target)
    {
        return BonusList.Where(b => b.TargetParameter == target).ToList();
    }

    public Buisness FindBuisnessByName(BuisnessEnum name)
    {
        return BuinsessList.Find(b => b.name == name);
    }

    public List<Bonus> FindBonusToCalculateIncome(BuisnessEnum name)
    {
        return BonusList.Where(b => 
        (b.Buisness == name 
        && b.type != BonusTypeEnum.Manager 
        && b.TargetParameter == TargetParameterEnum.Income)).ToList();
    }

    public Bonus FindNextUnlock(BuisnessEnum name)
    {
        return BonusList.Find(b => 
        (b.Buisness == name 
        && b.type == BonusTypeEnum.Unlock 
        && b.isObtained == false));
    }

    public void ChangeMonney(BigDouble value)
    {
        monney += value;
        gameEvent.Raise();
    }
}
