using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIList : MonoBehaviour
{
    [SerializeField] GameObject[] uiComponents;
    [SerializeField] float gap = 1f;

    [Tooltip("The Maximum columns(x) and rows(y) this UI can display.")]
    [SerializeField] Vector2 gridSize = new Vector2(3, 1);

    int firstItem = 0;
    int maxShownOnPage = 0;
    void Awake()
    {
        maxShownOnPage = (int)(gridSize.x * gridSize.y);
        calculateUIElement();
    }

    void calculateUIElement()
    {
        for (int i = 0; i < uiComponents.Length; i++)
        {
            int current = i - firstItem;
            if (i < firstItem || i >= maxShownOnPage + firstItem)
            {
                uiComponents[i].SetActive(false);
            }
            else 
            {
                uiComponents[i].transform.localPosition = new Vector3(gridSize.x / 2 * -gap + gap / 2 + current % gridSize.x * gap, gridSize.y / 2 * gap + -gap / 2 + (int)(current / gridSize.x) * -gap, 0);
                uiComponents[i].SetActive(true);
            }
            
        }
    }

    public void PrevItem()
    {
        if(firstItem > 0)
            firstItem--;
        calculateUIElement();
    }

    public void NextItem()
    {
        if (firstItem + maxShownOnPage < uiComponents.Length)
            firstItem++;
        calculateUIElement();
    }

}
