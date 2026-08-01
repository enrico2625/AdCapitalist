using System.Collections.Generic;
using System.Linq;
using BreakInfinity;
using UnityEngine;

public class Bonus
{
    public BonusTypeEnum type;
    public string Name;
    public BuisnessEnum Buisness;
    public TargetParameterEnum TargetParameter;
    public BigDouble Price;
    public bool isObtained = false;
    public BigDouble value;

    public Bonus(BonusTypeEnum type, string name, BuisnessEnum buisness, TargetParameterEnum targetParameter, int price, int value)
    {
        this.type = type;
        this.Name = name;
        this.Buisness = buisness;
        this.TargetParameter = targetParameter;
        this.Price = price;
        this.isObtained = false;
        this.value = value;
    }
}


public enum BonusTypeEnum
{
    Unlock,
    Upgrade,
    Manager,
}

public enum TargetParameterEnum
{
    Income,
    Delay,
    None
}