using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletUI : MonoBehaviour
{
    [SerializeField] RectTransform backgroundImage;
    public void Start()
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
    }
}
