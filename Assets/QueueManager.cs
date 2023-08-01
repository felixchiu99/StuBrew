using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using NaughtyAttributes;

public class QueueManager : MonoBehaviour
{
    [SerializeField] Transform queueStart;
    [SerializeField] Transform queueEnd;

    public event UnityAction<Transform, Transform> OnQueueEndChange;

    List<SimpleNPC> inQueue = new List<SimpleNPC>();

    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject NpcPrefab;

    [SerializeField] float destroyTimeout = 5f;

    // Start is called before the first frame update
    void Start()
    {
        queueEnd = queueStart;
        OnQueueEndChange?.Invoke(queueEnd, this.transform);
    }

    public void AddToQueueEnd(GameObject obj)
    {
        if (obj.TryGetComponent<SimpleNPC>(out SimpleNPC npc))
        {
            inQueue.Add(npc);
            ChangeQueueEnd(npc.GetQueuePos(), npc.transform);
        }
    }

    [Button]
    public void RemoveFromQueue()
    {
        if (inQueue.Count > 0)
        {
            inQueue[0].UpdateQueueTarget(spawnPoint, spawnPoint);
            inQueue[0].Destroy(destroyTimeout);
            inQueue.RemoveAt(0);
        }
        if (inQueue.Count > 0)
        {
            inQueue[0].UpdateQueueTarget(queueStart, this.transform);
        }
        else
        {
            ChangeQueueEnd(queueStart, this.transform);
        }
    }

    void ChangeQueueEnd(Transform newTransform, Transform newLook)
    {
        queueEnd = newTransform;
        OnQueueEndChange?.Invoke(queueEnd, newLook);
    }

    public Transform GetLastPlace()
    {
        return queueEnd;
    }

    public Transform GetLastLookAt()
    {
        if (inQueue.Count > 0)
        {
            return inQueue[0].transform;
        }
        
        return null;
    }

    public Transform FindLookAt(SimpleNPC npc)
    {
        int index = inQueue.IndexOf(npc);
        if (index > 0)
        {
            return inQueue[index-1].transform;
        }
        return this.transform;

    }

    [Button]
    public void SpawnNPC()
    {
        if(inQueue.Count < 10)
        {
            GameObject obj = Instantiate(NpcPrefab, spawnPoint.position, Quaternion.identity);
        }   
    }

    public bool HasQueue()
    {
        return inQueue.Count != 0;
    }

    public float GetBonus()
    {
        return 1f ;
    }
}
