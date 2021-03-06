using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateIslands : MonoBehaviour {

	//Constants
	//sets the distance of the island field from the water field at position x = 0, y = 0 to starting at x = -1, y = -1  
	const int DISTANCE_TO_ZERO = 1;
	const float HEIGHT_DIFFERENCE = 0.3f;
	public static int NUM_OF_TILES_X = 10;
	public static int NUM_OF_TILES_Y = 10;

	//Variables
    //This empty gameobject is made public, so you can drag the cube in the editor from the prefab folder onto this script on the GameController
    public GameObject prefabIslandSand;
	public GameObject prefabIslandDirt;
	public GameObject prefabIslandStone;
	public GameObject prefabIslandGrass;
	public GameObject[,] tileSet = new GameObject[NUM_OF_TILES_X, NUM_OF_TILES_Y];
	public List<Bunker> bunkerList = new List<Bunker>();
    int xpos = 0;
    int ypos = 0;

    // Use this for initialization
    void Start () {

		//create the island...
		for(xpos = 0; xpos < NUM_OF_TILES_X; xpos++)
        {
			// create the inside of the first ring of land of the island... (Dirt)
			if (xpos >= 1 && xpos <= NUM_OF_TILES_X-2 && ypos >= 1 && ypos <= NUM_OF_TILES_X-2) 
			{
				// create the second inner ring of the island... (Grass)
				if (xpos >= 2 && xpos <= NUM_OF_TILES_X-3 && ypos >= 2 && ypos <= NUM_OF_TILES_X-3)
				{
					// create the third inner ring of the island... (Stone)
					if (xpos >= 3 && xpos <= NUM_OF_TILES_X-4 && ypos >= 3 && ypos <= NUM_OF_TILES_X-4)
					{
						tileSet[xpos, ypos] = Instantiate(prefabIslandStone, new Vector3(-xpos - DISTANCE_TO_ZERO , Random.Range(0.0f + 2 * HEIGHT_DIFFERENCE, 0.0f + 3 * HEIGHT_DIFFERENCE), -ypos - DISTANCE_TO_ZERO), Quaternion.identity) as GameObject;
						tileSet[xpos, ypos].GetComponent<CubePosition>().cubeXcord = -xpos - DISTANCE_TO_ZERO;
						tileSet[xpos, ypos].GetComponent<CubePosition>().cubeYcord = -ypos - DISTANCE_TO_ZERO;
					}
					// create the second inner ring of the island... (Grass)
					else
					{
						tileSet[xpos, ypos] = Instantiate(prefabIslandGrass, new Vector3(-xpos - DISTANCE_TO_ZERO , Random.Range(0.0f + HEIGHT_DIFFERENCE, 0.0f + 2 * HEIGHT_DIFFERENCE), -ypos - DISTANCE_TO_ZERO), Quaternion.identity) as GameObject;
						tileSet[xpos, ypos].GetComponent<CubePosition>().cubeXcord = -xpos - DISTANCE_TO_ZERO;
						tileSet[xpos, ypos].GetComponent<CubePosition>().cubeYcord = -ypos - DISTANCE_TO_ZERO;
					}
				}
				// create the inside of the first ring of land of the island... (Dirt)
				else
				{
					tileSet[xpos, ypos] = Instantiate(prefabIslandDirt, new Vector3(-xpos - DISTANCE_TO_ZERO, Random.Range(0.0f, 0.0f + HEIGHT_DIFFERENCE), -ypos - DISTANCE_TO_ZERO), Quaternion.identity) as GameObject;
					tileSet[xpos, ypos].GetComponent<CubePosition>().cubeXcord = -xpos - DISTANCE_TO_ZERO;
					tileSet[xpos, ypos].GetComponent<CubePosition>().cubeYcord = -ypos - DISTANCE_TO_ZERO;
				}
			}
			// create the outermost ring of the island... (Sand)
			else 
			{
				tileSet[xpos, ypos] = Instantiate(prefabIslandSand, new Vector3(-xpos - DISTANCE_TO_ZERO, 0, -ypos - DISTANCE_TO_ZERO), Quaternion.identity) as GameObject;
				tileSet[xpos, ypos].GetComponent<CubePosition>().cubeXcord = -xpos - DISTANCE_TO_ZERO;
				tileSet[xpos, ypos].GetComponent<CubePosition>().cubeYcord = -ypos - DISTANCE_TO_ZERO;
			}
				
			//Debug.Log("xpos: " + xpos + ", ypos: " + ypos);

			if(xpos == NUM_OF_TILES_X-1 && ypos < NUM_OF_TILES_Y-1)
			{
				ypos++;
				xpos = -1;
			}
        }

		//Create the Bunkers
		//Structs der Bunker (1 x HQ, 2 x Airfield, 3 x Base, 4 x Outpost)
		//HQ
		Bunker bunker_01 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_01, "HQ", 5, tileSet, bunkerList);

		//Airfields
		Bunker bunker_02 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_02, "Airfield_01", 4, tileSet, bunkerList);
		Bunker bunker_03 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_03, "Airfield_02", 4, tileSet, bunkerList);

		//Bases
		Bunker bunker_04 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_04, "Base_01", 3, tileSet, bunkerList);
		Bunker bunker_05 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_05, "Base_02", 3, tileSet, bunkerList);
		Bunker bunker_06 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_06, "Base_03", 3, tileSet, bunkerList);

		//Outposts
		Bunker bunker_07 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_07, "Outpost_01", 2, tileSet, bunkerList);
		Bunker bunker_08 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_08, "Outpost_02", 2, tileSet, bunkerList);
		Bunker bunker_09 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_09, "Outpost_03", 2, tileSet, bunkerList);
		Bunker bunker_10 = ScriptableObject.CreateInstance<Bunker>();
		CreateAndAddBunker(bunker_10, "Outpost_04", 2, tileSet, bunkerList);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Methods
	static void CreateAndAddBunker(Bunker bunker, string name, int length, GameObject[,] tileSet, List<Bunker> bunkerList)
	{
		bunker.name = name;
		bunker.length = length;
		bunker.hitpoints = length;
		bunker = CreateBunker (tileSet, bunker);
		bunkerList.Add (bunker);
	}

	static Bunker CreateBunker(GameObject[,] tileSet, Bunker bunker)
	{
		//method sets the bool bunkerPosition of the gameobject at that position in the array tileset to true
		// //tileSet[xpos, ypos].GetComponent<CubePosition>().bunkerPosition = true;
		//update in script cubeposition turns on the mesh renderer of the bunkercube child objects of the island prefabs with bunkerposition true for debugging
		//remove this if statement in cubeposition to make bunkers invisible again...
		bool bunker_set = false;

		while (bunker_set == false) {
			//random horizontal or vertical
			float rnd = Random.Range(0,2);
			//Debug.Log ("rnd: " + rnd);
			if (rnd == 0) {
				bunker.horizontal = true;
			} else {
				bunker.horizontal = false;
			}
			//Debug.Log ("bunker.name=" + bunker.name);
			//Debug.Log ("bunker.horizontal: " + bunker.horizontal);
			//Debug.Log ("bunker.length: " + bunker.length);

			//Bunker horizontal
			if (bunker.horizontal) {
				//Random x and y coordinates long enough for the bunker
				bunker.x = Random.Range(0,tileSet.GetUpperBound(0)-bunker.length+2);
				bunker.y = Random.Range (0, tileSet.GetUpperBound(1));
				//Debug.Log ("bunker.x=" + bunker.x + ", bunker.y=" + bunker.y);
				bool valid_position = false;

				//check if valid position for the bunker
				int trigger = 0;
				//check at the bunker
				for (int i = 0; i < bunker.length; i++)
				{
					if (tileSet[bunker.x + i, bunker.y].GetComponent<CubePosition>().bunkerPosition == true)
					{
						trigger++;
					}
				}
				//check left of the bunker
				if (bunker.x + bunker.length - 1 < 9){
					if (tileSet[bunker.x + bunker.length, bunker.y].GetComponent<CubePosition>().bunkerPosition == true)
					{
						trigger++;
					}
				}
				//check right of the bunker
				if (bunker.x > 0){
					if (tileSet[bunker.x - 1, bunker.y].GetComponent<CubePosition>().bunkerPosition == true){
						trigger++;
					}
				}
				//check above the bunker
				if (bunker.y > 0){
					for (int i = 0; i < bunker.length; i++){
						if (tileSet[bunker.x + i, bunker.y - 1].GetComponent<CubePosition>().bunkerPosition == true)
						{
							trigger++;
						}
					}
				}
				//check below the bunker
				if (bunker.y < 9){
					for (int i = 0; i < bunker.length; i++){
						if (tileSet[bunker.x + i, bunker.y + 1].GetComponent<CubePosition>().bunkerPosition == true)
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
					for (int x = 0; x < bunker.length; x++) {
						tileSet [bunker.x + x, bunker.y].GetComponent<CubePosition> ().bunkerPosition = true;
						tileSet [bunker.x + x, bunker.y].GetComponent<CubePosition> ().bunkerName = bunker.name;
					}

					bunker_set = true;
				}
			}
			//Bunker Vertical
			else {
				//Random x and y coordinates for bunker
				bunker.x = Random.Range(0,tileSet.GetUpperBound(0));
				bunker.y = Random.Range (0, tileSet.GetUpperBound(1)-bunker.length+2);
				//Debug.Log ("bunker.x=" + bunker.x + ", bunker.y=" + bunker.y);
				bool valid_position = false;

				//check if valid position for the bunker
				int trigger = 0;
				//check at the bunker
				for (int i = 0; i < bunker.length; i++)
				{
					if (tileSet[bunker.x, bunker.y + i].GetComponent<CubePosition>().bunkerPosition == true)
					{
						trigger++;
					}
				}
				//check left of the bunker
				if (bunker.x < 9){
					for (int i = 0; i < bunker.length; i++) {
						if (tileSet [bunker.x + 1, bunker.y + i].GetComponent<CubePosition> ().bunkerPosition == true) {
							trigger++;
						}
					}
				}
				//check right of the bunker
				if (bunker.x > 0){
					for (int i = 0; i < bunker.length; i++) {
						if (tileSet [bunker.x - 1, bunker.y + i].GetComponent<CubePosition> ().bunkerPosition == true) {
							trigger++;
						}
					}
				}
				//check above the bunker
				if (bunker.y > 0){
					if (tileSet [bunker.x, bunker.y - 1].GetComponent<CubePosition> ().bunkerPosition == true) {
						trigger++;
					}
				}
				//check below the bunker
				if (bunker.y + bunker.length - 1 < 9){
					if (tileSet [bunker.x, bunker.y + bunker.length].GetComponent<CubePosition> ().bunkerPosition == true) {
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
					for (int y = 0; y < bunker.length; y++) {
						tileSet [bunker.x, bunker.y + y].GetComponent<CubePosition> ().bunkerPosition = true;
						tileSet [bunker.x, bunker.y + y].GetComponent<CubePosition> ().bunkerName = bunker.name;
					}

					bunker_set = true;
				}
			}
		}
		return bunker;
	}
}
