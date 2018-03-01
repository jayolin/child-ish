using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class switchMode : MonoBehaviour {

	// Use this for initialization
	int vrMode;
	public Button thisButton;
	IEnumerable <IViewerParameters> viewerParams;
	MixedRealityController.Mode mrCurrentMode;

	void Start () {

		thisButton = GetComponent<Button> ();
		//canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		thisButton.onClick.AddListener (ToggleVRMode);
		viewerParams = Device.Instance.GetViewerList().GetAllViewers();
		mrCurrentMode = MixedRealityController.Mode.HANDHELD_AR;

	}

	public void ToggleVRMode(){
		
		if (mrCurrentMode == MixedRealityController.Mode.VIEWER_AR) {

			mrCurrentMode = MixedRealityController.Mode.HANDHELD_AR;
			MixedRealityController.Instance.SetMode (MixedRealityController.Mode.HANDHELD_AR);
			//canvas.enabled = true;
			//VuforiaARController.Instance.SetWorldCenter (VuforiaARController.WorldCenterMode.SPECIFIC_TARGET);
			vrMode = 0;

		}
		else if (mrCurrentMode == MixedRealityController.Mode.HANDHELD_AR) {

			mrCurrentMode = MixedRealityController.Mode.VIEWER_AR;
			foreach(IViewerParameters vp in viewerParams)
				if(vp.GetName().Equals("Generic Cardboard"))
					MixedRealityController.Instance.SetViewerParameters (vp);


			MixedRealityController.Instance.SetMode (MixedRealityController.Mode.VIEWER_AR);
			//canvas.enabled = false;
			vrMode = 1;

		}

		PlayerPrefs.SetInt ("Vr Mode", vrMode);
		Debug.Log(vrMode);

	}
	
	// Update is called once per frame

}
