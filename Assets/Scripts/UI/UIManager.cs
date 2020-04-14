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

	private void OnEnable()
	{
		GameController.OnGameEnd += ShowStars;
	}

	private void OnDisable()
	{
		GameController.OnGameEnd -= ShowStars;
	}

	private void Update()
	{
		if (GameController.gameOn)
		{
			foodEatenText.text = "Food Eaten: " + PlayerScript.AmountOfFoodEaten;
			timeText.text = TimeController.roundTime.ToString("F2");
		}
	}

	private void ShowStars()
	{
		//endScoreText.text = ""
	}
}
