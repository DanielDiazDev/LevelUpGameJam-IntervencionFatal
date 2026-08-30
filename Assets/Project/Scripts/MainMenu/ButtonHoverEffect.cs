using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //[SerializeField] private Sprite normalSprite;
   // [SerializeField] private Sprite hoverSprite;
    [SerializeField] private Color normalButtonColor = Color.white;
    [SerializeField] private Color hoverButtonColor  = Color.black;
    [SerializeField] private Color normalTextColor = Color.white;
    [SerializeField] private Color hoverTextColor  = Color.black;
    private Image buttonImage;
    private TextMeshProUGUI buttonText;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();

        buttonImage.color = normalButtonColor;
        buttonText.color = normalTextColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonImage.color = hoverButtonColor;
        buttonText.color = hoverTextColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonImage.color = normalButtonColor;
        buttonText.color = normalTextColor;
    }
}