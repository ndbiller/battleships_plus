using UnityEngine;
using System.Collections;

public class QuitIt : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Escape)) {
			print ("Quit it!");
			Application.Quit ();
		}
	}
}
