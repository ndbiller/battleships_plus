using UnityEngine;
using System.Collections;

public class globalClickListener : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnMouseUp()
    {
        if(GameObject.Find("GameController").GetComponent<ShipPlacement>().globalFirstClick && GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne != this.gameObject)
        {
            Debug.Log("I got clicked");
            if(GameObject.Find("GameController").GetComponent<ShipPlacement>().theChosenOne.GetComponent<OnMouse>().badDirections.Count == 0)
            {
                Debug.Log("this could be placed");
                GameObject.Find("GameController").GetComponent<ShipPlacement>().PlaceShip();
            }
        }
    }

    // Update is called once per frame
    void Update () {
	
	}
}
