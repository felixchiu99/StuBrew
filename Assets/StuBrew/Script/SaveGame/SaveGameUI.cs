using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SaveGameUI : MonoBehaviour
{
    int fileIndex;
    [SerializeField]
    UnityEvent<int> saveGame;
    public void ChangeFilename(int fileIndex)
    {
        this.fileIndex = fileIndex;
    }

    public void save()
    {
        saveGame?.Invoke(fileIndex);
    }

}
