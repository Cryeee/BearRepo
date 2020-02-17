using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    TMP_Text uiText;
    // Start is called before the first frame update
    void Start()
    {
        uiText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimeController.roundTime <= GameController.roundTimeLimit)
        {
            uiText.text = TimeController.roundTime.ToString("F2") + " / " + GameController.roundTimeLimit;
        }
        
    }
}
