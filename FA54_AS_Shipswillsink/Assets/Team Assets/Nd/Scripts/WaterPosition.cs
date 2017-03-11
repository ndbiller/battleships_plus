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
	bool allowClick = false;

	//cube colors - they are vector 4, since our color is represented in RGBA (red, green, blue, alpha/transparency)
	public Color defaultColor;
	public Color hoverColor;
	public Color nopeColor;
	public Color hitColor;

	public List<Ship> list;
	public GameObject[,] grid;

	void Start(){
		prefabSmoke.GetComponent<ParticleSystem> ().Stop();
		grid = GameObject.Find ("GameControllerNd").GetComponent<CreateWater> ().grid;
		GetComponent<Renderer> ().material.SetColor ("_Color", defaultColor);
	}

	void OnMouseEnter()
	{
		if (GameObject.Find("GameControllerNd").GetComponent<GameStates>().enemyTurn && allowClick){
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
		if (GameObject.Find ("GameControllerNd").GetComponent<GameStates> ().enemyTurn && allowClick) {
			ShootAtField ();
		}
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
					prefabSmoke.GetComponent<ParticleSystem> ().Play();
					Debug.Log ("hitpoints: " + list [index].hitpoints);
					if (list [index].hitpoints == 0) {
						list [index].destroyed = true;
						//show that ship/bunker is destroyed
						ShowDestruction(index);
						list.RemoveAt (index);
					}
					if (list.Count == 0) {
						SceneManager.LoadScene ("GameLost");
					}
				}
				hasBeenShotAt = true;
				Invoke ("Switch", 1f);
			}
		}
	}

	public void ShowDestruction(int index){
		print ("Show Destruction:");
		print ("list[index]: " + list[index].name+","+list[index].x +","+list[index].y+","+list[index].horizontal+","+list[index].length);
		//grid = GameObject.Find ("GameControllerNd").GetComponent<CreateIslands> ().tileSet;
		for (int i = 0; i <= list [index].length; i++) {
			if (list [index].horizontal) {
				grid [list [index].x + i, list [index].y].GetComponentInChildren<Transform> ().FindChild ("CubeShip").localPosition = new Vector3 (0f, 0.42f, 0f);
				grid [list [index].x + i, list [index].y].GetComponentInChildren<Transform> ().FindChild ("ParticleSystemSmoke").GetComponent<ParticleSystem> ().Stop ();
			} else {
				grid [list [index].x, list [index].y + i].GetComponentInChildren<Transform> ().FindChild ("CubeShip").localPosition = new Vector3 (0f, 0.42f, 0f);
				grid [list [index].x, list [index].y + i].GetComponentInChildren<Transform> ().FindChild ("ParticleSystemSmoke").GetComponent<ParticleSystem> ().Stop ();
			}
		}
	}

	void OnMouseExit()
	{
		if (GameObject.Find("GameControllerNd").GetComponent<GameStates>().enemyTurn && allowClick) {
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
				//defaultColor = hitColor;
			} else {
				prefabShip.GetComponent<MeshRenderer> ().enabled = false;
			}
			defaultColor = hitColor;
			shotFired = false;
		}
	}

	void Switch(){
		GameObject.Find ("GameControllerNd").GetComponent<GameStates> ().ChangeTurn ();
	}

	public void MarkMe(){
		GetComponent<Renderer> ().material.SetColor ("_Color", hoverColor);
	}

}
