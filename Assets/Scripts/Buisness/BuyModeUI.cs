using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BuyModeUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI buyModeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buyModeText.text = GameManagaer.Instance.getBuyMode().ToString();
    }

    public void OnButtonClick()
    {
        GameManagaer.Instance.setBuyMode();
        buyModeText.text = GameManagaer.Instance.getBuyMode().ToString();
    }
}
