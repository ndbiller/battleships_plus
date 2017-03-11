using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickToLoadStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		GameObject.Find ("BGMusic").GetComponent<FadeSound> ().returning = true;
		SceneManager.LoadScene ("GameStart");
	}
}
