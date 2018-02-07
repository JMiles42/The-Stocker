﻿using JMiles42.Systems.MenuManager;
using UnityEngine;
using UnityEngine.UI;

public class LoadingMenu: SimpleMenu<LoadingMenu>
{
	public GameObject Spinner;
	public Image LoadingBar;

	public float LoadingBarFill
	{
		get { return LoadingBar.fillAmount; }
		set
		{
			LoadingBar.gameObject.SetActive(value <= 0);
			LoadingBar.fillAmount = value;
		}
	}

	public override void OnEnable()
	{
		base.OnEnable();
	}

	public override void OnDisable()
	{
		base.OnDisable();
	}
}