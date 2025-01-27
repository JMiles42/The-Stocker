﻿using ForestOfChaosLib;
using ForestOfChaosLib.AdvVar;
using ForestOfChaosLib.FoCsUI.Button;
using UnityEngine;

public class ScoreComponent: FoCsBehavior
{
	public ScoreManager Manger;
	public BoolVariable AllowKeyPress = false;
	public ButtonComponentBase Button;
	public BoolVariable EndGame;

	void Update()
	{
		if(AllowKeyPress && Input.GetKey(KeyCode.Space))
		{
			Manger.CalculateScore();
		}
	}

	private void OnEnable()
	{
		if(Button)
		{
			Button.onMouseClick += OnMouseClick;
		}
	}

	private void OnMouseClick()
	{
		Manger.CalculateScore();
		EndGame.Value = true;
	}

	private void OnDisable()
	{
		if(Button)
		{
			Button.onMouseClick -= OnMouseClick;
		}
	}
}