using UnityEngine;
using System.Collections;

public class CreateCubes : MonoBehaviour {

    //This empty gameobject is made public, so you can drag the cube in the editor from the prefab folder onto this script on the GameController
    public GameObject prefabCube;
    public static int numOfCubesX = 10;
    public static int numOfCubesY = 10;
    public int shipLength = 1;
    int counter = 0;

    int xpos = 0;
    int ypos = 0;

    public GameObject[,] cubeSet = new GameObject[numOfCubesX+1, numOfCubesY+1];
 


    // Use this for initializations
    void Start () {

        for(xpos = 0; xpos < numOfCubesX; xpos++ )
        {
            cubeSet[xpos, ypos] = Instantiate(prefabCube, new Vector3(xpos , 0, ypos), Quaternion.identity) as GameObject;
            cubeSet[xpos, ypos].GetComponent<OnMouse>().cubeXcord = xpos;
            cubeSet[xpos, ypos].GetComponent<OnMouse>().cubeYcord = ypos;

            //Debug.Log("xpos " + xpos + "ypos " + ypos);
             
            if(xpos == numOfCubesX-1 && ypos < numOfCubesY-1)
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
