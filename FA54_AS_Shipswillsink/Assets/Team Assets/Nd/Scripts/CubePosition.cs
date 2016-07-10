using UnityEngine;
using System.Collections;

public class CubePosition : MonoBehaviour {

	//since every cube has this script, these variables hold their own position in the array
	public int cubeXcord = 0;
	public int cubeYcord = 0;
	public GameObject prefabBunker;
	public bool bunkerPosition = false;
	public string bunkerName;
	public bool bunkerSegmentDestroyed = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (bunkerPosition) {
			prefabBunker.GetComponent<MeshRenderer> ().enabled = true;
		} else {
			prefabBunker.GetComponent<MeshRenderer> ().enabled = false;
		}
	
	}
}
