using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public enum EFilterType
{
    None,
    Layers,
    Tags,
    CustomTags,
    GameObjects
}

public class OnTriggerCall : MonoBehaviour
{

    public EFilterType filterType;

    public bool isInverseFilter = false;

    [ShowIf("filterType", EFilterType.Layers)]
    [Tooltip("The layers that Triggers")]
    public LayerMask collisionTriggers = ~0;

    [ShowIf("filterType", EFilterType.Tags)]
    [Tooltip("The tags that Triggers")]
    [Tag] [SerializeField] List<string> tags;

    [ShowIf("filterType", EFilterType.CustomTags)]
    [Tooltip("The customTags that Triggers")]
    [Tag] [SerializeField] List<string> customTags;

    [ShowIf("filterType", EFilterType.GameObjects)]
    [Tooltip("The GameObjects that Triggers")]
    [SerializeField] List<GameObject> gameObjects;

    [SerializeField] List<Collider> ignoreColliders;

    [SerializeField] UnityEvent<Collider> OnTriggerEnterCall;
    [SerializeField] UnityEvent<Collider> OnTriggerStayCall;
    [SerializeField] UnityEvent<Collider> OnTriggerExitCall;

    private void OnTriggerEnter(Collider other)
    {
        if (CheckType(other) && !ignoreColliders.Contains(other))
        {
            OnTriggerEnterCall?.Invoke(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (CheckType(other) && !ignoreColliders.Contains(other))
            OnTriggerStayCall?.Invoke(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (CheckType(other) && !ignoreColliders.Contains(other))
            OnTriggerExitCall?.Invoke(other);
    }

    private bool CheckType(Collider other)
    {
        if (filterType == EFilterType.None)
            return true;
        if (filterType == EFilterType.Layers)
        {
            if(isInverseFilter)
                return collisionTriggers != (collisionTriggers | (1 << other.gameObject.layer)) || collisionTriggers == (collisionTriggers | (1 << other.attachedRigidbody.gameObject.layer));
            return collisionTriggers == (collisionTriggers | (1 << other.gameObject.layer)) || collisionTriggers == (collisionTriggers | (1 << other.attachedRigidbody.gameObject.layer));
        }
        if (filterType == EFilterType.Tags)
        {
            foreach (string tag in tags)
            {
                if (other.tag == tag)
                    if (isInverseFilter)
                        return false;
                    else
                        return true;
                if (other.attachedRigidbody.tag == tag)
                    if (isInverseFilter)
                        return false;
                    else
                        return true;
            }
        }
        if (filterType == EFilterType.CustomTags)
        {
            foreach (string customTag in customTags)
            {
                if (other.gameObject.TryGetComponent<CustomTag>(out CustomTag tag))
                {
                    if (tag.HasTag(customTag))
                        if (isInverseFilter)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                }
                if (other.attachedRigidbody.gameObject.TryGetComponent<CustomTag>(out CustomTag rbTag))
                {
                    if (rbTag.HasTag(customTag))
                        if (isInverseFilter)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }   
                }
            }
        }

        if (filterType == EFilterType.GameObjects)
        {
            foreach (GameObject obj in gameObjects)
            {
                if(other.gameObject == obj || other.attachedRigidbody.gameObject == obj)
                {
                    if (isInverseFilter)
                        return false;
                    else
                        return true;
                }
            }

        }
        if (isInverseFilter)
            return true;
        else
            return false;
    }
}
