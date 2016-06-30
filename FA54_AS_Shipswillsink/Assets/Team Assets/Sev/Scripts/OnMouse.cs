using UnityEngine;
using System.Collections;

public class OnMouse : MonoBehaviour
{
    //code is not cleaned up yet! Some unused variables and overly complicated :D


    //since every cube has this script, these variables hold their own position in the array
    public int cubeXcord = 0;
    public int cubeYcord = 0;


    //boolean to check whether the ship placement hover effect shoud occure
    public bool placementMode = true;

    //checks if the cube this script is attached to is available for ship placement
    public bool isAvailable = true;

    //to check if the player is hovering over this cube
    public bool mouseHover = false;

    //cube colors - they are vector 4, since our color is represented in RGBA (red, green, blue, alpha/transparency)
    public Color defaultColor = new Vector4(13, 148, 193, 194); //blue
    public Color hoverColor = new Vector4(40, 159, 75, 240); //green
    public Color nopeColor = new Vector4(255, 0, 0, 127); //red

    //Variables to store local information
    int currentX = 0;
    int currentY = 0;
    int currentDirectionLength = 0;
    int currentShipLength = 0;
    int shipPos = 0;
    int badDirection = -1;
    bool currentDirAvailable = true;

    Color origColor;
    Color currentColor;

    //start is only called once at the beginning and then never again
    void Start()
    {

        //here the original color is applied to the cube
        gameObject.GetComponent<Renderer>().material.color = defaultColor;
        //Debug.Log("init " + gameObject.GetComponent<Renderer>().material.color);

    }

    // Update is called once per frame
    //usually most of the action happens in Update(), but the mouse hover functions have built in update cycles, so we don't use update
    void Update()
    {

    }


    //void OnMouseUp()
    //{
    //    if (gameObject.GetComponent<Renderer>().material.color == defaultColor)
    //    {
    //        Debug.Log("IfdefCol " + gameObject.GetComponent<Renderer>().material.color);
    //        gameObject.GetComponent<Renderer>().material.color = Color.red;
    //    }
    //    else if (gameObject.GetComponent<Renderer>().material.color == Color.red)
    //    {
    //        gameObject.GetComponent<Renderer>().material.color = defaultColor;
    //        Debug.Log("test1 " + gameObject.GetComponent<Renderer>().material.color);
    //    }
    //
    //}

	void OnMouseDown()
	{
		Destroy(this.gameObject);
	}

    void OnMouseEnter()
    {
        if (placementMode)
        {

            currentShipLength = GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength;
            //here the function is called to change the cube colors, while hovering
            changeBlockCol(currentShipLength, cubeXcord, cubeYcord, hoverColor, nopeColor);

            //this makes sure the color change is only requested once
            placementMode = false;


            //gameObject.GetComponent<Renderer>().material.color = Color.blue;
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[cubeXcord, cubeYcord + 1].GetComponent<Renderer>().material.color = Color.blue;
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[cubeXcord, cubeYcord + 2].GetComponent<Renderer>().material.color = Color.blue;

        }
    }

    void OnMouseExit()
    {
        if (!placementMode)
        {

            currentShipLength = GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength;

            //the same function is called to change the colors back to the original, when not hovering over the cube anymore
            changeBlockCol(currentShipLength, cubeXcord, cubeYcord, defaultColor, defaultColor);

            //resetting 
            badDirection = -1;
            //makes sure this is also only called once per exit
            placementMode = true;

            //gameObject.GetComponent<Renderer>().material.color = defaultColor;
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[cubeXcord, cubeYcord + 1].GetComponent<Renderer>().material.color = defaultColor;
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[cubeXcord, cubeYcord + 2].GetComponent<Renderer>().material.color = defaultColor;
        }
    }


