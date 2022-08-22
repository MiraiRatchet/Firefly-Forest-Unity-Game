using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class SettingsScript : MonoBehaviour
{

	public int level;

	public static SettingsScript instance;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}


	}

}
