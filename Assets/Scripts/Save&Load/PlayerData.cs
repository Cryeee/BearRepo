using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int[] stars = new int[3];
    public int unlockedLevels;
    public bool[] goldenBerriesCollected = new bool[3];
    public bool[] allEatenOnLevel = new bool[3];

    // 0 normal, 1 moon, 2 polar, 3 panda, 4 nallepuh:
    public int currentSkin;
    public int unlockedSkins;
}
