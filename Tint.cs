using UnityEngine;
using System.Collections;

public class Tint : MonoBehaviour {

	Color obj_color;
	float alpha_value;
	SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer>();
		obj_color = sr.color;
		alpha_value = obj_color.a;
	}

	public void ChangeTint(float offset)
	{
		print (alpha_value);
		alpha_value -= (offset/255);
		print (alpha_value);
		obj_color.a = alpha_value;
		sr.color = obj_color;
	}
}
