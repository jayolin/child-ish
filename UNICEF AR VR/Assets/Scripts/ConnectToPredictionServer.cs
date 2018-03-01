using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ConnectToPredictionServer: MonoBehaviour
{

	//string url = "http://127.0.0.1/predict/index.php";
	string url = "http://suenuguregion.com.ng/predict/index.php";

	void Start(){

		WWWForm form = new WWWForm ();
		form.AddField ("symptoms", PlayerPrefs.GetString("symptoms").Replace(" ", "-"));
		Debug.Log ( PlayerPrefs.GetString("symptoms").Replace(" ", "-"));
		WWW www = new WWW (url, form);

		StartCoroutine (WaitForRequest (www));
		Debug.Log ("Shii!!");

	}

	//public ConnectToPredictionServer (List<String> Symptoms)
	//{
		

	//}



	IEnumerator WaitForRequest(WWW www){

		yield return www;
		if (www.error == null) {

			Debug.Log (www.text);
			Variable.diagnosedDisease = www.text;
			SceneManager.LoadScene ("Result", LoadSceneMode.Single);

		} else {

			Debug.Log (www.error);

		}

	}
}

