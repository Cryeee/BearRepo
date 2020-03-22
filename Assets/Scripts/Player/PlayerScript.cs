using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    public GameObject CMFreeLookCamera;
    public float AmountOfFoodEaten;

    public Animator playerAnimator;

    public AnimationClip fatteningAnimation;

    float sizeIncrease = 0;

    // Reference to food display UI-script
    public UIFoodsEaten uiFoodsEaten;

    public Vector3 PlayerScaleSize;
    // Start is called before the first frame update
    void Start()
    {

        playerAnimator = gameObject.GetComponent<Animator>();
        //fatteningAnimation = playerAnimator.
    }

    // Update is called once per frame
    void Update()
    {
       // CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 20;

        //playerAnimator.Play("Fattening", 0, sizeIncrease);

        //PlayerScaleSize.Set(1 + AmountOfFoodEaten/10, 1 + AmountOfFoodEaten/10, 1 + AmountOfFoodEaten/10);
        //transform.localScale = PlayerScaleSize;


        if (Input.GetKeyDown(KeyCode.Q)) {
            //AmountOfFoodEaten++;
            //sizeIncrease = AmountOfFoodEaten / 100;
            Grow(1, null);
        }

        //transform.rotation.y = 
        //transform.rotation.y
    }

    // Grows player an amount and updates camera
    // TODO: Updates UI to display eaten food
    public void Grow(float amount, Sprite uiIcon)
    {
        AmountOfFoodEaten += amount;

		// If food item has grow amount of one, bear gets 1 unit fatter
		GetComponent<NormalMovement>().Fatten(amount / 10);

        sizeIncrease = AmountOfFoodEaten / 100;
        CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = sizeIncrease * 5 + 5;
        CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = sizeIncrease * 7 + 7;
        CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = sizeIncrease * 5 + 5;
        uiFoodsEaten.DisplayFoodItem(uiIcon);
    }
    
}
