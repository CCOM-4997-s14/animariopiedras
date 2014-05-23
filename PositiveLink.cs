using UnityEngine;
using System.Collections;

public class PositiveLink : MonoBehaviour {

	public byte link_id;
	public bool human_link;
	public LayerMask layerMask;
	public GameObject humanito_obj;

	void Awake()
	{
		Physics2D.IgnoreLayerCollision(9,layerMask);
	}

	void OnTriggerEnter2D(Collider2D hit)
	{
		if(hit.transform.tag == "PosObstacle" && !human_link)
		{
			GameObject pos_obj = (GameObject)hit.gameObject;
			PositiveObstacles p_O = pos_obj.GetComponent<PositiveObstacles>();

			if(p_O.object_link_id == link_id)
			{
				//SoundManager sManager = soundController.GetComponent<SoundManager>();
				//SoundManager.instance.PlayAudio(sManager.correctSound);
				GameObject humanito_place = GameObject.Find("HumanitosPlace");
				for(int i = 0; i < 5; i++)
				{
					Vector3 newPos = new Vector3(hit.transform.position.x + Random.Range(-1.0f,1.0f), 
					                             hit.transform.position.y + Random.Range(-1.0f,2.5f));
					GameObject humanito = (GameObject)Instantiate(humanito_obj,newPos,Quaternion.identity);
					Humanitos h_obj = humanito.GetComponent<Humanitos>();
					h_obj.SetHumanitosPlace(humanito_place);
					Controller.instance.humanitos_count += 1;
				}

				Vector3 newPos2 = new Vector3(transform.position.x,transform.position.y,hit.transform.position.z);
				hit.transform.position = newPos2;
				hit.transform.localRotation = Quaternion.identity;
				p_O.linked = true;
				Controller.instance.linked_objects++;
				Controller.instance.health_of_city++;
				if(Controller.instance.linked_objects >= Controller.instance.number_of_linked_objects)
				{
					Controller.instance.GetToPhase2();
				}
				p_O.transform.collider2D.enabled = false;
				Destroy(gameObject);
			}
		}else if(hit.transform.tag == "PosObstacle" && human_link)
		{
			GameObject pos_obj = (GameObject)hit.gameObject;
			PositiveObstacles p_O = pos_obj.GetComponent<PositiveObstacles>();
			if(p_O.is_Human)
			{
				//SoundManager sManager = soundController.GetComponent<SoundManager>();
				//SoundManager.instance.PlayAudio(sManager.correctSound);
				GameObject humanito_place = GameObject.Find("HumanitosPlace");
				for(int i = 0; i < 5; i++)
				{
					Vector3 newPos = new Vector3(hit.transform.position.x + Random.Range(-1.0f,1.0f), 
					                             hit.transform.position.y + Random.Range(-1.0f,2.5f));
					GameObject humanito = (GameObject)Instantiate(humanito_obj,newPos,Quaternion.identity);
					Humanitos h_obj = humanito.GetComponent<Humanitos>();
					h_obj.SetHumanitosPlace(humanito_place);
					Controller.instance.humanitos_count += 1;
				}

				Vector3 newPos2 = new Vector3(transform.position.x,-4.127533f,hit.transform.position.z);
				hit.transform.position = newPos2;
				hit.transform.localRotation = Quaternion.identity;
				p_O.linked = true;
				Controller.instance.health_of_city++;
				Controller.instance.SetHumanoHelper(pos_obj);
				gameObject.collider2D.enabled = false;
			}
		}
	}
}