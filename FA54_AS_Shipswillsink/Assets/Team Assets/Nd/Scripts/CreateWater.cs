using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateWater : MonoBehaviour {

	//Constants
	//sets the distance of the island field from the water field at position x = 0, y = 0 to starting at x = -1, y = -1  
	const int DISTANCE_TO_ZERO = 0;
	public static int NUM_OF_CUBES_X = 10;
	public static int NUM_OF_CUBES_Y = 10;

	//Variables
	//This empty gameobject is made public, so you can drag the cube in the editor from the prefab folder onto this script on the GameController
	public GameObject prefabGameCube;
	public GameObject[,] grid = new GameObject[NUM_OF_CUBES_X, NUM_OF_CUBES_Y];
	public List<Ship> shipList = new List<Ship>();
	int xpos = 0;
	int ypos = 0;

	// Use this for initialization
	void Start () {

		//create the water...
		for(xpos = 0; xpos < NUM_OF_CUBES_X; xpos++)
		{
			for (ypos = 0; ypos < NUM_OF_CUBES_X; ypos++){
				grid[xpos, ypos] = Instantiate(prefabGameCube, new Vector3(xpos, 0f, ypos), Quaternion.identity) as GameObject;
				grid[xpos, ypos].GetComponent<WaterPosition>().cubeXcord = xpos;
				grid[xpos, ypos].GetComponent<WaterPosition>().cubeYcord = ypos;
			} 
		}

		//Create the Ships
		Ship ship_01 = ScriptableObject.CreateInstance<Ship>();
		ship_01.name = "Battleship";
		ship_01.length = 5;
		ship_01.hitpoints = 5;
		ship_01 = CreateShip (grid, ship_01);
		Debug.Log ("ship_01.x=" + ship_01.x + ", ship_01.y=" + ship_01.y);
		shipList.Add (ship_01);

		Ship ship_02 = ScriptableObject.CreateInstance<Ship>();
		ship_02.name = "Cruiser_01";
		ship_02.length = 4;
		ship_02.hitpoints = 4;
		ship_02 = CreateShip (grid, ship_02);
		Debug.Log ("ship_02.x=" + ship_02.x + ", ship_02.y=" + ship_02.y);
		shipList.Add (ship_02);

		Ship ship_03 = ScriptableObject.CreateInstance<Ship>();
		ship_03.name = "Cruiser_02";
		ship_03.length = 4;
		ship_03.hitpoints = 4;
		ship_03 = CreateShip (grid, ship_03);
		Debug.Log ("ship_03.x=" + ship_03.x + ", ship_03.y=" + ship_03.y);
		shipList.Add (ship_03);

		Ship ship_04 = ScriptableObject.CreateInstance<Ship>();
		ship_04.name = "Destroyer_01";
		ship_04.length = 3;
		ship_04.hitpoints = 3;
		ship_04 = CreateShip (grid, ship_04);
		Debug.Log ("ship_04.x=" + ship_04.x + ", ship_04.y=" + ship_04.y);
		shipList.Add (ship_04);

		Ship ship_05 = ScriptableObject.CreateInstance<Ship>();
		ship_05.name = "Destroyer_02";
		ship_05.length = 3;
		ship_05.hitpoints = 3;
		ship_05 = CreateShip (grid, ship_05);
		Debug.Log ("ship_05.x=" + ship_05.x + ", ship_05.y=" + ship_05.y);
		shipList.Add (ship_05);

		Ship ship_06 = ScriptableObject.CreateInstance<Ship>();
		ship_06.name = "Destroyer_03";
		ship_06.length = 3;
		ship_06.hitpoints = 3;
		ship_06 = CreateShip (grid, ship_06);
		Debug.Log ("ship_06.x=" + ship_06.x + ", ship_06.y=" + ship_06.y);
		shipList.Add (ship_06);

		Ship ship_07 = ScriptableObject.CreateInstance<Ship>();
		ship_07.name = "Submarine_01";
		ship_07.length = 2;
		ship_07.hitpoints = 2;
		ship_07 = CreateShip (grid, ship_07);
		Debug.Log ("ship_07.x=" + ship_07.x + ", ship_07.y=" + ship_07.y);
		shipList.Add (ship_07);

		Ship ship_08 = ScriptableObject.CreateInstance<Ship>();
		ship_08.name = "Submarine_02";
		ship_08.length = 2;
		ship_08.hitpoints = 2;
		ship_08 = CreateShip (grid, ship_08);
		Debug.Log ("ship_08.x=" + ship_08.x + ", ship_08.y=" + ship_08.y);
		shipList.Add (ship_08);

		Ship ship_09 = ScriptableObject.CreateInstance<Ship>();
		ship_09.name = "Submarine_03";
		ship_09.length = 2;
		ship_09.hitpoints = 2;
		ship_09 = CreateShip (grid, ship_09);
		Debug.Log ("ship_09.x=" + ship_09.x + ", ship_09.y=" + ship_09.y);
		shipList.Add (ship_09);

		Ship ship_10 = ScriptableObject.CreateInstance<Ship>();
		ship_10.name = "Submarine_04";
		ship_10.length = 2;
		ship_10.hitpoints = 2;
		ship_10 = CreateShip (grid, ship_10);
		Debug.Log ("ship_10.x=" + ship_10.x + ", ship_10.y=" + ship_10.y);
		shipList.Add (ship_10);
	}

	//Methods
	static Ship CreateShip(GameObject[,] grid, Ship ship)
	{
		bool bunker_set = false;

		while (bunker_set == false) {
			//random horizontal or vertical
			float rnd = Random.Range(0,2);
			//Debug.Log ("rnd: " + rnd);
			if (rnd == 0) {
				ship.horizontal = true;
			} else {
				ship.horizontal = false;
			}
			Debug.Log ("ship.name=" + ship.name);
			Debug.Log ("ship.horizontal: " + ship.horizontal);
			Debug.Log ("ship.length: " + ship.length);

			//Ship horizontal
			if (ship.horizontal) {
				//Random x and y coordinates long enough for the ship
				ship.x = Random.Range(0,grid.GetUpperBound(0)-ship.length+2);
				ship.y = Random.Range (0,grid.GetUpperBound(1));
				Debug.Log ("ship.x=" + ship.x + ", ship.y=" + ship.y);
				bool valid_position = false;

				//check if valid position for the bunker
				int trigger = 0;
				//check at the bunker
				for (int i = 0; i < ship.length; i++)
				{
					if (grid[ship.x + i, ship.y].GetComponent<WaterPosition>().shipPosition == true)
					{
						trigger++;
					}
				}
				//check left of the bunker
				if (ship.x + ship.length - 1 < 9){
					if (grid[ship.x + ship.length, ship.y].GetComponent<WaterPosition>().shipPosition == true)
					{
						trigger++;
					}
				}
				//check right of the bunker
				if (ship.x > 0){
					if (grid[ship.x - 1, ship.y].GetComponent<WaterPosition>().shipPosition == true){
						trigger++;
					}
				}
				//check above the bunker
				if (ship.y > 0){
					for (int i = 0; i < ship.length; i++){
						if (grid[ship.x + i, ship.y - 1].GetComponent<WaterPosition>().shipPosition == true)
						{
							trigger++;
						}
					}
				}
				//check below the bunker
				if (ship.y < 9){
					for (int i = 0; i < ship.length; i++){
						if (grid[ship.x + i, ship.y + 1].GetComponent<WaterPosition>().shipPosition == true)
						{
							trigger++;
						}
					}
				}
				//decide if position is valid or not
				if (trigger > 0)
				{
					valid_position = false;
				}
				else
				{
					valid_position = true;
				}

				if (valid_position) {
					//Set Bunker from this position over bunker.length along x-axis
					for (int x = 0; x < ship.length; x++) {
						grid [ship.x + x, ship.y].GetComponent<WaterPosition> ().shipPosition = true;
						grid [ship.x + x, ship.y].GetComponent<WaterPosition> ().shipName = ship.name;
					}

					bunker_set = true;
				}
			}
			//Ship Vertical
			else {
				//Random x and y coordinates for bunker
				ship.x = Random.Range(0,grid.GetUpperBound(0));
				ship.y = Random.Range (0, grid.GetUpperBound(1)-ship.length+2);
				Debug.Log ("ship.x=" + ship.x + ", ship.y=" + ship.y);
				bool valid_position = false;

				//check if valid position for the bunker
				int trigger = 0;
				//check at the bunker
				for (int i = 0; i < ship.length; i++)
				{
					if (grid[ship.x, ship.y + i].GetComponent<WaterPosition>().shipPosition == true)
					{
						trigger++;
					}
				}
				//check left of the bunker
				if (ship.x < 9){
					for (int i = 0; i < ship.length; i++) {
						if (grid [ship.x + 1, ship.y + i].GetComponent<WaterPosition> ().shipPosition == true) {
							trigger++;
						}
					}
				}
				//check right of the bunker
				if (ship.x > 0){
					for (int i = 0; i < ship.length; i++) {
						if (grid [ship.x - 1, ship.y + i].GetComponent<WaterPosition> ().shipPosition == true) {
							trigger++;
						}
					}
				}
				//check above the bunker
				if (ship.y > 0){
					if (grid [ship.x, ship.y - 1].GetComponent<WaterPosition> ().shipPosition == true) {
						trigger++;
					}
				}
				//check below the bunker
				if (ship.y + ship.length - 1 < 9){
					if (grid [ship.x, ship.y + ship.length].GetComponent<WaterPosition> ().shipPosition == true) {
						trigger++;
					}
				}
				//decide if position is valid or not
				if (trigger > 0)
				{
					valid_position = false;
				}
				else
				{
					valid_position = true;
				}

				if (valid_position){
					//Set Bunker from this position over bunker.length along y-axis
					for (int y = 0; y < ship.length; y++) {
						grid [ship.x, ship.y + y].GetComponent<WaterPosition> ().shipPosition = true;
						grid [ship.x, ship.y + y].GetComponent<WaterPosition> ().shipName = ship.name;
					}

					bunker_set = true;
				}
			}
		}

		return ship;
	}

}