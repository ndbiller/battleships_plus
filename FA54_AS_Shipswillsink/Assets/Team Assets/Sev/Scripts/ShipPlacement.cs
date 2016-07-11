using UnityEngine;
using System.Collections;

public class ShipPlacement : MonoBehaviour
{

    public bool globalFirstClick = false;
    public GameObject theChosenOne; //this stores the cube the player clicked
    public GameObject currentlyHovered;
    public GameObject lastHovered;

    int chosenXchord = 0;
    int chosenYchord = 0;
    int hoveredXchord = 0;
    int hoveredYchord = 0;

    int xDifference = 0;
    int yDifference = 0;

    int switcher = 0;
    int setDirection = 0;

    int chosenXpos = 0;
    int chosenYpos = 0;

    int minGridSize = -1;
    int maxGridSize = CreateCubes.numOfCubesX;

    // Use this for initialization
    void Start()
    {
        lastHovered = this.gameObject;
        
    }

    void makeSurroundingBlocksUnavailable(int myXpos, int myYpos)
    {

        Debug.Log("maxgrid: " + maxGridSize);
        GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos, myYpos].GetComponent<OnMouse>().isAvailable = false;
        if (myXpos + 1 < maxGridSize && myYpos + 1 < maxGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos + 1, myYpos + 1].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("0");
        }
        if (myXpos + 1 < maxGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos + 1, myYpos].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("1");
        }
        if (myYpos + 1 < maxGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos, myYpos + 1].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("2");
        }
        if (myXpos - 1 > minGridSize && myYpos - 1 > minGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos - 1, myYpos - 1].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("3");
        }
        if (myXpos - 1 > minGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos - 1, myYpos].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("4");
        }
        if (myYpos - 1 > minGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos, myYpos - 1].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("5");
        }
        if (myXpos + 1 < maxGridSize && myYpos - 1 > minGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos + 1, myYpos - 1].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("6");
        }
        if (myXpos - 1 > minGridSize && myYpos + 1 < maxGridSize)
        {
            GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[myXpos - 1, myYpos + 1].GetComponent<OnMouse>().isAvailable = false;
        }
        else
        {
            Debug.Log("7");
        }


    }

    public void PlaceShip()
    {
        chosenXpos = theChosenOne.GetComponent<OnMouse>().cubeXcord;
        chosenYpos = theChosenOne.GetComponent<OnMouse>().cubeYcord;



        for (int i = 0; i < GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength; i++)
        {
            switch (setDirection)
            {
                case 0:
                    makeSurroundingBlocksUnavailable(chosenXpos, chosenYpos + i);
                    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[chosenXpos, chosenYpos + i].GetComponent<OnMouse>().isShipped = true;
                    break;
                case 1:
                    makeSurroundingBlocksUnavailable(chosenXpos, chosenYpos - i);
                    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[chosenXpos, chosenYpos - i].GetComponent<OnMouse>().isShipped = true;
                    break;
                case 2:
                    makeSurroundingBlocksUnavailable(chosenXpos + i, chosenYpos);
                    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[chosenXpos + i, chosenYpos].GetComponent<OnMouse>().isShipped = true;
                    break;
                case 3:
                    makeSurroundingBlocksUnavailable(chosenXpos - i, chosenYpos);
                    GameObject.Find("GameController").GetComponent<CreateCubes>().cubeSet[chosenXpos - i, chosenYpos].GetComponent<OnMouse>().isShipped = true;
                    break;

            }
        }


        Debug.Log("unclick!");
        globalFirstClick = false;
        theChosenOne.GetComponent<OnMouse>().firstClick = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (globalFirstClick)
        {
            //Cursor.visible = false;

        }
        else
        {
            //Cursor.visible = true;

        }

        //asks if the player has clicked and if this action has already been preformed
        if (globalFirstClick && (currentlyHovered != lastHovered))
        {
            //theChosenOne.GetComponent<OnMouse>().setDirection = setDirection;

            //theChosenOne.GetComponent<OnMouse>().resetThisBlocksColors();
            //theChosenOne.GetComponent<OnMouse>().setThisBlocksColors();
            Debug.Log("it has been clicked and hovered!");

            if (theChosenOne != currentlyHovered)
            {
                chosenXchord = theChosenOne.GetComponent<OnMouse>().cubeXcord;
                chosenYchord = theChosenOne.GetComponent<OnMouse>().cubeYcord;
                hoveredXchord = currentlyHovered.GetComponent<OnMouse>().cubeXcord;
                hoveredYchord = currentlyHovered.GetComponent<OnMouse>().cubeYcord;



                xDifference = Mathf.Abs(hoveredXchord - chosenXchord);
                yDifference = Mathf.Abs(hoveredYchord - chosenYchord);

                Debug.Log(xDifference + " : " + yDifference);

                //0 = east (positive Y from clicked block)
                //1 = west (negative Y)
                //2 = south (positive X)
                //3 = north (negative X)


                if (xDifference > yDifference)
                {

                    if (chosenXchord > hoveredXchord)
                    {
                        setDirection = 3;
                    }
                    else if (chosenXchord < hoveredXchord)
                    {
                        setDirection = 2;
                    }


                }
                else if (xDifference < yDifference)
                {
                    if (chosenYchord > hoveredYchord)
                    {
                        setDirection = 1;
                    }
                    else if (chosenYchord < hoveredYchord)
                    {
                        setDirection = 0;
                    }
                }


                Debug.Log("coloring: " + setDirection);
                if (theChosenOne.GetComponent<OnMouse>().setDirection != setDirection)
                {
                    theChosenOne.GetComponent<OnMouse>().resetThisBlocksColors();

                }
                theChosenOne.GetComponent<OnMouse>().setDirection = setDirection;
                theChosenOne.GetComponent<OnMouse>().setThisBlocksColors();
            }

            //this makes sure the process is only done once per mouse movement
            //Otherwise this would be repleated every frame since it in the update function
            lastHovered = currentlyHovered;

        }

    }

    void OnMouseUpAsButton()
    {

    }
}
