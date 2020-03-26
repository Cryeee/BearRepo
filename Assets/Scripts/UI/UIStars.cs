using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIStars : MonoBehaviour
{
    TMP_Text uiText;
    public int mapID;

    // Start is called before the first frame update
    void Start()
    {
        uiText = gameObject.GetComponent<TextMeshProUGUI>();
        mapID = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>().mapID;
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = "Stars: " + StaticScoreScript.starArray[mapID];
    }
}
