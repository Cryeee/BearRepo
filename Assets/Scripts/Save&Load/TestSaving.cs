using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSaving : MonoBehaviour
{
    PlayerData data;

    [ContextMenu("ChangeValues")]
    void ChangeValues()
    {
        data = new PlayerData();
        data.stars[0] = 3;
        data.times[0] = 81.98f;
        Debug.Log("values changed");
    }

    [ContextMenu("Save")]
    void Save()
    {
        SaveLoadManager.Save(data);
        Debug.Log("Saved!");
    }

    [ContextMenu("Load")]
    void Load()
    {
        data = SaveLoadManager.Load();
        Debug.Log("Loaded!");
        Debug.Log(data.stars[0].ToString());
        Debug.Log(data.times[0].ToString());
    }
}
