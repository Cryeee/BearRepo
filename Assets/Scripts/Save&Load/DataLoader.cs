using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{
    #region Menu-gameobject references
    public Button level2;
    public Button level3;
    public GameObject[] level1Stars = new GameObject[3];
    public GameObject[] level2Stars = new GameObject[3];
    public GameObject[] level3Stars = new GameObject[3];

    public GameObject plat1;
    public GameObject plat2;
    public GameObject plat3;

    public GameObject[] goldenBerriesCollected = new GameObject[3];
    #endregion

    // Load cheated stuff instead of actual saved binaryfile:
    private UnlocksCheater cheater;
    private PlayerData saveFile;
    private BearSkins bearSkins;

    public static bool playedAnimation;
    public Animator startMenuAnimator;

    void Awake()
    {
        cheater = GetComponent<UnlocksCheater>();
        bearSkins = GetComponent<BearSkins>();
        //Hacks();
        Load();
        bearSkins.Initialize(saveFile);
        SetLevelButtonsLocked();
        SetLevelSelectionStars();
        SetGoldenBerries();
    }

    private void Start()
    {
        if (!playedAnimation && startMenuAnimator != null)
        {
            startMenuAnimator.SetBool("firstTime", true);
            playedAnimation = true;
        }
        else if (playedAnimation && startMenuAnimator != null)
        {
            startMenuAnimator.SetBool("firstTime", false);
        }
    }

    private void Load()
    {
        saveFile = SaveLoadManager.Load();
    }

    private void Save()
    {
        SaveLoadManager.Save(saveFile);
    }

    private void Hacks()
    {
        // Override savefile with stuff set on editor:
        saveFile = SaveLoadManager.Load();
        saveFile.unlockedLevels = cheater.unlockedLevels;

        saveFile.stars[0] = cheater.firstLevelStars;
        saveFile.stars[1] = cheater.secondLevelStars;
        saveFile.stars[2] = cheater.thirdLevelStars;

        saveFile.goldenBerriesCollected[0] = cheater.firstLevelBerryCollected;
        saveFile.goldenBerriesCollected[1] = cheater.secondLevelBerryCollected;
        saveFile.goldenBerriesCollected[2] = cheater.thirdLevelBerryCollected;

        //saveFile.currentSkin = cheater.currentSkin;
        saveFile.unlockedSkins = cheater.unlockedSkins;

        Save();

    }

    private void SetLevelButtonsLocked()
    {
        switch (saveFile.unlockedLevels)
        {
            case 0:
                level2.interactable = false;
                level3.interactable = false;
                break;
            case 1:
                level3.interactable = false;
                break;
            case 2:
                level2.interactable = true;
                level3.interactable = true;
                break;
            default:
                level2.interactable = false;
                level3.interactable = false;
                break;
        }
    }

    private void SetLevelSelectionStars()
    {
        for (int i = 0; i < saveFile.stars[0]; i++)
        {
            if (!saveFile.allEatenOnLevel[0])
            {
                level1Stars[i].SetActive(true);
            }
        }

        if(saveFile.allEatenOnLevel[0])
        {
            plat1.SetActive(true);
        }

        for (int i = 0; i < saveFile.stars[1]; i++)
        {

            if (!saveFile.allEatenOnLevel[1])
            {
                level2Stars[i].SetActive(true);
            }

            
        }

        if (saveFile.allEatenOnLevel[1])
        {
            plat2.SetActive(true);
        }

        for (int i = 0; i < saveFile.stars[2]; i++)
        {
            if (!saveFile.allEatenOnLevel[2])
            {
                level3Stars[i].SetActive(true);
            }

            
        }

        if (saveFile.allEatenOnLevel[2])
        {
            plat3.SetActive(true);
        }
    }

    private void SetGoldenBerries()
    {
        for (int i = 0; i < saveFile.goldenBerriesCollected.Length; i++)
        {
            if(saveFile.goldenBerriesCollected[i])
            {
                goldenBerriesCollected[i].SetActive(true);
            }
        }
    }

    private void SaveSkinChange()
    {
        saveFile.currentSkin = BearSkins.currentSkin;
        SaveLoadManager.Save(saveFile);
    }

    // When exiting menu, save current skin:
    private void OnDisable()
    {
        SaveSkinChange();
    }
}
