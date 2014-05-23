using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public GameObject playerObject;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(Mathf.Clamp(playerObject.transform.position.x, -68.5f, 72.5f),
		                                 Mathf.Clamp(playerObject.transform.position.y,-0.7684f,9.5f), -20.0f); 
	}
}
