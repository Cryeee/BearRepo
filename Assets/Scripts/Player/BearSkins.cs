using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSkins : MonoBehaviour
{
    #region Gameobject References

    public Renderer playerBear;
    public Material brown;
    public Material polar;
    public Material panda;

    public static int currentSkin = 0;
    private int unlockedSkins;

    #endregion

    private PlayerData saveFile;

    // Start is called before the first frame update
    void Start()
    {
        saveFile = SaveLoadManager.Load();
        SetSkin(saveFile.currentSkin);
        unlockedSkins = saveFile.unlockedSkins;
    }

    private void OnDisable()
    {
        SaveSkinChange();
    }

    private void SetSkin(int id)
    {
        // Set menu's bear to match last used skin:
        switch (id)
        {
            case 0:
                playerBear.material = brown;
                currentSkin = 0;
                break;
            case 1:
                playerBear.material = polar;
                currentSkin = 1;
                break;
            case 2:
                playerBear.material = panda;
                currentSkin = 2;
                break;
            default:
                playerBear.material = brown;
                currentSkin = 0;
                break;
        }
    }

    // Right button to select next skin:
    public void NextSkin()
    {
        if(currentSkin < unlockedSkins)
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

    // When exiting menu, save current skin:
    private void SaveSkinChange()
    {
        saveFile.currentSkin = currentSkin;
        SaveLoadManager.Save(saveFile);
    }
}
