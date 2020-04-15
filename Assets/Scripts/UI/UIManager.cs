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

	private Animator canvasAnimator;
	public bool canLevelUp;

	private void OnEnable()
	{
		GameController.OnGameEnd += ShowStars;
	}

	private void OnDisable()
	{
		GameController.OnGameEnd -= ShowStars;
	}

	private void Awake()
	{
		// hakee animaattorin canvas objektista
		canvasAnimator = GetComponent<Animator>();
	}

	private void Update()
	{
		// näyttää syödyn määrän ja ajan
		if (GameController.gameOn)
		{
			foodEatenText.text = PlayerScript.AmountOfFoodEaten + " / " + GameController.targetFoodAmountValue + " kg";
			timeText.text = TimeController.roundTime.ToString("F2");
		}

		// UI Bounce 
		if (PlayerScript.AmountOfFoodEaten >= 11 && PlayerScript.AmountOfFoodEaten <= 20 && canLevelUp || PlayerScript.AmountOfFoodEaten >= 50 && canLevelUp)
		{
			canvasAnimator.SetTrigger("LevelUp");
			canLevelUp = false;
		}
		if (PlayerScript.AmountOfFoodEaten > 20 && PlayerScript.AmountOfFoodEaten < 40)
		{
			canLevelUp = true;
		}
	}

	private void ShowStars()
	{
		endScoreText.text = "Stars earned: " + GameController.stars.ToString();
		endScoreText.gameObject.SetActive(true);
	}
}