    //This function generates the hover effect
    void changeBlockCol(int shipLength, int myXpos, int myYpos, Color hoverCol, Color nopeCol)
    {

        if (isAvailable)
        {
            gameObject.GetComponent<Renderer>().material.color = hoverCol;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = nopeCol;
        }

        for (int i = 0; i < shipLength; i++)
        {
            //Debug.Log("X:" + myXpos + " Y:" + myYpos);
            //Debug.Log(i);

            for (int a = 0; a < 4; a++)
            {

                //Debug.Log("Case: " + a);
                switch (a)
                {
                    //create Y+ direction
                    case 0:
                        currentX = myXpos;
                        currentY = myYpos + i;
                        currentDirectionLength = CreateCubes.numOfCubesY;
                        shipPos = myYpos;
                        break;
                    //create Y- direction
                    case 1:
                        currentX = myXpos;
                        currentY = myYpos - i;
                        currentDirectionLength = CreateCubes.numOfCubesY;
                        shipPos = myYpos;
                        break;

                    //create X+ direction
                    case 2:
                        currentX = myXpos + i;
                        currentY = myYpos;
                        currentDirectionLength = CreateCubes.numOfCubesX;
                        shipPos = myXpos;
                        break;

                    //create X- direction
                    case 3:
                        currentX = myXpos - i;
                        currentY = myYpos;
                        currentDirectionLength = CreateCubes.numOfCubesX;
                        shipPos = myXpos;
                        break;

                }

                //Debug.Log("shipPos: " + shipPos + " shipLength: " + shipLength + " currentDirectionLength: " + currentDirectionLength + " i: " + i);
                //
                //check if it fits the grid
                if (isAvailable && (badDirection != a) && 
                    (((shipPos + shipLength <= currentDirectionLength) && (currentY > myYpos || currentX > myXpos)) || 
                    ((shipPos - shipLength > -2) && (currentY < myYpos || currentX < myXpos))))
                {
                    

                    //check if cubes in the current direction are available 
                    //Debug.Log("checking Cube " + currentX + ":" + currentY);
                    if (GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[currentX, currentY].GetComponent<OnMouse>().isAvailable)
                    {
                        GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[currentX, currentY].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, hoverCol, 100f);
                    }
                    else
                    {
                        //HIER STiMMT WAS NICHT!!!!!
                        //HIER STiMMT WAS NICHT!!!!!
                        //HIER STiMMT WAS NICHT!!!!!
                        //Debug.Log("X:" + currentX + " Y:" + currentY + " shiplength: " + shipLength);
                        currentDirAvailable = false;
                        badDirection = a;

                        //resetting the whole loop
                        a = 0;
                        i = 0;
                        
                    }
                }
                else
                {
                    //Debug.Log(shipPos - i);
                    //if (currentY < myYpos || currentX < myXpos) Debug.Log("pos ok");


                    //check if the individual block still fits on the grid
                    if (((shipPos + i < currentDirectionLength) && ((currentY > myYpos || currentX > myXpos)) || 
                        (shipPos - i > -1) && (currentY < myYpos || currentX < myXpos)))
                    {
                        //Debug.Log(shipPos + " i: " + i + " < " + currentDirectionLength);
                        //Debug.Log(shipPos + " i: " + -i + " >" + -1);

                        GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[currentX, currentY].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, nopeCol, 100f);
                    }
                }


            }




            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos, myYpos - i].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, hoverCol, 100f);
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos + i, myYpos].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, hoverCol, 100f);
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos - i, myYpos].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, hoverCol, 100f);
        }


    }
    //for (int i = 0; i < shipLength; i++)
    //{
    //    gameObject.GetComponent<Renderer>().material.color = defaultColor;
    //    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos, myYpos + i].GetComponent<Renderer>().material.color = Color.Lerp(hoverColor, defaultColor, 100f);
    //    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos, myYpos - i].GetComponent<Renderer>().material.color = Color.Lerp(hoverColor, defaultColor, 100f);
    //    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos + i, myYpos].GetComponent<Renderer>().material.color = Color.Lerp(hoverColor, defaultColor, 100f);
    //    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos - i, myYpos].GetComponent<Renderer>().material.color = Color.Lerp(hoverColor, defaultColor, 100f);
    //
    //}


}
