using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class vbtn : MonoBehaviour, IVirtualButtonEventHandler {

	// Use this for initialization
	public GameObject vbtnObj;

	void Start () {

		vbtnObj = GameObject.Find ("virt");
		vbtnObj.GetComponent<VirtualButtonBehaviour> ().RegisterEventHandler (this);

	}

	public void OnButtonPressed (VirtualButtonBehaviour vb){

		Debug.Log ("Pressed!");

	}

	public void OnButtonReleased(VirtualButtonBehaviour vb){

		Debug.Log ("Released!");

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
