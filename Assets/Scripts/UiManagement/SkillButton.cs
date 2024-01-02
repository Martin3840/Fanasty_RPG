using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    public static event Action<SkillType> OnSkillButtonClicked;
    public SkillType type;
    public Image image;
    private float timer = 0;
    public bool isHolding = false;

    void Awake()
    {
        image = this.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if (isHolding)
        {
            timer += Time.deltaTime;
            if (timer > 3.0f)
            {
                gameObject.GetComponentInParent<SkillUI>().GetSkillDescription(type);
                timer = 0;
                isHolding = false;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isHolding = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHolding = false;
        timer = 0;
    }

    public void Setup(Sprite icon)
    {
        image.sprite = icon;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        OnSkillButtonClicked(type);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        this.transform.localScale *= 1.25f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        this.transform.localScale /= 1.25f;
    }
}
