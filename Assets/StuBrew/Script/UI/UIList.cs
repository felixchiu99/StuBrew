using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

[System.Serializable]
public class UIComponent
{
    public GameObject UIElement;

    [HideInInspector]
    public RectTransform rectTransform;

    public UIComponent(GameObject element)
    {
        UIElement = element;
    }
}

public class UIList : MonoBehaviour
{
    [SerializeField] bool manualComponents = true;
    [ShowIf("manualComponents")]
    [SerializeField] List<UIComponent> uiComponents;
    [HideIf("manualComponents")]
    [SerializeField] Transform parentUI;

    public Button[] prevBtn;
    public Button[] nextBtn;

    [Tooltip("The Maximum columns(x) and rows(y) this UI can display.")]
    [MinValue(1)]
    [SerializeField] Vector2 gridSize = new Vector2(3, 1);

    int firstItem = 0;
    int maxShownOnPage = 0;
    void Awake()
    {
        if (!manualComponents)
        {
            uiComponents.Clear();
            for (int i = 0; i< parentUI.childCount; i++)
            {
                uiComponents.Add(new UIComponent(parentUI.GetChild(i).gameObject));
            }
        }

        maxShownOnPage = (int)(gridSize.x * gridSize.y);

        RearrangeUIList();

        for (int i = 0; i < uiComponents.Count; i++)
        {
            uiComponents[i].rectTransform = uiComponents[i].UIElement.GetComponent<RectTransform>();
        }

        PrevItem();
        //CalculateUIElement();
    }

    void RearrangeUIList()
    {
        if (maxShownOnPage > uiComponents.Count)
        {
            if (uiComponents.Count <= gridSize.x)
            {
                gridSize.x = uiComponents.Count;
                gridSize.y = 1;
            }
            else
            {
                gridSize.y = Mathf.Ceil((float)uiComponents.Count / gridSize.x);
            }
            maxShownOnPage = uiComponents.Count;
        }
    }

    void CalculateUIElement()
    {
        for (int i = 0; i < uiComponents.Count; i++)
        {
            int current = i - firstItem;
            if (i < firstItem || i >= maxShownOnPage + firstItem)
            {
                uiComponents[i].UIElement.SetActive(false);
            }
            else 
            {
                float gapX = 1 / gridSize.x * 0.8f;
                gapX = -(gridSize.x / 2 * -gapX + gapX / 2 + current % gridSize.x * gapX);
                float gapY = 1 / gridSize.y * 0.8f;
                gapY = -(gridSize.y / 2 * gapY + -gapY / 2 + (int)(current / gridSize.x) * -gapY);

                Vector2 anchor = new Vector2(0.5f - gapX, 0.5f - gapY);

                uiComponents[i].rectTransform.anchorMin = anchor;
                uiComponents[i].rectTransform.anchorMax = anchor;
                uiComponents[i].rectTransform.anchoredPosition = new Vector3(0,0,0);
                uiComponents[i].UIElement.SetActive(true);  
            }
             
        }
    }

    void SetBtnState(Button[] btns, bool state)
    {
        foreach (Button btn in btns){
            btn.interactable = state;
        }
    }

    public void PrevItem()
    {
        SetBtnState(nextBtn, true);
        if (firstItem > 0) 
        {
            firstItem--;
        }
        
        if(firstItem <= 0)
        {
            SetBtnState(prevBtn, false);
        }
        CalculateUIElement();
    }

    public void NextItem()
    {
        SetBtnState(prevBtn, true);
        if (firstItem + maxShownOnPage < uiComponents.Count)
        {
            firstItem++;
        }
        if(firstItem + maxShownOnPage >= uiComponents.Count)
        {
            SetBtnState(nextBtn, false);
        }

        CalculateUIElement();
    }

    public void SetScale(float height)
    {

    }
}
