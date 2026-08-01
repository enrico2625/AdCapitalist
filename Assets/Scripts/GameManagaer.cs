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

    public void ChangeMonney(BigDouble value)
    {
        monney += value;
        gameEvent.Raise();
    }
}
