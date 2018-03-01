using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionScript : MonoBehaviour {

	#region GAME_OBJECTS
	Text description;
	#endregion // PRIVATE_MEMBER_VARIABLES


	// Use this for initialization
	void Start () {

		description = GameObject.Find ("description").GetComponent<Text>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Action(AnimationClass animationClass, int animationIndex, Animator anim){

		//Debug.Log(animationIndex);
		anim.Play (animationClass.animations [animationIndex].animation);

		description.text = animationClass.info [animationIndex];



		GameObject symptomToggle;
		GameObject[] gameObjects;
		if (GameObject.FindGameObjectsWithTag("Toggle").Length > 0) {

			gameObjects = GameObject.FindGameObjectsWithTag ("Toggle");
			for (int i = 0; i < gameObjects.Length; i++) {
				//Debug.Log (i);
				gameObjects[i].GetComponent<CanvasGroup> ().alpha = 0;
				gameObjects[i].GetComponent<CanvasGroup> ().interactable = false;
				gameObjects[i].GetComponent<CanvasGroup> ().blocksRaycasts = false;
			
			}

			symptomToggle = GameObject.Find ("toggle-"+animationIndex);
			symptomToggle.GetComponent<CanvasGroup> ().alpha = 1;
			symptomToggle.GetComponent<CanvasGroup> ().interactable = true;
			symptomToggle.GetComponent<CanvasGroup> ().blocksRaycasts = true;

		}



	}
}
