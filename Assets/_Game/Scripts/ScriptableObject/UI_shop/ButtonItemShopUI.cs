using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonItemShopUI : MonoBehaviour
{
    [SerializeField] protected Button itemShopBtn;
    [SerializeField] protected Image btnSprite;
    [SerializeField] protected TMP_Text costText;
    [SerializeField] protected Button itemBtn;
    [SerializeField] protected EnumItemCategory itemType;


    public virtual void OnInit(Sprite sp, string price)
    {
        this.btnSprite.sprite = sp;
        this.costText.text = price;
    }

    
    
}
