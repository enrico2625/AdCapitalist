using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusCardUI : MonoBehaviour
{
    [SerializeField]
    public TextMeshProUGUI bonusNameText;
    public TextMeshProUGUI bonusEffectText;
    public TextMeshProUGUI managerPriceText;
    public Button HireButton;
    public Image buisnessIcon;
    public Image IconBackground;

    [SerializeField]
    public string iconsFilePath;

    private Bonus bonus; 

    public void Init(Bonus bonus)
    {
        this.bonus = bonus;
    }

    public void Start()
    {

        bonusNameText.SetText(bonus.Name);
        managerPriceText.SetText("$" + bonus.Price);
        buisnessIcon.sprite = SpriteLoader.LoadSprite(iconsFilePath, BuisnessNameStringMapper.ToReadableString(bonus.Buisness));

        if(bonus.type == BonusTypeEnum.Manager)
        {
            bonusEffectText.SetText("automize produce action");
        }
        if (bonus.type == BonusTypeEnum.Upgrade)
        {
            bonusEffectText.SetText("profit x" + bonus.multiplier);
        }
        ChangeColor();
    }

    public void Update()
    {
        if (bonus.isObtained)
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
            ChangeColor();
    }

    public void BuyBonusButtonClicked()
    {
        if(GameManagaer.Instance.monney >= bonus.Price)
        {
            GameManagaer.Instance.ChangeMonney(-bonus.Price);
            bonus.isObtained = true;
            deactivetBonusUI();
        }
    }

    private void deactivetBonusUI()
    {
        this.gameObject.SetActive(false);
        GameManagaer.Instance.FindBuisnessByName(bonus.Buisness).calculatedIncomeProduced();
        if (bonus.type == BonusTypeEnum.Manager)
            GameManagaer.Instance.FindBuisnessByName(bonus.Buisness).isManager = true;
    }

    public void ChangeColor()
    {
        if (bonus != null && HireButton != null && buisnessIcon != null && IconBackground != null)
        {
            if (GameManagaer.Instance.monney < bonus.Price)
            {

                Color inactive = new Color();
                if (bonus.type == BonusTypeEnum.Manager)
                    inactive = new Color(150 / 255f, 170 / 255f, 185 / 255f);

                if (bonus.type == BonusTypeEnum.Upgrade)
                    inactive = new Color(165 / 255f, 90 / 255f, 40 / 255f);

                HireButton.interactable = false;
                buisnessIcon.color = inactive;
                IconBackground.color = inactive;
            }
            else
            {

                Color active = new Color();
                if (bonus.type == BonusTypeEnum.Manager)
                    active = new Color(96 / 255f, 160 / 255f, 190 / 255f);

                if (bonus.type == BonusTypeEnum.Upgrade)
                    active = new Color(200 / 255f, 120 / 255f, 70 / 255f);

                HireButton.interactable = true;
                buisnessIcon.color = Color.white;
                IconBackground.color = active;
            }
        }
    }



}
