using TMPro;
using UnityEngine;

public class MonneyUi : MonoBehaviour
{
    public TextMeshProUGUI monneyText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateMonneyText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMonneyText()
    {
        if(monneyText != null)
            monneyText.SetText("$"+GameManagaer.Instance.monney.ToString());
    }
}
