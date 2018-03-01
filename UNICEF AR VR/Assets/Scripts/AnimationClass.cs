using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Reflection;

public class AnimationGroup{

	public  string animation;
	public enum types{
		
		PROBLEM,
		SOLUTION
	}

	public types AnimeType;

	public AnimationGroup(string anim){

		animation = anim;

		if (anim.Contains ("@prob_")) {
			AnimeType = types.PROBLEM;
		} else {
			AnimeType = types.SOLUTION;
		}

	}

}


public class AnimationClass {

	public List <AnimationGroup> animations;
	public List<string> info;

	public GameObject togglePrefab;
	public GameObject infoCanvas;

	public enum SceneNames{
		MATERNITY,
		DIAGNOSIS
	};

	public AnimationClass(SceneNames SceneName){
		
		togglePrefab = Resources.Load("Prefabs/togglePrefab") as GameObject;
		infoCanvas = GameObject.Find ("info canvas");

		switch (SceneName) {
			
		case SceneNames.MATERNITY:
			
			animations = new List<AnimationGroup> () {

				//Problems
				new AnimationGroup ("@prob_1"),
				new AnimationGroup ("@prob_2"),

				//Solutions

			};
			info = new List<string> () {
				//Problems
				"This is some weird shit that might happen to you and you could lose your life!!",
				"This is the follow up! This is how you know you've lost your life and are now a vegetable! Sorry bro.",

				//Solutions

			};
			break;	

		//Diagnosis
		case SceneNames.DIAGNOSIS:

			animations = new List<AnimationGroup> ();
			info = new List<string> ();

			//using (var stream = Assembly.GetExecutingAssembly ().GetManifestResourceStream ("Assets.Resources.DataSets.dataset_clean1.csv"))
			using (var reader = new StreamReader ("Assets/Resources/DataSets/dataset_clean1.csv")) {

				int index = 0;
				while (!reader.EndOfStream) {
					var line = reader.ReadLine ();
					var values = line.Split(',');
					//Debug.Log (values);

					if(info.IndexOf(values[1] == -1)){//unique
						info.Add (values [1]);//these are the symptoms... will add a lil description later
						animations.Add(new AnimationGroup ("@prob_1")); //will change later on

						//LoadTogglePrefab ();


						GameObject prefab = MonoBehaviour.Instantiate (togglePrefab, new Vector3(108, -97, 0), Quaternion.identity) as GameObject;
						prefab.gameObject.name = "toggle-" + index;
						//prefab.SetActive (false);
						prefab.transform.SetParent(infoCanvas.transform, false);
						prefab.transform.localScale = Vector3.one;
						

						index++;
					}
				}

			}


			break;

		}


	}


}
