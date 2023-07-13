using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildIgnoreCollider : MonoBehaviour
{
    public Collider[] toBeIgnored;

    public void Awake()
    {
        MakeChildrenIgnore();
    }
    protected void MakeChildrenIgnore()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            AddChildIgnoreCollider(transform.GetChild(i));
        }

        void AddChildIgnoreCollider(Transform obj)
        {
            Collider[] cols = obj.GetComponents<Collider>();
            foreach (Collider col in cols)
            {
                for (int i = 0; i < toBeIgnored.Length; i++)
                    Physics.IgnoreCollision(toBeIgnored[i], col, true);
            }
            for (int i = 0; i < obj.childCount; i++)
            {
                AddChildIgnoreCollider(obj.GetChild(i));
            }
        }
    }
}
