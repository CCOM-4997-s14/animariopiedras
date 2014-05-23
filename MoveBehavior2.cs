using UnityEngine;
using System.Collections;

public class MoveBehavior2 : MonoBehaviour {
	
	public float moveSpeed = 5.0f;
	public float jumpSpeed;
	public float gravity = 13.0f;
	public float posX = 0f;
	public float posY = 0f;
	
	public Vector3 mouseClickPos;
	public Vector2 tmpPos;
	
	Vector2 moveTowards;
	
	public bool isGrounded = true;
	private Transform groundCheck;
	public bool canJump = true;
	bool didPhase2Start = false;
	
	public SpriteRenderer myRenderer;

	private Animator animator;

	void Awake()
	{
		Physics2D.IgnoreLayerCollision(8,9);
		//Physics2D.IgnoreLayerCollision(8,12);
		//Physics2D.IgnoreLayerCollision(8,13);
		//Physics2D.IgnoreLayerCollision(8,14);
	}

	void Start ()
	{
		jumpSpeed = 375f;
		
		//playerSprites = Resources.LoadAll<Sprite>("TanSkinBaseSheet_strip16");
		myRenderer = gameObject.GetComponent<SpriteRenderer>();

		animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Controller.instance.phase == 1)
		{
			if(mouseClickPos.x == transform.position.x)
			{
				animator.SetBool("floating", false);
				//return; 
			}
			
			if (Input.GetMouseButton(0)) {
				mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				mouseClickPos.z = transform.position.z;
				
				animator.SetBool("floating", true);
				
				// Find the difference vector pointing from the weapon to the cursor
				Vector3 diff = mouseClickPos - transform.position;			
				// Always normalize the difference vector before using Atan2 function			
				diff.Normalize();
				
				// calculate the Z rotation angle using atan2			
				// Atan2 will give you an angle in radians, so you			
				// must use Rad2Deg constant to convert it to degrees			
				float rotZ = Mathf.Atan2(diff.y,diff.x) * Mathf.Rad2Deg;
				
				if (Vector3.Dot(Vector3.right, diff) < 0)
					rotZ += 180;
				
				// now assign the roation using Euler angle function			
				transform.rotation = Quaternion.Euler(0f,0f,rotZ);
			}
			
			if (transform.position != mouseClickPos)
				transform.position = Vector3.MoveTowards(transform.position, mouseClickPos, moveSpeed * Time.deltaTime);
		}
		else if (Controller.instance.phase == 2)
		{
			transform.localRotation = Quaternion.identity;
			
			if (didPhase2Start == false)
			{
				didPhase2Start = true;
				transform.position = new Vector2(this.transform.position.x, -4);
				animator.SetBool("floating", false);
				
				animator.SetInteger("phase", 2);
			}
			
			if(mouseClickPos.x == transform.position.x)
			{
				animator.SetBool("moving", false);
			}
			
			if (isGrounded)
			{
				
				if (Input.GetMouseButton(0))
				{
					mouseClickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					animator.SetBool("moving", true);
					if (mouseClickPos.x < transform.position.x)
					{
						Debug.Log("Move Left");
						
						//myRenderer.sprite = leftPlayer;
						//animator.SetInteger("Direction", -1);
						Vector3 newscale = new Vector3(-0.1f,0.1f,1.0f);
						transform.localScale = newscale;
					}
					else if (mouseClickPos.x > transform.position.x)
					{
						Debug.Log("Move Right"); 
						//myRenderer.sprite = rightPlayer;
						//animator.SetInteger("Direction", 1);
						Vector3 newscale = new Vector3(0.1f,0.1f,1.0f);
						transform.localScale = newscale;
						
					}
				}
				//transform.Translate(new Vector3(0, -gravity * Time.deltaTime, 0)); 
				
				transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), new Vector2(mouseClickPos.x, transform.position.y), (moveSpeed - 1) * Time.deltaTime);
			} 
		}
	}
	
	void Jump()
	{
		rigidbody2D.AddForce(new Vector2(0, jumpSpeed));
	}
	
	void OnCollisionEnter2D (Collision2D hit)
	{
		if (hit.gameObject.tag == "Road")
		{
			//Debug.Log("Hit the floor");
			isGrounded = true;
		}
	}
	
	void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.transform.tag == "PosObstacle")
		{
			//SoundManager sManager = soundController.GetComponent<SoundManager>();
			//SoundManager.instance.PlayAudio(sManager.hitSound);
			GameObject pos_obs = (GameObject)hit.gameObject;
			PositiveObstacles p_O = pos_obs.GetComponent<PositiveObstacles>();
			p_O.hitByPlayer = true;
			if(Controller.instance.phase == 2 && !p_O.hitByPlayerPhase2)
			{
				p_O.hitByPlayerPhase2 = true;
				Controller.instance.current_helpers++;
			}
		}
	}
}
