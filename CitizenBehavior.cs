using UnityEngine;
using System.Collections;

public class CitizenBehavior : MonoBehaviour {

	public GameObject gameController;
	Controller controller;

	private Animator animator;

	// Use this for initialization
	void Start () 
	{
		controller = gameController.GetComponent<Controller>();
		animator = this.GetComponent<Animator>();

		animator.SetBool("floating", true);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
