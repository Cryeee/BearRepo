using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerScript : MonoBehaviour
{
    public GameObject CMFreeLookCamera;
    public float AmountOfFoodEaten;

    public Animator fatAnimator;

    private AnimationClip fatteningAnimation;

    public float sizeIncrease = 0;

	public static bool inBallMode = false;

    // Reference to food display UI-script
    public UIFoodsEaten uiFoodsEaten;

    private Vector3 PlayerScaleSize;

    public GameObject bodyColldier;

    private void OnEnable()
    {
        GameController.OnGameStart += EnableCollider;
    }

    private void OnDisable()
    {
        GameController.OnGameStart -= EnableCollider;
    }

    private void EnableCollider()
    {
        bodyColldier.SetActive(true);
    }

    void Update()
    {
       // CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = 20;

       

        //PlayerScaleSize.Set(1 + AmountOfFoodEaten/10, 1 + AmountOfFoodEaten/10, 1 + AmountOfFoodEaten/10);
        //transform.localScale = PlayerScaleSize;


        if (Input.GetKeyDown(KeyCode.Q)) {
            //AmountOfFoodEaten++;
            //sizeIncrease = AmountOfFoodEaten / 100;
            Grow(10, null);
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
        else if (GetComponentInChildren<RollingMovement>() != null)
        {
            // If food item has grow amount of one, bear gets 1 unit fatter
            GetComponentInChildren<RollingMovement>().Fatten();
        }

        sizeIncrease = AmountOfFoodEaten / 100;

        // 1 means max fatness
        if(sizeIncrease <= 1)
        {
            fatAnimator.Play("Fattening", 2, sizeIncrease);
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = sizeIncrease * 5 + 5;
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = sizeIncrease * 7 + 7;
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = sizeIncrease * 5 + 5;
        }
       
        uiFoodsEaten.DisplayFoodItem(uiIcon);
    }

	public void TurnToBall(Vector3 currentPostion)
	{
		GameObject ball = transform.Find("pallokarhu").gameObject;
        GameObject skinny = transform.Find("BearSkinny").gameObject;
        ball.transform.position = currentPostion;
        ball.transform.rotation = skinny.transform.rotation;
        //ball.transform.localRotation = Quaternion.Euler(new Vector3(skinny.transform.rotation.x, skinny.transform.rotation.y, skinny.transform.rotation.z));
        ball.transform.Rotate(-90, 0, 0);
        ball.SetActive(true);
        inBallMode = true;
		CMFreeLookCamera.GetComponent<CinemachineFreeLook>().Follow = ball.transform;
		CMFreeLookCamera.GetComponent<CinemachineFreeLook>().LookAt = ball.transform;
	}

    
}
