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

    [SerializeField] float gapScale = 0.8f;
    [SerializeField] Vector2 centre = new Vector2(0.5f, 0.5f);

    int firstItem = 0;
    int maxShownOnPage = 0;
    void Start()
    {
        GetUIListComponents();

        PrevItem();
    }

    public void GetUIListComponents()
    {
        if (!manualComponents)
        {
            uiComponents.Clear();
            for (int i = 0; i < parentUI.childCount; i++)
            {
                GameObject listItem = parentUI.GetChild(i).gameObject;
                if (listItem.activeSelf)
                    uiComponents.Add(new UIComponent(parentUI.GetChild(i).gameObject));
            }
            for (int i = 0; i < uiComponents.Count; i++)
            {
                uiComponents[i].rectTransform = uiComponents[i].UIElement.GetComponent<RectTransform>();
            }
        }
        maxShownOnPage = (int)(gridSize.x * gridSize.y);

        RearrangeUIList();
        CalculateUIElement();
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
                float gapX = 1 / gridSize.x * gapScale;
                gapX = -(gridSize.x / 2 * -gapX + gapX / 2 + current % gridSize.x * gapX);
                float gapY = 1 / gridSize.y * gapScale;
                gapY = -(gridSize.y / 2 * gapY + -gapY / 2 + (int)(current / gridSize.x) * -gapY);

                Vector2 anchor = new Vector2(centre.x - gapX, centre.y - gapY);

                uiComponents[i].rectTransform.anchorMin = anchor;
                uiComponents[i].rectTransform.anchorMax = anchor;
                uiComponents[i].rectTransform.anchoredPosition = new Vector3(0,0,0);
                uiComponents[i].UIElement.SetActive(true);  
            }
             
        }
        SetBtnState(prevBtn, true);
        SetBtnState(nextBtn, true);

        if (firstItem <= 0)
        {
            SetBtnState(prevBtn, false);
        }
        if (firstItem + maxShownOnPage >= uiComponents.Count)
        {
            SetBtnState(nextBtn, false);
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
        if (firstItem > 0) 
        {
            firstItem--;
        }
        CalculateUIElement();
    }

    public void NextItem()
    {
        if (firstItem + maxShownOnPage < uiComponents.Count)
        {
            firstItem++;
        }

        CalculateUIElement();
    }

    public void SetScale(float height)
    {

    }
}
