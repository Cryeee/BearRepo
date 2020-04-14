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

	private void Update()
	{
		if (GameController.gameOn)
		{
			foodEatenText.text = "Food Eaten: " + PlayerScript.AmountOfFoodEaten + " / " + GameController.targetFoodAmountValue;
			timeText.text = TimeController.roundTime.ToString("F2");
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
