using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuisnessCardUI : MonoBehaviour
{
    // Riferimenti agli elementi UI
    [SerializeField]
    public Slider BranchCounterBar;
    public Slider ProduceActionBar;
    public TextMeshProUGUI BranchCounterText;
    public TextMeshProUGUI ProduceActionText;
    public TextMeshProUGUI BuinessNameText;
    public TextMeshProUGUI BranchPriceText;
    public TextMeshProUGUI DeleyText;
    public Button ProduceButton;
    public Button BuyBranchButton;
    public Image buisnessIcon;

    [SerializeField]
    public string iconsFilePath;

    [SerializeField]
    private BuisnessAnimationManager animationManager;

    private Buisness buisness;
    private bool isDelay = false;

    public void Init(Buisness buisness)
    {
        this.buisness = buisness;
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        BranchCounterText.SetText(buisness.BranchCounter.ToString() + "/100");
        ProduceActionText.SetText(buisness.IncomeProduced.ToString());
        BranchPriceText.SetText(buisness.PriceNextBranche.ToString());
        DeleyText.SetText(buisness.DelayProduceAction.ToString());
        ProduceActionBar.maxValue = buisness.DelayProduceAction;
        ProduceActionBar.value = 0;
        buisnessIcon.sprite = SpriteLoader.LoadSprite(iconsFilePath, BuisnessNameStringMapper.ToReadableString(buisness.name));
    }

    // Update is called once per frame
    void Update()
    {
        UpdateIcomeGeneratedText();
        if (buisness.BranchCounter > 0 && buisness.isManager && !isDelay)
        {
            isDelay = true;
            StartCoroutine(CountdownCoroutine());
        }

        if (GameManagaer.Instance.monney < buisness.PriceNextBranche)
            BuyBranchButton.interactable = false;
        else BuyBranchButton.interactable = true;
    }

    private void OnEnable()
    {
        UpdateIcomeGeneratedText();
    }

    public void BuyBranch()
    {
        if (GameManagaer.Instance.monney >= buisness.PriceNextBranche)
        {
            GameManagaer.Instance.ChangeMonney(-buisness.PriceNextBranche);
            buisness.branchPurched();
            BranchCounterText.SetText(buisness.BranchCounter.ToString() + "/100");
            BranchPriceText.SetText(buisness.PriceNextBranche.ToString());
            BranchCounterBar.value = buisness.BranchCounter;
        }

    }

    public void startProduction()
    {
        if(buisness.BranchCounter > 0 && !isDelay && !buisness.isManager)
        {
            isDelay = true;
            StartCoroutine(CountdownCoroutine());
        }
    }

    private IEnumerator CountdownCoroutine()
    {
        float duration = buisness.DelayProduceAction;
        float elapsed = 0f;

        ProduceButton.interactable = false;
        ProduceActionBar.maxValue = duration;
        ProduceActionBar.value = 0;

        animationManager.startAnimation(duration);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float remaining = Mathf.Max(0, duration - elapsed);

            DeleyText.SetText(remaining.ToString("F2"));
            ProduceActionBar.value = elapsed;

            yield return null;
        }

        ProduceActionBar.value = 0;
        DeleyText.SetText(duration.ToString());

        GameManagaer.Instance.ChangeMonney(buisness.IncomeProduced);
        ProduceButton.interactable = true;
        isDelay = false;

        animationManager.stopAnimation();
    }


    /*
    private IEnumerator CountdownCoroutine()
    {
        float decrement = Time.deltaTime;
        float current = buisness.DelayProduceAction;
        ProduceButton.interactable = false;
        ProduceActionBar.maxValue = current;
        ProduceActionBar.value = 0;
        animationManager.startAnimation(current);

        while (current > 0)
        {
            DeleyText.SetText(current.ToString("F2"));
            ProduceActionBar.value+= decrement;
            yield return new WaitForSeconds(decrement);
            current-= decrement;
        }

        ProduceActionBar.value = 0;
        DeleyText.SetText(buisness.DelayProduceAction.ToString());
        GameManagaer.Instance.ChangeMonney(buisness.IncomeProduced);
        ProduceButton.interactable = true;
        isDelay = false;
        animationManager.stopAnimation();
    }
    */

    public void ActiveBuyBranch()
    {
        BuyBranchButton.interactable = true;
    }

    public void UpdateIcomeGeneratedText()
    {
        if(ProduceActionText != null && buisness != null)
            ProduceActionText.SetText(buisness.IncomeProduced.ToString());
    }
}
