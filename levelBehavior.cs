using UnityEngine;
using System.Collections;

public class levelBehavior : MonoBehaviour {

	public int currentLevel = 0;

	public GameObject nextLevel;
	public GameObject prevLevel;

	// Use this for initialization
	void Start () 
	{
		currentLevel = 1;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (currentLevel == 1)
			DisablePrevButton();
		else
			ActivatePrevButton();

		if (currentLevel == 3)
			DisableNextButton();
		else
			ActivateNextButton();
	}

	public void DisablePrevButton ()
	{
		prevLevel.SetActive(false);
	}
	
	public void ActivatePrevButton ()
	{
		prevLevel.SetActive(true);
	}

	public void DisableNextButton ()
	{
		nextLevel.SetActive(false);
	}

	public void ActivateNextButton ()
	{
		nextLevel.SetActive(true);
	}

	public void AddLevel ()
	{
		currentLevel += 1;
	}

	public void SubtractLevel ()
	{
		currentLevel -= 1;
	}
}
