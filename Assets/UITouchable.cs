using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITouchable : MonoBehaviour
{
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
}
