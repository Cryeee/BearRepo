using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSkins : MonoBehaviour
{
    #region Gameobject References

    public SkinnedMeshRenderer skinnyBear;
    public SkinnedMeshRenderer ballBear;

    public Mesh[] skinnyMeshes;
    public Mesh[] ballMeshes;
    public Material[] materials;

    public static int currentSkin = 0;
    private int index = 0;
    private PlayerData saveFile;

    public GameObject lockIcon;

    #endregion

    private void Start()
    {
        index = currentSkin;
    }

    public void Initialize(PlayerData saveFile)
    {
        this.saveFile = saveFile;
        SetSkin(saveFile.currentSkin);
    }

    public void SetSkin(int id)
    {
        skinnyBear.material = materials[id];
        skinnyBear.sharedMesh = skinnyMeshes[id];


        if(ballBear != null)
        {
            ballBear.material = materials[id];
            ballBear.sharedMesh = ballMeshes[id];
        }

        if(saveFile != null)
        {
            if (saveFile.unlockedSkins >= id)
            {
                currentSkin = id;
                lockIcon.SetActive(false);
            }
            else
            {
                lockIcon.SetActive(true);
            }
        }
    }

    // Right button to select next skin:
    public void NextSkin()
    {
        if(index + 1 <= 4)
        {
            SetSkin(index + 1);
            index++;
        }
        
    }

    // Left button to select previous skin:
    public void PreviousSkin()
    {
        if(index -1 >= 0)
        {
            SetSkin(index - 1);
            index--;
        }
    }

    public void DisableLock()
    {
        lockIcon.SetActive(false);
        SetSkin(currentSkin);
        index = currentSkin;
    }
}
