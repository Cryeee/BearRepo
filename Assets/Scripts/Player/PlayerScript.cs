using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float amountOfBerriesEaten;

    public GameObject playerPrefab;

    public Vector3 PlayerScaleSize;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerScaleSize.Set(1 + amountOfBerriesEaten/10, 1 + amountOfBerriesEaten/10, 1 + amountOfBerriesEaten/10);
        transform.localScale = PlayerScaleSize;


        if(Input.GetKeyDown(KeyCode.Q)) {
            amountOfBerriesEaten++;
        }
    }
    void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "PickUpable") {
            print("collided with: " + collision);
        }
    }
}
