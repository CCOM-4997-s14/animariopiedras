using UnityEngine;
using System.Collections;

public class IntroBehavior : MonoBehaviour {

	public GameObject soundController;
	SoundManager sManager;

	void Start ()
	{
		sManager = soundController.GetComponent<SoundManager>(); 
		sManager.PlayAudio(sManager.music1);
	}

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0))
		{
			Application.LoadLevel("MenuScreen");
		}
	}
}