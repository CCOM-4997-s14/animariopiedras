using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = new Vector3(player.transform.position.x, transform.position.y,transform.position.z);
		transform.position = newPos;
	}
}
