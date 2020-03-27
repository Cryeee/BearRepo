﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    public GameObject CMFreeLookCamera;
    public float AmountOfFoodEaten;

    private Animator playerAnimator;

    private AnimationClip fatteningAnimation;

    float sizeIncrease = 0;

    // Reference to food display UI-script
    public UIFoodsEaten uiFoodsEaten;

    private Vector3 PlayerScaleSize;
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
    // PickUp.cs calls this method
    public void Grow(float amount, Sprite uiIcon)
    {
        AmountOfFoodEaten += amount;

		// Fattens skinny bear if player is in skinny mode
		if(GetComponentInChildren<NormalMovement>() != null)
		{
			// If food item has grow amount of one, bear gets 1 unit fatter
			GetComponentInChildren<NormalMovement>().Fatten(amount / 10);
		}
		
        sizeIncrease = AmountOfFoodEaten / 100;
        CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = sizeIncrease * 5 + 5;
        CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = sizeIncrease * 7 + 7;
        CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = sizeIncrease * 5 + 5;
        uiFoodsEaten.DisplayFoodItem(uiIcon);
    }

	public void TurnToBall(Vector3 currentPostion)
	{
		GameObject ball = transform.Find("pallokarhu").gameObject;
		ball.transform.position = currentPostion;
		ball.SetActive(true);
		CMFreeLookCamera.GetComponent<CinemachineFreeLook>().Follow = ball.transform;
		CMFreeLookCamera.GetComponent<CinemachineFreeLook>().LookAt = ball.transform;
	}
    
}
