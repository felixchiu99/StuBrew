using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITouchable : MonoBehaviour
{
    [Tooltip("The layers that cause the Button to be activated")]
    public LayerMask collisionTriggers = ~0;
    [Tooltip("CustomTag for Touch collider tag")]
    [SerializeField] string customTag = "FingerTip";

    bool isTrigger = true;
    Autohand.Hand hand;

    public void SetInactive()
    {
        isTrigger = false;
    }
    public void SetActive()
    {
        isTrigger = true;
    }
    public bool IsActive()
    {
        return isTrigger;
    }

    public void IgnoreHand(Autohand.Hand h, Autohand.Grabbable grab)
    {
        hand = h;
    }
    public void RemoveHand()
    {
        hand = null;
    }

    public bool CheckHand(Autohand.Hand h)
    {
        return h != hand;
    }

    void OnTriggerExit(Collider other)
    {
        if (collisionTriggers == (collisionTriggers | (1 << other.gameObject.layer)) && !IsActive())
        {
            SetActive();
        }
    }

    public bool IsFingerTip(Collider other)
    {
        if (other.gameObject.TryGetComponent<CustomTag>(out CustomTag tag))
        {
            if (customTag == null || customTag == "")
            {
                return false;
            }
            else
            {
                return tag.HasTag(customTag);
            }
        }
        else
        {
            return false;
        }
    }
}
