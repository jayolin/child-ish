using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour {

	// Use this for initialization
	public GameObject result, treatment_option, go_back;
	//Variable variableClass;

	void Start () {

		//variableClass = new Variable ();
		Debug.Log (Variable.diagnosedDisease);
		result.GetComponent<Text> ().text = Variable.diagnosedDisease;

	}
	
	// Update is called once per frame
	void Update () {

		go_back.GetComponent<Button> ().onClick.AddListener (GoBack);

	}

	void GoBack(){
	
		SceneManager.LoadScene ("Diagnosis", LoadSceneMode.Single);

	}
}
