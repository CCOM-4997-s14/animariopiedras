using UnityEngine;
using System.Collections;

public class levelNameBehavior : MonoBehaviour {

	public GameObject levelController;
	levelBehavior lController;

	// Use this for initialization
	void Start () 
	{
		lController = levelController.GetComponent<levelBehavior>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (lController.currentLevel == 1)
			this.guiText.text = "avenida universidad";
		if (lController.currentLevel == 2)
			this.guiText.text = "       paseo de diego";
		if (lController.currentLevel == 3)
			this.guiText.text = "plaza de convalecencia";
	}
}
