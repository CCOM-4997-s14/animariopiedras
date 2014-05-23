using UnityEngine;
using System.Collections;

public class PositiveObstacles : MonoBehaviour {

	public float moveSpeed;
	public byte object_link_id;
	public bool is_Human;
	public LayerMask layerMask;
	GameObject playerObject;

	[HideInInspector]
	public bool hitByPlayer;
	public bool hitByPlayerPhase2;
	public bool linked;
	public bool go_clean;
	public GameObject Obj_Clean;

	// Use this for initialization
	void Start () 
	{
		hitByPlayer = false;
		hitByPlayerPhase2 = false;
		linked = false;
		go_clean = false;
		moveSpeed = 4.5f;
		playerObject = GameObject.FindGameObjectWithTag("Player");
		Obj_Clean = null;
	}

	// Update is called once per frame
	void Update () 
	{
		if (hitByPlayer && !linked)
		{
			transform.position = Vector3.MoveTowards(transform.position, new Vector3(playerObject.transform.position.x,playerObject.transform.position.y,
			                                                                         12.0f),moveSpeed * Time.deltaTime);
		}

		if(is_Human && Controller.instance.phase == 2)
		{
			//Vector3 stuck_y = new Vector3(transform.position.x,-4.127533f,transform.position.z);
			//transform.position = stuck_y;
			if(hitByPlayerPhase2 && !go_clean)
			{
				transform.position = Vector3.MoveTowards(transform.position,
				                 	new Vector3(playerObject.transform.position.x,
				                    playerObject.transform.position.y,12.0f),moveSpeed * Time.deltaTime);
			}else if(hitByPlayerPhase2 && go_clean)
			{
				transform.position = Vector3.MoveTowards(transform.position,
				                                         new Vector3(Obj_Clean.transform.position.x+Random.Range(-2f,2f),
				            -4.127533f,12.0f),moveSpeed * Time.deltaTime);
			}
		}else if(is_Human && Controller.instance.phase == 2 && !hitByPlayer)
		{
			Destroy(gameObject);
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
