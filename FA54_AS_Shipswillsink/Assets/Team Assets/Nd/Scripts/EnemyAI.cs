using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
	public GameObject[,] grid;
	public bool targeting = false;
	public bool targetSelected = false;
	public int targetX;
	public int targetY;

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
		targetX = posX;
		targetY = posY;
		MarkTarget();
		//shoot
		Invoke("Shoot", 1.5f);
	}



	public void MarkTarget(){
		grid [targetX, targetY].GetComponent<WaterPosition> ().MarkMe ();
	}

	void Shoot(){
//		char split = ',';
//		string[] positions = x_and_y.Split (split);
//		int posX = int.Parse (positions [0]);
//		int posY = int.Parse (positions [1]);
		grid [targetX, targetY].GetComponent<WaterPosition> ().ShootAtField ();
	}

	IEnumerator WaitForSomeTimeAndShot(int time, int posX, int posY) {
		print ("targeting...");
		//print(Time.time);
		yield return new WaitForSeconds(time);
		//print(Time.time);
		print("shooting...");
		//Shoot(posX, posY);
	}
}
