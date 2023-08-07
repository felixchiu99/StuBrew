using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField] RectTransform backgroundImage;
    [SerializeField] RectTransform screen;

    [SerializeField] float scaleX = 0.98f;
    [SerializeField] float scaleY = 0.98f;
    public void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ResizeBorder()
    {
        if (backgroundImage)
        {
            float offset = 10f;
            backgroundImage.offsetMin = new Vector2(offset, offset);
            backgroundImage.offsetMax = new Vector2(-offset, -offset);
        }
        if (screen)
        {
            Vector3 newScale = screen.localScale;
            newScale.x = newScale.x * scaleX;
            newScale.y = newScale.y * scaleY;
            screen.localScale = newScale;
        }
    }
}
