using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerriesEaten : MonoBehaviour
{
    public GameObject player;

    TMP_Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        uiText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        uiText.text = "Berries Eaten: " + player.GetComponent<PlayerScript>().amountOfBerriesEaten;
    }
}
