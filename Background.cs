using UnityEngine;
using System.Collections;

public class Background : MonoBehaviour {

	public LayerMask layerMask;

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
