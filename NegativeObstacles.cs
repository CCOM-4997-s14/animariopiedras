using UnityEngine;
using System.Collections;

public class NegativeObstacles : MonoBehaviour {


	public float scale_value;
	public LayerMask layerMask;
	public Sprite garbageMark;

	[HideInInspector]
	public bool object_clicked;

	// Use this for initialization
	void Start () {
		object_clicked = false;
		scale_value = 1.5f;
	}

	void Awake()
	{
		Physics2D.IgnoreLayerCollision(10,9);
	}

	// Update is called once per frame
	void Update () {
		if(Timer.instance.timeOver)
		{
			Vector3 lockPos = transform.position;
			rigidbody2D.gravityScale = 0;
			collider2D.isTrigger = enabled;
			SpriteRenderer s = gameObject.GetComponent<SpriteRenderer>();
			s.sprite = garbageMark;
			transform.position = lockPos;
		}
	}

	void OnMouseDown()
	{
		if(Controller.instance.phase >= 2)
		{
			RaycastHit2D hitInfo = new RaycastHit2D();
			hitInfo = Physics2D.Raycast(Input.mousePosition, Vector2.zero, 1000, 1<<layerMask);
			if(hitInfo != null)
			{
				if(!object_clicked && hitInfo.collider == transform.collider)
				{
					Vector3 newButtonPos = new Vector3(transform.position.x,transform.position.y,-5);
					ContextMessageManager.instance.SpawnButton_Clean(newButtonPos,gameObject);
					object_clicked = true;
					transform.localScale *= scale_value;
					Controller.instance.hit_neg_obj = true;
					Controller.instance.ChangeLastObjectClicked(gameObject);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D hit)
	{
		if(hit.transform.tag == "Player") 
		{
			transform.rigidbody2D.AddForce((transform.position - hit.transform.position).normalized
			                               * 200);
		}else if(hit.transform.tag == "NegObstacle")
		{
			transform.rigidbody2D.AddForce((transform.position - hit.transform.position).normalized
			                               * 50);
			hit.transform.rigidbody2D.AddForce(-(transform.position - hit.transform.position).normalized
			                               * 50);
		}else if(hit.transform.tag == "Road")
		{
			transform.rigidbody2D.AddForce((transform.position - hit.transform.position).normalized
			                               * 50);
		}
	}

	public void StartCleaning()
	{
		StartCoroutine(GetCleaned());
	}

	IEnumerator GetCleaned()
	{
		float scale_multiplier_by_helpers = 0.9f - (Controller.instance.current_helpers * 0.1f);
		float time_multiplier_by_helpers = 1.0f / (Controller.instance.current_helpers + 1);

		for(int i = 0; i < 5; i++)
		{
			transform.localScale *= scale_multiplier_by_helpers;

			yield return new WaitForSeconds(time_multiplier_by_helpers);
		}

		Controller.instance.SetHumanosHelperClean(false);
		Controller.instance.cleaning = false;
		Destroy (gameObject);
	}
}
