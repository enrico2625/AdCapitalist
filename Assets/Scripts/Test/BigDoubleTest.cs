using System;
using BreakInfinity;
using UnityEngine;

public class BigDoubleTest : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        BigDouble myNumber = 5;
        String str = myNumber.ToString();
        BigDouble parsedNumber = BigDouble.Parse(str);
        Debug.Log("in stringa: " + str);
        Debug.Log("in stringa: " + parsedNumber);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
