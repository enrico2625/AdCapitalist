using System;
using System.Collections.Generic;
using UnityEngine;

public class BonusGeneretionTest : MonoBehaviour
{
    [SerializeField]
    bonusCurveSegment bonusCurveSegment;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        floatListStamp(GenerateBonus.GenerateBonusMultipierList(bonusCurveSegment));
    }

    public void floatListStamp(List<float> floatList)
    {
        if (floatList == null)
        {
            Debug.Log("floatList is null");
            return;
        }
        String list = "count:" + floatList.Count + " Lista: | ";
        foreach (float f in floatList)
            list += f + " | ";
        Debug.Log(list);
    }

}
