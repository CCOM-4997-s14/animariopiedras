using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {

	public static Timer instance;

	float timer;

	[HideInInspector]
	public bool timeOver;

	// Use this for initialization
	void Start () {
		timer = 60.0f;
		timeOver = false;
	}

	void Awake()
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(Controller.instance.phase == 2)
		{
			guiText.enabled = true;
			if(timer >= 0)
			{
				timer -= Time.deltaTime;
			}else
			{
				timeOver = true;
				timer = 0;
			}
			guiText.text = timer.ToString("F0");
		}
	}
}
