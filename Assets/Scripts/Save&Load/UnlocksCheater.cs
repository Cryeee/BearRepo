﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlocksCheater : MonoBehaviour
{
    [Header("0 means only tutorial level is unlocked")]
    [Range(0, 2)]
    public int unlockedLevels;

    [Header("how many skins are unlocked in addition to brown")]
    [Range(0, 2)]
    public int unlockedSkins;

    [Header("0 brown, 1 polar, 2 panda etc.")]
    [Range(0, 2)]
    public int currentSkin;

    [Range(0, 3)]
    public int firstLevelStars;

    [Range(0, 3)]
    public int secondLevelStars;

    [Range(0, 3)]
    public int thirdLevelStars;

    public float firstLevelTime;
    public float secondLevelTime;
    public float thirdLevelTime;

    public bool firstLevelBerryCollected;
    public bool secondLevelBerryCollected;
    public bool thirdLevelBerryCollected;
}