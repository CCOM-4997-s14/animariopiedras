using UnityEngine;
using System.Collections;

public class ContextMessageManager : MonoBehaviour {

	public static ContextMessageManager instance;
	public GameObject clean_button;
	GameObject clickedObj;

	//Button buffers
	GameObject clean;

	// Use this for initialization
	void Start () {
		clean = (GameObject)Instantiate(clean_button,new Vector3(-10,-10,-5),Quaternion.identity);
	}

	void Awake()
	{
		instance = this;
	}

	public void SpawnButton_Clean(Vector3 pos, GameObject obj)
	{
		pos = new Vector3(pos.x,pos.y+1,pos.z);
		clean.transform.position = pos;
		clickedObj = obj;
	}

	public void DeSpawnButton_Clean()
	{
		Vector3 newPos = new Vector3(-10,-10,-5);
		clean.transform.position = newPos;
		clickedObj = null;
	}

	public GameObject GetClickedObj()
	{
		return clickedObj;
	}
}
