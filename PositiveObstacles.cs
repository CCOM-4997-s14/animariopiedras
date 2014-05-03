using UnityEngine;
using System.Collections;

public class PositiveObstacles : MonoBehaviour {

	public float moveSpeed;
	public byte object_link_id;
	public LayerMask layerMask;
	GameObject playerObject;

	[HideInInspector]
	public bool hitByPlayer;
	public bool linked;
	
	// Use this for initialization
	void Start () 
	{
		hitByPlayer = false;
		linked = false;
		moveSpeed = 4.5f;
		playerObject = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () 
	{
		if (hitByPlayer == true && linked == false)
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerObject.transform.position.x,playerObject.transform.position.y,
			                                                                         12.0f),moveSpeed * Time.deltaTime);
		}
	}

	void OnMouseDown()
	{
		if(Controller.instance.phase >= 2)
		{
			RaycastHit2D hitInfo = new RaycastHit2D();
			hitInfo = Physics2D.Raycast(Input.mousePosition, Vector2.zero, 100, 1<<layerMask);
			if(hitInfo != null)
			{
				if(hitInfo.collider == transform.collider)
				{
					Controller.instance.ChangeLastObjectClicked();
				}
			}
		}
	}
}
