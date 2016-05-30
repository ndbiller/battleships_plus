using UnityEngine;
using System.Collections;

public class SelectShipSize : MonoBehaviour {

    int shipLength = 0;

	// Use this for initialization
	void Start () {
        shipLength = Mathf.RoundToInt(transform.localPosition.z);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseEnter()
    {
        switch (shipLength)
        {
            case 0:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.green;
                break;
            case 1:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.green;
                break;
            case 2:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_3").GetComponent<Renderer>().material.color = Color.green;
                break;
            case 3:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_3").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_4").GetComponent<Renderer>().material.color = Color.green;
                break;
            case 4:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_3").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_4").GetComponent<Renderer>().material.color = Color.green;
                GameObject.Find("ShipLength_5").GetComponent<Renderer>().material.color = Color.green;
                break;
        }

    }
    void OnMouseExit()
    {
        switch (shipLength)
        {
            case 0:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.white;
                break;
            case 1:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.white;
                break;
            case 2:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_3").GetComponent<Renderer>().material.color = Color.white;
                break;
            case 3:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_3").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_4").GetComponent<Renderer>().material.color = Color.white;
                break;
            case 4:
                GameObject.Find("ShipLength_1").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_2").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_3").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_4").GetComponent<Renderer>().material.color = Color.white;
                GameObject.Find("ShipLength_5").GetComponent<Renderer>().material.color = Color.white;
                break;
        }

    }




    void OnMouseUp()
    {
        switch(shipLength)
        {
            case 0:
                GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength = 1;
                break;
            case 1:
                GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength = 2;
                break;
            case 2:
                GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength = 3;
                break;
            case 3:
                GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength = 4;
                break;
            case 4:
                GameObject.Find("GameController").GetComponent<CreateCubes>().shipLength = 5;
                break;
        }
    }


}
