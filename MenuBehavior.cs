using UnityEngine;
using System.Collections;

public class MenuBehavior : MonoBehaviour {

	float displayTime = 0.0f;
	string message;
	bool displayMessage = false;

	//LevelButtonBehavior levelBehavior;
	public GameObject levelController;
	levelBehavior lController;

	// Use this for initialization
	void Start () 
	{
		//levelBehavior = Camera.main.GetComponent<LevelButtonBehavior>();
		lController = levelController.GetComponent<levelBehavior>();
		// Disable prevlevel button
	}

	void Update ()
	{
		displayTime -= Time.deltaTime;

		if (displayTime <= 0.0f)
			displayMessage = false;
	}
	
	// Update is called once per frame
	void OnMouseUpAsButton () 
	{
		if (this.gameObject.tag == "PlayAvenida")
		{
			Debug.Log("Clicked Play Level");
			// Supposed to go to video first
			Application.LoadLevel("AlphaScene");
		}
		else if (this.gameObject.tag == "LockedLevel")
		{
			Debug.Log("Clicked Locked Level");
			// Display Message that level is locked
			message = "You have to unlock this level first.";
			displayMessage = true;
			displayTime = 3.0f;
		}
		else if (this.gameObject.tag == "NextLevel")
		{
			Debug.Log("Clicked Next Level");
			lController.AddLevel();
			Camera.main.transform.Translate(new Vector3(7f, 0, 0));
		}
		else if (this.gameObject.tag == "PrevLevel")
		{
			Debug.Log("Clicked Prev Level");
			lController.SubtractLevel();
			Camera.main.transform.Translate(new Vector3(-7f, 0, 0));
		}
		else if (this.gameObject.tag == "Credits")
		{
			Debug.Log("Clicked Credits");
			Application.LoadLevel("Credits");
		}
		else if (this.gameObject.tag == "Settings")
		{
			Debug.Log("Clicked Settings - Not available at the moment.");
			message = "Settings are not available at the moment.";
			displayMessage = true;
			displayTime = 3.0f;
		}
	}

	void OnGUI ()
	{
		if (displayMessage) {
			GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2, 300f, 100f), message);
		}
	}
}
