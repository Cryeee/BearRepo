﻿using System.Collections;
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

	private Animator canvasAnimator;
	public bool canLevelUp;

	private void OnEnable()
	{
		GameController.OnGameEnd += ShowStars;
		GoldenBerry.OnPickedGoldenBerry += DisplayBerry;
	}

	private void OnDisable()
	{
		GameController.OnGameEnd -= ShowStars;
		GoldenBerry.OnPickedGoldenBerry -= DisplayBerry;
	}

	private void Awake()
	{
		// hakee animaattorin canvas objektista
		canvasAnimator = GetComponent<Animator>();
		canLevelUp = true;
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
		if (PlayerScript.AmountOfFoodEaten >= 30 && PlayerScript.AmountOfFoodEaten <= 35 && canLevelUp || PlayerScript.AmountOfFoodEaten >= 50 && canLevelUp)
		{
			canvasAnimator.SetTrigger("LevelUp");
			canLevelUp = false;
		}
		if (PlayerScript.AmountOfFoodEaten > 35 && PlayerScript.AmountOfFoodEaten < 40)
		{
			canLevelUp = true;
		}
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
}
