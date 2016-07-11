using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject[,] grid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShootAtShips(){
		grid = GameObject.Find ("GameControllerNd").GetComponent<CreateWater> ().grid;
		bool targetValid = false;
		int posX = 0;
		int posY = 0;
		while (!targetValid){
			print ("AI is targeting up to x -> "+grid.GetUpperBound(0)+1);
			print ("AI is targeting up to y -> "+grid.GetUpperBound(1)+1);
			//get random x and y position
			posX = Random.Range(0,grid.GetUpperBound(0)+1);
			posY = Random.Range (0,grid.GetUpperBound(1)+1);
			//see if field has been shot at before (is valid)
			if (grid[posX, posY].GetComponent<WaterPosition>().hasBeenShotAt == false) {
				targetValid = true;
			}
		}
		//shoot
		print("Ka-Boom!");
		WaitForSomeTimeAndShot(3, posX, posY);
		Shoot(posX, posY);
	}

	void Shoot(int posX, int posY){
		grid [posX, posY].GetComponent<WaterPosition> ().ShootAtField ();
	}

	IEnumerator WaitForSomeTimeAndShot(int time, int posX, int posY) {
		print ("targeting...");
		//print(Time.time);
		yield return new WaitForSeconds(time);
		//print(Time.time);
		print("shooting...");
		Shoot(posX, posY);
	}
}
