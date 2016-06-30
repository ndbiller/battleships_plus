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

		for(xpos = 0; xpos < numOfTilesX; xpos++)
        {
            tileSet[xpos, ypos] = Instantiate(prefabIslandSand, new Vector3(-xpos - 1 , 0, -ypos - 1), Quaternion.identity) as GameObject;
            tileSet[xpos, ypos].GetComponent<OnMouse>().cubeXcord = -xpos - 1;
            tileSet[xpos, ypos].GetComponent<OnMouse>().cubeYcord = -ypos - 1;

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