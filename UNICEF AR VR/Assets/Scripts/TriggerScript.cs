using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Vuforia;




public class TriggerScript : MonoBehaviour {

	// Use this for initialization
	#region PUBLIC_MEMBER_VARIABLES
	public float activationTime = 1.5f;
	public bool Focused { get; set; }
	public GameObject Reticle;
	public GameObject modelContainer;
	public EventSystem eventSystem;
	#endregion // PUBLIC_MEMBER_VARIABLES

	Toggle symptomsToggle;


	#region PRIVATE_MEMBER_VARIABLES
	float mFocusedTime;
	bool mTriggered;
	ActionScript actionScript;
	Transform cameraTransform;
	Camera mainCamera;
	AnimationClass animationClass;
	int animationIndex = -1;


	GameObject model;
	Animator anim;
	Text title;

	//Variable
	Variable variables;

	#endregion // PRIVATE_MEMBER_VARIABLES


	private List<string> checkedSymptoms;




	void Start () {
		
		cameraTransform = Camera.main.transform;
		actionScript = gameObject.AddComponent<ActionScript>();
		mainCamera = Camera.main;
		eventSystem = EventSystem.current;


		//AnimationClass
		//variables = new Variable();
		AnimationClass.SceneNames sceneName = Variable.sceneName;

		//Game Objects init
		title = GameObject.Find ("title").GetComponent<Text>();
		title.text = Variable.title;


		//model and anim controller
		model = modelContainer.transform.Find(Variable.modelName).gameObject;
		model.SetActive (true);
		anim = model.GetComponent<Animator>();


		//init animation class
		animationClass = new AnimationClass(sceneName);


		//string array
		checkedSymptoms = new List<string>();

	}
	
	// Update is called once per frame
	void Update () {

		RaycastHit hit;
		Ray cameraGaze = new Ray(cameraTransform.position, cameraTransform.forward);
		Physics.Raycast(cameraGaze, out hit, Mathf.Infinity);
		//Focused = hit.transform.tag == "navButtons";

		PointerEventData pointerData = new PointerEventData (EventSystem.current);
		Vector3 vector = mainCamera.WorldToScreenPoint (Reticle.transform.position);
		vector.z = 0;
		pointerData.position = vector;

		//Debug.Log (vector+"vector");
		//Debug.Log (Input.mousePosition+"input");

		//Debug.Log (animationClass.animations[0].);

		List<RaycastResult> results = new List<RaycastResult> ();


		EventSystem.current.RaycastAll (pointerData, results);
		//if(results.Count > 0) Debug.Log (results[0].gameObject.name);

		Focused = (results.Count > 0) && (results [0].gameObject.tag == "navButtons" || results [0].gameObject.tag == "Toggle" || results [0].gameObject.tag == "submitButton");
		//Focused = hit.collider && (hit.collider.gameObject == (nextButton || previousButton));
		//if(Focused) Debug.Log("Is watching you");


		bool startAction = false || Input.GetMouseButtonUp(0);

		if (Focused || startAction)
		{

			if (mTriggered)
				return;

			// Update the "focused state" time
			mFocusedTime += Time.deltaTime;
			if ((mFocusedTime > activationTime) || startAction)
			{
				mTriggered = true;
				mFocusedTime = 0;

				string interactedObjectName = "", interactedObjectTag = "";

				if (Focused) {
					interactedObjectTag = results [0].gameObject.tag;
					interactedObjectName = results [0].gameObject.name;
					//Debug.Log (interactedObjectTag);
				} else {
					if (EventSystem.current.currentSelectedGameObject != null) {
						interactedObjectTag = EventSystem.current.currentSelectedGameObject.tag;
						interactedObjectName = EventSystem.current.currentSelectedGameObject.name;
						//Debug.Log (interactedObjectName);
					}
				}
				//Debug.Log (EventSystem.current);

				switch (interactedObjectTag) {

					case "navButtons":
						CurrentAnimationClass (interactedObjectName == "nextButton");
						break;
					
					case "Toggle":
							
						if (Focused)
							results [0].gameObject.GetComponent<Toggle> ().isOn = results [0].gameObject.GetComponent<Toggle> ().isOn ? false : true;
						//Debug.Log ("Works!!");
							toggleSymptom ();

						break;

					case "submitButton":
						submitSymptoms ();
						break;
					default:
						break;

				}

				/*if (interactedObjectName != "nextButton" || interactedObjectName != "previousButton")
					CurrentAnimationClass (interactedObjectName == "nextButton");
				else
					ResolveDiagnosis ();*/



			}
		}
		else
		{
			// Reset the "focused state" time
			mFocusedTime = 0;
			ResetVars();

		}

	}


	void OnGUI(){

		if (Focused && mFocusedTime>0) {
		
			GUI.Label(new Rect (50, 50, 200, 25), "Clicking in "+mFocusedTime);
		
		}

	}


	private void ResetVars()
	{
		// Reset variables
		mTriggered = false;
		mFocusedTime = 0;
		Focused = false;
	}

	private void CurrentAnimationClass(bool direction){

		if (direction) {//next button

			animationIndex = animationIndex + 1 >= animationClass.animations.Count ? 0 :animationIndex + 1;

		} else {//previous button

			animationIndex = animationIndex - 1 < 0 ? animationClass.animations.Count - 1 :animationIndex - 1; 

		}


		actionScript.Action (animationClass, animationIndex, anim);

	}

	private bool IsOnSymptomsToggle(){

		return GameObject.Find ("toggle-" + animationIndex).GetComponent<Toggle> ().isOn;
	
	}


	private void toggleSymptom(){

		if (IsOnSymptomsToggle ())
			checkedSymptoms.Add (animationClass.info [animationIndex>-1?animationIndex:0]);
		else 
			checkedSymptoms.Remove (animationClass.info [animationIndex]);
		Debug.Log(checkedSymptoms.Count);


	}

	private void submitSymptoms(){

		//call to php then to python...
		//ConnectToPredictionServer ctps = new ConnectToPredictionServer(checkedSymptoms);
		PlayerPrefs.SetString("symptoms", string.Join("##", checkedSymptoms.ToArray()));
		ConnectToPredictionServer ctps = gameObject.AddComponent<ConnectToPredictionServer>() as ConnectToPredictionServer;
		//ctps.ConnectToServer ();

	}

}