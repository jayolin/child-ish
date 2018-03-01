using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OpeningScript : MonoBehaviour {

	// Use this for initialization
	public GameObject diagnosis, maternity, settings, exit;
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {


		diagnosis.GetComponent<Button> ().onClick.AddListener (delegate {
			HandleClick(diagnosis.GetComponent<Button> ());
		});


		maternity.GetComponent<Button> ().onClick.AddListener (delegate {
			HandleClick(maternity.GetComponent<Button> ());
		});


		settings.GetComponent<Button> ().onClick.AddListener (delegate {
			HandleClick(settings.GetComponent<Button> ());
		});


		exit.GetComponent<Button> ().onClick.AddListener (delegate {
			HandleClick(exit.GetComponent<Button> ());
		});

	}

	void HandleClick(Button button){

		switch(button.name){

		case "diagnosis":
			Variable.sceneName = AnimationClass.SceneNames.DIAGNOSIS;
			Variable.title = "Diagnosis";
			Variable.modelName = "maternity_model";
			SceneManager.LoadScene ("Diagnosis", LoadSceneMode.Single);

			break;

			case "maternity":
				Variable.sceneName = AnimationClass.SceneNames.MATERNITY;
				Variable.title = "Maternity";
				Variable.modelName = "maternity_model";
				SceneManager.LoadScene ("Maternity", LoadSceneMode.Single);

			break;

			case "settings":
			break;

			case "exit":
				Application.Quit ();
			break;

			default:
			break;

		}
	}
}
