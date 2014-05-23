using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

	public static Controller instance;

	public GameObject NegativeObjects;
	public GameObject PositiveObjects;
	public GameObject ChangingBackgrounds;
	public GameObject PosLinkHumanos;
	public GameObject humanito_obj;
	public GameObject player;
	Rigidbody2D[] neg_rigid;
	BoxCollider2D[] poslink_humans;
	SpriteRenderer[] pos_objects;
	SpriteRenderer[] changing_bg;
	GameObject LastNegObj;
	public GameObject Tint_bg;
	Tint tint;
	float internal_timer_phase1;

	[HideInInspector]
	public byte linked_objects;
	public byte number_of_linked_objects;
	public bool hit_neg_obj;
	public byte phase;
	public ushort humanitos_count;
	public bool destroy_humanitos_after_clean;
	public GameObject[] PosHumanosHelpers;
	public int poshumanoshelpers_index;
	public bool cleaning;
	public byte current_helpers = 0;
	int background_index;
	public byte health_of_city;

	// Use this for initialization
	void Start () {
		neg_rigid = NegativeObjects.GetComponentsInChildren<Rigidbody2D>();
		pos_objects = PositiveObjects.GetComponentsInChildren<SpriteRenderer>();
		poslink_humans = PosLinkHumanos.GetComponentsInChildren<BoxCollider2D>();
		changing_bg = ChangingBackgrounds.GetComponentsInChildren<SpriteRenderer>();
		tint = Tint_bg.GetComponent<Tint>();
		linked_objects = 0;
		phase = 1;
		hit_neg_obj = false;
		destroy_humanitos_after_clean = false;
		humanitos_count = 0;
		PosHumanosHelpers = new GameObject[12];
		poshumanoshelpers_index = 0;
		background_index = 2;
		cleaning = false;
		internal_timer_phase1 = 0f;
		health_of_city = 0;
		DetectNumberOfPosObjects();
	}

	void Awake()
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if(phase == 1)
		{
			internal_timer_phase1 += Time.deltaTime;
			if(internal_timer_phase1 >= 20.0f)
			{
				internal_timer_phase1 = 0f;
				ChangeBackgroundsNegative();
			}
			if(health_of_city == 3)
			{
				health_of_city = 0;
				ChangeBackgroundsPositive();
			}
		}
	}

	void ChangeBackgroundsPositive()
	{
		background_index++;
		changing_bg[background_index].renderer.enabled = true;
		tint.ChangeTint(25.0f);
	}

	void ChangeBackgroundsNegative()
	{
		changing_bg[background_index].renderer.enabled = false;
		background_index--;
		tint.ChangeTint(-25.0f);
	}

	void DetectNumberOfPosObjects()
	{
		foreach(SpriteRenderer s in pos_objects)
		{
			number_of_linked_objects++;
		}
	}

	public void GetToPhase2()
	{
		foreach(Rigidbody2D r in neg_rigid)
		{
			r.rigidbody2D.gravityScale = 1;
			phase = 2;
		}
		foreach(BoxCollider2D b in poslink_humans)
		{
			b.enabled = false;
		}
		player.rigidbody2D.gravityScale = 1;
		changing_bg[4].renderer.enabled = true;
		Destroy(Tint_bg);
	}

	public void CleanObject()
	{
		GameObject tempNeg = LastNegObj.gameObject;
		/*GameObject[] humanito_list = new GameObject[3];
		for(int i = 0; i < 3; i++)
		{
			Vector3 newPos = new Vector3(transform.position.x + Random.Range(-1.0f,1.0f), 
			                             transform.position.y + Random.Range(-1.0f,2.5f));
			humanito_list[i] = (GameObject)Instantiate(humanito_obj,newPos,Quaternion.identity);
			humanito_list[i].GetComponent<Humanitos>().SetCleanTarget(LastNegObj);
			humanitos_count -= 1;
		}*/
		SetObjectToHumanosHelper(tempNeg);
		cleaning = true;
		tempNeg.GetComponent<NegativeObstacles>().StartCleaning();
		SetHumanosHelperClean(true);
		ChangeLastObjectClicked();
	}

	void DestroyHumanitosAfterClean(GameObject[] h)
	{
		foreach (GameObject g in h)
		{
			Destroy(g);
		}
	}

	public void SetHumanoHelper(GameObject humano)
	{
		PosHumanosHelpers[poshumanoshelpers_index] = humano;
		poshumanoshelpers_index++;
	}

	public void SetHumanosHelperClean(bool b)
	{
		for(int i = 0; i < poshumanoshelpers_index; i++)
		{
			PosHumanosHelpers[i].GetComponent<PositiveObstacles>().go_clean = b;
		}
	}

	public void SetObjectToHumanosHelper(GameObject obj)
	{
		for(int i = 0; i < poshumanoshelpers_index; i++)
		{
			PosHumanosHelpers[i].GetComponent<PositiveObstacles>().Obj_Clean = obj;
		}
	}

	#region Negative Object Functions
	void LastObjectClicked(GameObject obj)
	{
		LastNegObj = obj;
	}	

	public void ChangeLastObjectClicked()
	{
		if(LastNegObj != null)
		{
			NegativeObstacles tempComp = LastNegObj.GetComponent<NegativeObstacles>();
			tempComp.object_clicked = false;
			LastNegObj.transform.localScale /= tempComp.scale_value;
			hit_neg_obj = false;
			ContextMessageManager.instance.DeSpawnButton_Clean();
			LastNegObj = null;
		}
	}

	public void ChangeLastObjectClicked(GameObject obj)
	{
		if(LastNegObj != null)
		{
			NegativeObstacles tempComp = LastNegObj.GetComponent<NegativeObstacles>();
			tempComp.object_clicked = false;
			LastNegObj.transform.localScale /= tempComp.scale_value;
			hit_neg_obj = false;
		}
		LastObjectClicked(obj);
	}
	#endregion

}
