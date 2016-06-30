using UnityEngine;
using System.Collections;

public class CreateIslands : MonoBehaviour {

    //This empty gameobject is made public, so you can drag the cube in the editor from the prefab folder onto this script on the GameController
	//formerly prefabCube
    public GameObject prefabIslandSand;
	public GameObject prefabIslandDirt;
	public GameObject prefabIslandStone;
	public GameObject prefabIslandGrass;
    public static int numOfTilesX = 10;
    public static int numOfTilesY = 10;
    public int shipLength = 1;
    //int counter_1 = 0;
	//public int positionDirt = 2;

    int xpos = 0;
    int ypos = 0;

	//public GameObject[,] tileSet = new GameObject[numOfTilesX, numOfTilesY];
 
	public GameObject[,] tileSet = new GameObject[numOfTilesX+1, numOfTilesY+1];

    // Use this for initialization
    void Start () {

		//create the island...
		for(xpos = 0; xpos < numOfTilesX; xpos++)
        {
			// create the inside of the first ring of land of the island... (Dirt)
			if (xpos >= 1 && xpos <= numOfTilesX-2 && ypos >= 1 && ypos <= numOfTilesX-2) 
			{
				// create the second inner ring of the island... (Grass)
				if (xpos >= 2 && xpos <= numOfTilesX-3 && ypos >= 2 && ypos <= numOfTilesX-3)
				{
					// create the third inner ring of the island... (Stone)
					if (xpos >= 3 && xpos <= numOfTilesX-4 && ypos >= 3 && ypos <= numOfTilesX-4)
					{
						tileSet[xpos, ypos] = Instantiate(prefabIslandStone, new Vector3(-xpos - 1 , Random.Range(1.0f, 1.5f), -ypos - 1), Quaternion.identity) as GameObject;
						tileSet[xpos, ypos].GetComponent<OnMouse>().cubeXcord = -xpos - 1;
						tileSet[xpos, ypos].GetComponent<OnMouse>().cubeYcord = -ypos - 1;
					}
					// create the second inner ring of the island... (Grass)
					else
					{
						tileSet[xpos, ypos] = Instantiate(prefabIslandGrass, new Vector3(-xpos - 1 , Random.Range(0.5f, 1.0f), -ypos - 1), Quaternion.identity) as GameObject;
						tileSet[xpos, ypos].GetComponent<OnMouse>().cubeXcord = -xpos - 1;
						tileSet[xpos, ypos].GetComponent<OnMouse>().cubeYcord = -ypos - 1;
					}
				}
				// create the inside of the first ring of land of the island... (Dirt)
				else
				{
					tileSet[xpos, ypos] = Instantiate(prefabIslandDirt, new Vector3(-xpos - 1 , Random.Range(0.0f, 0.5f), -ypos - 1), Quaternion.identity) as GameObject;
					tileSet[xpos, ypos].GetComponent<OnMouse>().cubeXcord = -xpos - 1;
					tileSet[xpos, ypos].GetComponent<OnMouse>().cubeYcord = -ypos - 1;
				}
			}
			// create the outermost ring of the island... (Sand)
			else 
			{
				tileSet[xpos, ypos] = Instantiate(prefabIslandSand, new Vector3(-xpos - 1 , 0, -ypos - 1), Quaternion.identity) as GameObject;
				tileSet[xpos, ypos].GetComponent<OnMouse>().cubeXcord = -xpos - 1;
				tileSet[xpos, ypos].GetComponent<OnMouse>().cubeYcord = -ypos - 1;
			}
				
			Debug.Log("xpos: " + xpos + ", ypos: " + ypos);

			if(xpos == numOfTilesX-1 && ypos < numOfTilesY-1)
			{
				ypos++;
				xpos = -1;
			}
        }

	}
	
	// Update is called once per frame
	void Update () {
		

	}
}