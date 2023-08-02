using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

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

    [SerializeField] TextMeshProUGUI displayText;
    [SerializeField] TextMeshProUGUI displayVistorText;

    [SerializeField] float npcSpawnRate = 30f;

    private int expectedVistor;
    void Start()
    {
        queueEnd = queueStart;
        OnQueueEndChange?.Invoke(queueEnd, this.transform);
        UpdateDisplay();
        expectedVistor = Random.Range(10,50);
        UpdateVistorDisplay();
        InvokeRepeating("SpawnNPC", 1f, npcSpawnRate);
    }

    public void AddToQueueEnd(GameObject obj)
    {
        if (obj.TryGetComponent<SimpleNPC>(out SimpleNPC npc))
        {
            inQueue.Add(npc);
            ChangeQueueEnd(npc.GetQueuePos(), npc.transform);
        }
        UpdateDisplay();
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
        UpdateDisplay();
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
        if(inQueue.Count < 10 && expectedVistor > 0 )
        {
            GameObject obj = Instantiate(NpcPrefab, spawnPoint.position, Quaternion.identity);
            expectedVistor--;
            UpdateVistorDisplay();
        }   
    }

    public bool HasQueue()
    {
        return inQueue.Count != 0;
    }

    public void SellObj(GameObject obj)
    {
        if (inQueue.Count > 0)
        {
            inQueue[0].HoldItem(obj);
        }
    }

    public float GetBonus(GameObject gameObject)
    {
        if (inQueue.Count > 0)
        {
            return inQueue[0].GetBonus(gameObject);
        }
        return 1f;
    }

    public void UpdateDisplay()
    {
        if (inQueue.Count > 0)
        {
            BrewPreference pref = inQueue[0].GetBrewPreference();
            displayText.SetText(pref.GetPreferenceString());
            if (pref.GetIsMore())
            {
                displayText.color = new Color(0, 255, 0, 255);
            }
            else
            {
                displayText.color = new Color(255, 0, 0, 255);
            }
        }
        else
        {
            displayText.SetText("");
        }
        
    }

    public void UpdateVistorDisplay()
    {
        displayVistorText.SetText(expectedVistor.ToString()); 
    }
}
