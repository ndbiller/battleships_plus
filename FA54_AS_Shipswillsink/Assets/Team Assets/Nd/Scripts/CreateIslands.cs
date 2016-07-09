using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Structs
//public struct Bunker
//{
//	public string Name;
//	public int Length;
//	public int Hitpoints;
//	public bool Horizontal;
//	public int X;
//	public int Y;
//	public bool Destroyed;
//
//	public Bunker(string name, int length, bool horizontal, int x, int y, bool destroyed)
//	{
//		this.Name = name;
//		this.Length = length;
//		this.Hitpoints = length;
//		this.Horizontal = horizontal;
//		this.X = x;
//		this.Y = y;
//		this.Destroyed = destroyed;
//	}
//}

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
		Bunker bunker_01 = new Bunker("HQ", 5, true, 0, 0, false);
		bunkerList.Add (bunker_01);
		//mehr, wenn einer gesetzt wird...

		//Debug.Log("Start Setting Bunkers... [xpos=" + xpos + ", ypos=" + ypos + "]");
		for (xpos = 0; xpos < NUM_OF_TILES_X; xpos++)
		{
			//Debug.Log ("[for x] -> xpos: " + xpos + ", ypos: " + ypos);
			for (ypos = 0; ypos < NUM_OF_TILES_Y; ypos++) 
			{
				//Debug.Log ("[for y] -> xpos: " + xpos + ", ypos: " + ypos);
				Debug.Log(tileSet[xpos, ypos]);
			}
		}
		//Debug.Log("End Setting Bunkers... [xpos=" + xpos + ", ypos=" + ypos + "]");

		Debug.Log ("bunker_01.name: " + bunker_01.name);
		Debug.Log ("bunkerList.Contains(bunker_01): " + bunkerList.Contains(bunker_01));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Methods
	static Bunker CreateBunker(GameObject[,] tileSet, Bunker bunker)
	{
		//method sets the bool bunkerPosition of the gameobject at that position in the array tileset to true
		// //tileSet[xpos, ypos].GetComponent<CubePosition>().bunkerPosition = true;
		//update in script cubeposition turns on the mesh renderer of the bunkercube child objects of the island prefabs with bunkerposition true for debugging
		//remove this if statement in cubeposition to make bunkers invisible again...
		return bunker;
	}

}
