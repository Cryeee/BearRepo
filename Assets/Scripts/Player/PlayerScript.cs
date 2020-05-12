using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class PlayerScript : MonoBehaviour
{
    public GameObject CMFreeLookCamera;
    public static float AmountOfFoodEaten = 0;

    [Tooltip("Paljonko pitää syödä että muuttuu palloks? 1 = 1 marja")]
    public int amountFoodToBallMode = 30;
    public Animator fatAnimator;

    public float sizeIncrease = 0;

	public static bool inBallMode = false;

    // Reference to food display UI-script
    public UIFoodsEaten uiFoodsEaten;

    public GameObject bodyColldier;
    private float tmp = 0;

    public static Action<float> OnGrow;

    private void OnEnable()
    {
        GameController.OnGameStart += EnableCollider;
    }

    private void OnDisable()
    {
        GameController.OnGameStart -= EnableCollider;
    }
    private void Awake()
    {
        AmountOfFoodEaten = 0;
    }

    private void Start()
    {
       
        inBallMode = false;
    }

    private void EnableCollider()
    {
        bodyColldier.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            Grow(10, null);
        }
    }

    // Grows player an amount and updates camera
    // PickUp.cs calls this method
    public void Grow(float amount, Sprite uiIcon)
    {
        AmountOfFoodEaten += amount;

        // Set new weight goal when bear gets fat enough for a new star
        if(AmountOfFoodEaten >= GameController.nextWeightGoal && GameController.stars < 3)
        {
            GameController.SetNewWeightGoal(AmountOfFoodEaten);
        }

        if(inBallMode)
        {
            tmp += amount;
            sizeIncrease = tmp / 100;
        }

		// Fattens skinny bear if player is in skinny mode
		if(GetComponentInChildren<NormalMovement>() != null)
		{
			// If food item has grow amount of one, bear gets 1/30 unit fatter
			GetComponentInChildren<NormalMovement>().Fatten(amount / amountFoodToBallMode);
		}
        else if (GetComponentInChildren<RollingMovement>() != null)
        {
            // If food item has grow amount of one, bear gets 1 unit fatter
            GetComponentInChildren<RollingMovement>().Fatten();
        }

        

        // 1 means max fatness
        if(sizeIncrease <= 1)
        {
            fatAnimator.Play("Fattening", 2, sizeIncrease);
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[0].m_Radius = sizeIncrease * 5 + 5;
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[1].m_Radius = sizeIncrease * 7 + 7;
            CMFreeLookCamera.GetComponent<CinemachineFreeLook>().m_Orbits[2].m_Radius = sizeIncrease * 5 + 5;
        }
       
        uiFoodsEaten.DisplayFoodItem(uiIcon);
        OnGrow?.Invoke(amount);
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
