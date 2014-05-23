using UnityEngine;
using System.Collections;

public class Humanitos : MonoBehaviour {

	GameObject Humanitos_Place;
	GameObject CleanTarget;
	public float moveSpeed;
	public bool clean = false;
	// Update is called once per frame
	void Update () {
		if(clean)
		{
			transform.position = Vector3.MoveTowards(transform.position, CleanTarget.transform.position,
			                                         moveSpeed * Time.deltaTime);
		}
		else{
			transform.position = Vector3.MoveTowards(transform.position, Humanitos_Place.transform.position,
		    	                                     moveSpeed * Time.deltaTime);
		}
	}

	public void SetHumanitosPlace(GameObject huma)
	{
		Humanitos_Place = huma;
	}

	public void SetCleanTarget(GameObject clean_obj)
	{
		clean = true;
		CleanTarget = clean_obj;
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.transform.tag == "HumanitosPlace" && !clean)
		{
			Destroy(gameObject);
		}
	}
}
