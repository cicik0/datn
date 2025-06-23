using UnityEngine;
using UnityEngine.UI;

public class ButtonOnShopControl : MonoBehaviour
{
    [SerializeField] Image m_Image;

    Color activeColor = new Color(1f, 1f, 1f);
    Color deActiveColor = new Color(0.5f, 0.5f, 0.5f);

    public void DeactiveButton()
    {
        m_Image.color = deActiveColor;
    }

    public void ActiveButton()
    {
        m_Image.color = activeColor;
    }
    
}
