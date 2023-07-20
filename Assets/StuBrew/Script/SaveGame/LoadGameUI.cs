using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LoadGameUI : MonoBehaviour
{
    int fileIndex;
    [SerializeField]
    UnityEvent<int> loadGame;
    public void ChangeFilename(int fileIndex)
    {
        this.fileIndex = fileIndex;
    }

    public void Load()
    {
        loadGame?.Invoke(fileIndex);
    }

}
