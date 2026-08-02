using System.Collections.Generic;
using BreakInfinity;
using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Bonus",
    menuName = "Idle Game/Bonus")]
public class BuisnessBonusData: ScriptableObject
{
    public BonusGenerationData upgrades;
    public BonusGenerationData unloks;
    public BonusGenerationData managers;
}

[System.Serializable]
public class BonusGenerationData
{
    public BuisnessEnum name;
    public BonusTypeEnum type;
    public int baseCost;
    public List<bonusCurveSegment> curveSegmentList;
}

[System.Serializable]
public class bonusCurveSegment
{
    public TargetParameterEnum TargetParameter;
    public float Coefficient;
    public CurvePoint topBonus;
    public CurvePoint midBonus;
    public CurvePoint baseBonus;
}

[System.Serializable]
public class CurvePoint
{
    public int count;
    public float multiplayer;
}
