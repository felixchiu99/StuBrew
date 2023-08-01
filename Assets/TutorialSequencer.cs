using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TutorialObject
{
    public Transform target;
    public float yOffset = 0;
    public bool isShow = true;
}


public class TutorialSequencer : MonoBehaviour
{
    [SerializeField] public TutorialObject[] tutObj;
    public FloatingArrow arrow;
    public int index = 0;
    public bool isFirstTime = true;

    public event UnityAction<TutorialObject> OnIndexChange;

    public void OnEnable()
    {
        SceneManager.sceneLoaded -= InitArrow;
        SceneManager.sceneLoaded += InitArrow;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= InitArrow;
    }

    public void InitArrow(Scene scene, LoadSceneMode mode)
    {
        if (isFirstTime)
        {
            index = 1;
        }
        else
        {
            index = 0;
        }
        UpdateArrow();

    }

    [Button]
    public void UpdateArrow()
    {
        if (index < tutObj.Length)
        {
            OnIndexChange?.Invoke(tutObj[index]);
        }
            
    }

    public void ChangeIndex(int index)
    {
        if (isFirstTime && index < this.index)
            return;
        this.index = index;
        UpdateArrow();
    }

    public void SetIsFirstTime(bool isFirstTime)
    {
        this.isFirstTime = isFirstTime;
    }
}
