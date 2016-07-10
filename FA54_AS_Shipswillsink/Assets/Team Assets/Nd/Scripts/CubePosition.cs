using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CubePosition : MonoBehaviour {

	//since every cube has this script, these variables hold their own position in the array
	public int cubeXcord = 0;
	public int cubeYcord = 0;
	public GameObject prefabBunker;
	public GameObject prefabSmoke;
	public bool bunkerPosition = false;
	public string bunkerName;
	public bool bunkerSegmentDestroyed = false;
	public bool hasBeenShotAt = false;
	public bool mouseOver = false;
	public bool shotFired = false;

	//cube colors - they are vector 4, since our color is represented in RGBA (red, green, blue, alpha/transparency)
	public Color defaultColor;
	public Color hoverColor;
	public Color nopeColor;
	public Color hitColor;

	public List<Bunker> list;

	void Start(){
		prefabSmoke.GetComponent<ParticleSystem> ().Stop();
	}

	void OnMouseEnter()
	{
		//this makes sure the color change is only requested once
		mouseOver = true;

		if (hasBeenShotAt || bunkerSegmentDestroyed) {
			//change the cube colors, while hovering
			//GetComponent<Renderer> ().material.SetColor ("_Color", nopeColor);
		} else {
			GetComponent<Renderer> ().material.SetColor ("_Color", hoverColor);
		}
	}

	void OnMouseDown()
	{
		if (!hasBeenShotAt) {
			shotFired = true;
			defaultColor = hitColor;
			GetComponent<Renderer>().material.SetColor("_Color", defaultColor);

			//Check if bunker is on here
			Debug.Log("bunkerPosition: " + this.gameObject.GetComponent<CubePosition>().bunkerPosition);
			if (this.gameObject.GetComponent<CubePosition> ().bunkerPosition) {
				Debug.Log ("bunkerName: " + this.gameObject.GetComponent<CubePosition> ().bunkerName);
				bunkerSegmentDestroyed = true;
				string hitName = this.gameObject.GetComponent<CubePosition> ().bunkerName;
				list = GameObject.Find("GameController").GetComponent<CreateIslands>().bunkerList;
				int index = list.FindIndex (d => d.name == hitName);
				Debug.Log ("index: " + index);
				Debug.Log ("name: " + list[index].name);
				Debug.Log ("length: " + list[index].length);
				list [index].hitpoints = list [index].hitpoints - 1;
				Debug.Log ("hitpoints: " + list[index].hitpoints);
				if (list[index].hitpoints == 0){
					list [index].destroyed = true;
					list.RemoveAt (index);
				}
				if (list.Count == 0) {
					SceneManager.LoadScene ("GameWon");
				}
			}
			hasBeenShotAt = true;
		}
	}

	void OnMouseExit()
	{
		//this makes sure the color change is only requested once
		mouseOver = false;

		//change the cube colors, while hovering
		GetComponent<Renderer>().material.SetColor("_Color", defaultColor);
	}
	
	// Update is called once per frame
	void Update () {
		if (shotFired) {
			if (bunkerPosition && hasBeenShotAt) {
				prefabBunker.GetComponent<MeshRenderer> ().enabled = true;
				prefabSmoke.GetComponent<ParticleSystem> ().Play();
				//defaultColor = hitColor;
			} else {
				//prefabBunker.GetComponent<MeshRenderer> ().enabled = false;
			}
			defaultColor = hitColor;
			shotFired = false;
		}
	}

}
