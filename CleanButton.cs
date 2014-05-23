using UnityEngine;
using System.Collections;

public class CleanButton : MonoBehaviour {

	GameObject objClicked;

	void Awake()
	{
		Physics2D.IgnoreLayerCollision(13,10);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		RaycastHit2D hitInfo = new RaycastHit2D();
		hitInfo = Physics2D.Raycast(Input.mousePosition, Vector2.zero, 100);
		if(hitInfo != null)
		{
			if(hitInfo.collider == transform.collider && !Controller.instance.cleaning)
			{
				objClicked = ContextMessageManager.instance.GetClickedObj();
				ContextMessageManager.instance.DeSpawnButton_Clean();
				Controller.instance.CleanObject();
			}
		}
	}
}
