using UnityEngine;
using System.Collections;
using System.Collections.Generic;


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
    public bool isShipped = false;

    //to check if the player is hovering over this cube
    public bool mouseHover = false;

    //cube colors - they are vector 4, since our color is represented in RGBA (red, green, blue, alpha/transparency)
    public Color defaultColor = new Vector4(13, 148, 193, 194); //blue
    public Color hoverColor = new Vector4(40, 159, 75, 240); //green
    public Color nopeColor = new Vector4(255, 0, 0, 127); //red
    public Color mouseDownColor = new Vector4(96, 96, 96, 240); //gray

    //Variables to store local information
    int currentX = 0;
    int currentY = 0;
    int currentDirectionLength = 0;
    int currentShipLength = 0;
    int shipPos = 0;
    //int badDirection = -1;
    bool currentDirAvailable = true;
    public bool firstClick = false;
    bool secondClick = false;

    int coloringDirection = 0;


    public int setDirection = 0;

    Color origColor;
    Color currentColor;

    public List<int> badDirections = new List<int>();

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
        if(isShipped)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else if (!isAvailable)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.gray;
        }
    }


    void OnMouseEnter()
    {
        if (placementMode)
        {
            mouseHover = true;
            GameObject.Find("GameController").GetComponent<ShipPlacement>().lastHovered = GameObject.Find("GameController").GetComponent<ShipPlacement>().currentlyHovered;
            GameObject.Find("GameController").GetComponent<ShipPlacement>().currentlyHovered = this.gameObject;


            //makes sure the colors are only changed if the player has not clicked
            if (GameObject.Find("GameController").GetComponent<ShipPlacement>().globalFirstClick == false)
            {
                
                currentShipLength = GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength;
                //here the function is called to change the cube colors, while hovering
                changeBlockCol(currentShipLength, cubeXcord, cubeYcord, hoverColor, nopeColor);
            }
            else if(GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne == this.gameObject)
            {
                //placeholder in case I want to change the color of the cube i clicked on, when i hover over it again
                GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne.GetComponent<OnMouse>().setThisBlocksColors();

            }
            else
            {
                GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne.GetComponent<OnMouse>().setThisBlocksColors();
            }
            //this makes sure the color change is only requested once per entry
            placementMode = false;

            //Debug.Log(Input.mousePosition);
        }
    }

    void OnMouseExit()
    {
        if (!placementMode)
        {

            if (GameObject.Find("GameController").GetComponent<ShipPlacement>().globalFirstClick == false ||
                GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne == this.gameObject)
            {
                if (!firstClick)
                {
                    mouseHover = false;
                    currentShipLength = GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength;

                    changeBlockCol(currentShipLength, cubeXcord, cubeYcord, defaultColor, defaultColor);

                }
                //the same function is called to change the colors back to the original, when not hovering over the cube anymore

                //resetting bad directions
                badDirections.Clear();
            }
            else
            {
                GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne.GetComponent<OnMouse>().resetThisBlocksColors();
                badDirections.Clear();

            }


            badDirections.Clear();

            //makes sure this is also only called once per exit
            placementMode = true;

            //gameObject.GetComponent<Renderer>().material.color = defaultColor;
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[cubeXcord, cubeYcord + 1].GetComponent<Renderer>().material.color = defaultColor;
            //GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[cubeXcord, cubeYcord + 2].GetComponent<Renderer>().material.color = defaultColor;
        }
    }


    void OnMouseUp()
    {
        if (mouseHover && isAvailable)
        {
            
            if (!GameObject.Find("GameController").GetComponent<ShipPlacement>().globalFirstClick || firstClick)
            {
                changeBlockCol(currentShipLength, cubeXcord, cubeYcord, defaultColor, defaultColor);
                if (firstClick)
                {
                    //mouseHover = false;
                    firstClick = false;
                    //Cursor.visible = true;

                }
                else
                {
                    //mouseHover = true;
                    firstClick = true;
                    //Cursor.visible = false;
                    GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne = this.gameObject;
                    GameObject.Find("GameController").GetComponent<ShipPlacement>().currentlyHovered = this.gameObject;
                    GameObject.Find("GameController").GetComponent<ShipPlacement>().lastHovered = this.gameObject;

                }
                GameObject.Find("GameController").GetComponent<ShipPlacement>().globalFirstClick = firstClick;


            }

            

        }
    }

    //call the color change from a different script
    public void setThisBlocksColors()
    {
        badDirections.Clear();
        changeBlockCol(currentShipLength, cubeXcord, cubeYcord, hoverColor, nopeColor);
    }
    public void resetThisBlocksColors()
    {
        //badDirections.Clear();
        changeBlockCol(currentShipLength, cubeXcord, cubeYcord, defaultColor, defaultColor);
    }



    //this function will be called to set which direction away from the currently hovered cube should be colored
    void setCurrentDirection(int currentDirection, int setXpos, int setYpos, int currentShipBlock)
    {
        switch (currentDirection)
        {
            //create Y+ direction (South)
            case 0:
                currentX = setXpos;
                currentY = setYpos + currentShipBlock;
                currentDirectionLength = CreateCubes.numOfCubesY;
                shipPos = setYpos;
                break;
            //create Y- direction (North)
            case 1:
                currentX = setXpos;
                currentY = setYpos - currentShipBlock;
                currentDirectionLength = CreateCubes.numOfCubesY;
                shipPos = setYpos;
                break;

            //create X+ direction (West)
            case 2:
                currentX = setXpos + currentShipBlock;
                currentY = setYpos;
                currentDirectionLength = CreateCubes.numOfCubesX;
                shipPos = setXpos;
                break;

            //create X- direction (East)
            case 3:
                currentX = setXpos - currentShipBlock;
                currentY = setYpos;
                currentDirectionLength = CreateCubes.numOfCubesX;
                shipPos = setXpos;
                break;

        }
    }

    //This function changes the color of blocks 
    void changeBlockCol(int shipLength, int myXpos, int myYpos, Color hoverCol, Color nopeCol)
    {


        for (int i = 0; i < shipLength; i++)
        {

            for (int a = 0; a < 4; a++)
            {


                if (firstClick)
                {
                    coloringDirection = setDirection;
                }
                else
                {
                    coloringDirection = a;
                }
                //Debug.Log("Case: " + a);
                setCurrentDirection(coloringDirection, myXpos, myYpos, i);


                //checks if the current cube fits the grid

                //Since this if asks for quiet a lot, let me break it down!
                //The overview of this if statement is:
                //IF(A && B && (C || D))
                //A asks if the cube the mouse is currently hovering over is available
                //B asks if any other cubes in the same direction as the current cube are already taken
                //C has several components: (C1 && (C2 || C3))
                //C1 asks if the cube we want to color is even within the length of the ship we want to place, in a POSITIVE direction
                //C2 asks if the Y position of the block we want to color is higher than the block we are hovering over
                //C3 asks if the X position of the block we want to color is higher than the block we are hovering over
                //I check for C2 or C3 because C1 would always be true if we go in the negative direction
                //D is exactly like C, but in the negative direction

                if (isAvailable && (!badDirections.Contains(a)) &&
                    (((shipPos + shipLength <= currentDirectionLength) && (currentY > myYpos || currentX > myXpos)) ||
                    ((shipPos - shipLength > -2) && (currentY < myYpos || currentX < myXpos))))
                {


                    //check if the cube we currently want to color is available / not taken by another ship 
                    //this is done by accessing the cubeSet array stored on the gameObject called GameController
                    //I ask for a specific position within the array of all cubes (i saved them into that array, when they were created)
                    //and then I access the script onMouse on the cube we want to color, to check the variable isAvailable (which is false if the cube is already taken by another ship)
                    //ps: a gameObject is the common class for all things that can be placed in the editor/3D world of unity

                    //Debug.Log("checking Cube " + currentX + ":" + currentY);
                    if (GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[currentX, currentY].GetComponent<OnMouse>().isAvailable)
                    {
                        GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[currentX, currentY].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, hoverCol, 100f);
                    }
                    else
                    {
                        //if the cube is not available, add it to the list of bad direction, in which a ship can not be placed
                        //Debug.Log("X:" + currentX + " Y:" + currentY + " shiplength: " + shipLength);

                        if (!badDirections.Contains(a))
                        {
                            badDirections.Add(a);

                            Debug.Log(a + " direction is already taken!");
                        }

                        //resetting the whole loop (next time this direction will be skipped and instead made red)
                        a = 0;
                        i = 0;


                    }
                }
                else
                {
                    //Debug.Log(shipPos - i);
                    //if (currentY < myYpos || currentX < myXpos) Debug.Log("pos ok");


                    //check if the individual block still fits on the grid
                    //same check as explained above, just a bit simpler
                    if (((shipPos + i < currentDirectionLength) && ((currentY > myYpos || currentX > myXpos)) ||
                        (shipPos - i > -1) && (currentY < myYpos || currentX < myXpos)))
                    {
                        GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[currentX, currentY].GetComponent<Renderer>().material.color = Color.Lerp(defaultColor, nopeCol, 100f);

                        //I also add bad directions here, since I want to include directions that go over the edge of the map
                        if (!badDirections.Contains(a))
                        {
                            badDirections.Add(a);

                            Debug.Log(a + " direction is already taken!");
                        }
                    }
                }
            }
        }

        //Debug.Log(badDirections.Count);
        if (isAvailable && badDirections.Count < 4) //this makes sure, if all 4 directions are unavailable, the block we hover over is also unavailable
        {
            gameObject.GetComponent<Renderer>().material.color = hoverCol;
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = nopeCol;
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
