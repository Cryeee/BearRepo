using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
    public int[] stars = new int[3];
    public int unlockedLevels;
    public float[] times = new float [3];
    public bool[] goldenBerriesCollected = new bool[3];

    // 0 normal, 1 panda, 2 polar, 3 nallepuh:
    public int currentSkin;
    public int unlockedSkins;
}
