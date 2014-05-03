using UnityEngine;
using System.Collections;

public class CameraBehavior : MonoBehaviour {

	public GameObject playerObject;
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3(Mathf.Clamp(playerObject.transform.position.x, -33.0f, 32.0f), Mathf.Clamp(playerObject.transform.position.y, -0.7684f, 1.78f), -5); 
	}
}
