using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonTransitioner : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{
    public Color32 m_NormalColor = Color.white;
    public Color32 m_HoverColor = Color.grey;
    public Color32 m_DownColor = Color.white;
    public bool enableChangeColor = true;

    private Image m_Image = null;

    private void Awake()
    {
        m_Image = GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (enableChangeColor)
        {
          //  print("Enter");

            m_Image.color = m_HoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (enableChangeColor)
        {
           // print("Exit");

            m_Image.color = m_NormalColor;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (enableChangeColor)
        {
          //  print("Down");

            m_Image.color = m_DownColor;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (enableChangeColor)
        {
           // print("Up");
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (enableChangeColor)
        {
          //  print("Click");

            m_Image.color = m_HoverColor;
        }
    }
}
