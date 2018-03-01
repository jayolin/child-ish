using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BodyPartTileController : MonoBehaviour {

	// Use this for initialization
	//array, prefab and list controller on UI are needed
	public GameObject TilePrefab;
	public GameObject GridView;
	ArrayList BodyParts;

	public Sprite[] Sprites;


	void Start () {

		Sprites = Resources.LoadAll<Sprite>("Sprites/143204-arrow-collection");

		BodyParts = new ArrayList () {

			new BodyPart (Sprites[0]),
			new BodyPart (Sprites[1]),
			new BodyPart (Sprites[2]),
			new BodyPart (Sprites[3]),
			new BodyPart (Sprites[4]),
			//new BodyPart (Sprites[5]),

			//new BodyPart (AnimalImg[0]),
			//new BodyPart (AnimalImg[1]),
			//new BodyPart (AnimalImg[2])
		};

		foreach (BodyPart bodyPart in BodyParts) {

			GameObject prefab = Instantiate (TilePrefab) as GameObject;
			BodyPartTile tile = prefab.GetComponent<BodyPartTile>();
			tile.Icon.sprite = bodyPart.Icon;

			prefab.transform.parent = GridView.transform;
			prefab.transform.localScale = Vector3.one;

		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
