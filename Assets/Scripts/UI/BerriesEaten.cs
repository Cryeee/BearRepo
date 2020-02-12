using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BerriesEaten : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().text = "Berries Eaten: " + player.GetComponent<PlayerScript>().amountOfBerriesEaten;
    }
}
