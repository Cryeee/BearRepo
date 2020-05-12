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
    private PlayerData saveFile;

    #endregion

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

        currentSkin = id;
    }

    // Right button to select next skin:
    public void NextSkin()
    {
        if(currentSkin < saveFile.unlockedSkins)
        {
            currentSkin++;
            SetSkin(currentSkin);
        }
    }

    // Left button to select previous skin:
    public void PreviousSkin()
    {
        if(currentSkin > 0)
        {
            currentSkin--;
            SetSkin(currentSkin);
        }
    }
}
