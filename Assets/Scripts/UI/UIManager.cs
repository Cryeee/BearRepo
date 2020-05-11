using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	public TMP_Text endScoreText;
	public TMP_Text foodEatenText;
	public TMP_Text timeText;
	public Image goldenBerry;

	public RectTransform bodyImage;
	public Slider timeSlider;

	private Animator canvasAnimator;
	public bool canLevelUp;


	private void OnEnable()
	{
		GameController.OnGameEnd += ShowStars;
		GoldenBerry.OnPickedGoldenBerry += DisplayBerry;
		GameController.OnGameStart += DisplayUI;
		PlayerScript.OnGrow += GrowUI;

	}

	private void OnDisable()
	{
		GameController.OnGameStart -= DisplayUI;
		GameController.OnGameEnd -= ShowStars;
		GoldenBerry.OnPickedGoldenBerry -= DisplayBerry;
		PlayerScript.OnGrow -= GrowUI;
	}

	private void Awake()
	{
		// hakee animaattorin canvas objektista
		canvasAnimator = GetComponent<Animator>();
		canLevelUp = true;
	}

	private void Start()
	{
		//fatnessUI.SetActive(false);
		//timeSlider.gameObject.SetActive(false);
	}

	private void Update()
	{
		// näyttää syödyn määrän ja ajan
		if (GameController.gameOn)
		{
			foodEatenText.text = PlayerScript.AmountOfFoodEaten + " kg" + " / " + GameController.nextWeightGoal + " kg";
			//timeText.text = TimeController.currentTime.ToString("F2");

			timeSlider.value = TimeController.currentTime / TimeController.startTime;
		} else
		{
			//timeText.text = "0.00";
		}

		// UI Bounce 
		if (PlayerScript.AmountOfFoodEaten >= 30 && PlayerScript.AmountOfFoodEaten <= 35 && canLevelUp || PlayerScript.AmountOfFoodEaten >= 50 && canLevelUp)
		{
			canvasAnimator.SetTrigger("LevelUp");
			canLevelUp = false;
		}
		if (PlayerScript.AmountOfFoodEaten > 35 && PlayerScript.AmountOfFoodEaten < 40)
		{
			canLevelUp = true;
		}

        //whiteout
        if (TimeController.currentTime <= 0)
            gameObject.GetComponent<Animator>().Play("whiteout", 0);
    }

	private void ShowStars()
	{
		endScoreText.text = "Stars earned: " + GameController.stars.ToString();
		endScoreText.gameObject.SetActive(true);
	}

	private void DisplayBerry()
	{
		goldenBerry.gameObject.SetActive(true);
	}

	private void GrowUI(float amount)
	{
		float growAmount;
		int weightGoal;
		int prewWeight;

		switch (GameController.stars)
		{
			case 0:
				growAmount = 0.6f;
				weightGoal = GameController.weight1;
				prewWeight = 0;
					break;
			case 1:
				growAmount = 0.8f;
				weightGoal = GameController.weight2;
				prewWeight = GameController.weight1;
				break;
			case 2:
				growAmount = 0.7f;
				weightGoal = GameController.weight3;
				prewWeight = GameController.weight2;
				break;
			default: 
				growAmount = 0.6f;
				weightGoal = GameController.weight1;
				prewWeight = 0;
				break;
		}

		if(GameController.stars < 3)
		{
			Vector3 current = bodyImage.localScale;
			current.x += growAmount / (weightGoal - prewWeight) * amount;
			current.y += growAmount / (weightGoal - prewWeight) * amount;

			//bodyImage.localScale = new Vector3(current.x, current.y, 1);
			bodyImage.localScale = current;
		}
		
	}

	private void DisplayUI()
	{
		//fatnessUI.SetActive(true);
		//timeSlider.gameObject.SetActive(true);
	}
}
