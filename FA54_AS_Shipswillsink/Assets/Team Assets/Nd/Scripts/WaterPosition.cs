using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WaterPosition : MonoBehaviour {

	//since every cube has this script, these variables hold their own position in the array
	public int cubeXcord = 0;
	public int cubeYcord = 0;
	public GameObject prefabShip;
	public GameObject prefabSmoke;
	public bool shipPosition = false;
	public string shipName;
	public bool shipSegmentDestroyed = false;
	public bool hasBeenShotAt = false;
	public bool mouseOver = false;
	public bool shotFired = false;
	public bool shipMadeVisible = false;

	//cube colors - they are vector 4, since our color is represented in RGBA (red, green, blue, alpha/transparency)
	public Color defaultColor;
	public Color hoverColor;
	public Color nopeColor;
	public Color hitColor;

	public List<Ship> list;

	void Start(){
		prefabSmoke.GetComponent<ParticleSystem> ().Stop();
		GetComponent<Renderer> ().material.SetColor ("_Color", defaultColor);
	}

	void OnMouseEnter()
	{
		if (GameObject.Find("GameControllerNd").GetComponent<GameStates>().enemyTurn){
			//this makes sure the color change is only requested once
			mouseOver = true;

			if (hasBeenShotAt || shipSegmentDestroyed) {
				//change the cube colors, while hovering
				//GetComponent<Renderer> ().material.SetColor ("_Color", nopeColor);
			} else {
				GetComponent<Renderer> ().material.SetColor ("_Color", hoverColor);
			}
		}
	}

	void OnMouseDown()
	{
		ShootAtField();
	}

	public void ShootAtField(){
		if (GameObject.Find ("GameControllerNd").GetComponent<GameStates> ().enemyTurn) {
			if (!hasBeenShotAt) {
				shotFired = true;
				defaultColor = hitColor;
				GetComponent<Renderer> ().material.SetColor ("_Color", defaultColor);

				//Check if bunker is on here
				Debug.Log ("shipPosition: " + this.gameObject.GetComponent<WaterPosition> ().shipPosition);
				if (this.gameObject.GetComponent<WaterPosition> ().shipPosition) {
					Debug.Log ("shipName: " + this.gameObject.GetComponent<WaterPosition> ().shipName);
					shipSegmentDestroyed = true;
					string hitName = this.gameObject.GetComponent<WaterPosition> ().shipName;
					list = GameObject.Find ("GameControllerNd").GetComponent<CreateWater> ().shipList;
					int index = list.FindIndex (d => d.name == hitName);
					Debug.Log ("index: " + index);
					Debug.Log ("name: " + list [index].name);
					Debug.Log ("length: " + list [index].length);
					list [index].hitpoints = list [index].hitpoints - 1;
					Debug.Log ("hitpoints: " + list [index].hitpoints);
					if (list [index].hitpoints == 0) {
						list [index].destroyed = true;
						list.RemoveAt (index);
					}
					if (list.Count == 0) {
						SceneManager.LoadScene ("GameLost");
					}
				}
				hasBeenShotAt = true;
				GameObject.Find ("GameControllerNd").GetComponent<GameStates> ().ChangeTurn ();
			}
		}
	}

	void OnMouseExit()
	{
		if (GameObject.Find("GameControllerNd").GetComponent<GameStates>().enemyTurn) {
			//this makes sure the color change is only requested once
			mouseOver = false;
				
			//change the cube colors, while hovering
			GetComponent<Renderer> ().material.SetColor ("_Color", defaultColor);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (shipPosition && !shipMadeVisible) {
			prefabShip.GetComponent<MeshRenderer> ().enabled = true;
			shipMadeVisible = true;
		}
		if (shotFired) {
			if (shipPosition && hasBeenShotAt) {
				//prefabShip.GetComponent<MeshRenderer> ().enabled = true;
				prefabSmoke.GetComponent<ParticleSystem> ().Play();
				//defaultColor = hitColor;
			} else {
				prefabShip.GetComponent<MeshRenderer> ().enabled = false;
			}
			defaultColor = hitColor;
			shotFired = false;
		}
	}

}
