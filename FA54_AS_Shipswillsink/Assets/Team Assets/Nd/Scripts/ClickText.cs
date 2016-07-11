using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickText : MonoBehaviour {
	public string originalText;
	bool option1Clicked = false;
	bool option2Clicked = false;
	public Color originalColor;
	public Color hoverColor;
	public Color selectedColor;
	public GameObject title;
	public GameObject option1;
	public GameObject option2;

	void OnMouseEnter () {
		GetComponent<Transform> ().localScale = new Vector3 (1.2f,1.2f,1);
		GetComponent<TextMesh> ().color = hoverColor;
	}

	void OnMouseExit () {
		GetComponent<Transform> ().localScale = new Vector3 (1,1,1);
		GetComponent<TextMesh> ().color = originalColor;
		if (this.name == "GameStartOption2" && option2Clicked) {
			GetComponent<TextMesh> ().text = originalText;
			option2Clicked = false;
		}
	}

	void OnMouseDown () {
		if (this.name == "GameStartOption1"){
			option1Clicked = true;
			//GetComponent<TextMesh> ().color = selectedColor;
			originalColor = hoverColor;
			StartCoroutine(WaitForSomeTime(5));
		}
		if (this.name == "GameStartOption2") {
			//GetComponent<TextMesh> ().color = selectedColor;
			print("Not yet implemented.");
			originalText = GetComponent<TextMesh> ().text;
			GetComponent<TextMesh> ().text = "Not yet implemented.";
			option2Clicked = true;
		}
	}

	//sideRate = Speed of movement (default: none), 
	//changeDirection = -1 (default, to the left)
	//changeDirection = 1 (supply this for movement to the right)
	void MoveText(GameObject myObject, float sideRate = 0, int changeDirection = -1){
		myObject.transform.Translate (((0f - (-sideRate * changeDirection)) * Time.deltaTime), (-1f - Random.Range (1.0f, 5.0f)) * Time.deltaTime, 0f);
		myObject.transform.Rotate (0f,0f,(-1f - (sideRate * 2) * changeDirection)*Time.deltaTime);
	}

	void Update () {
		if(option1Clicked){
			MoveText(title, 3.5f, 1);
			MoveText (option1, 2.5f);
			MoveText (option2, 3f, 1);
		}
	}

	void Start() {
		
	}

	IEnumerator WaitForSomeTime(int time) {
		//print(Time.time);
		yield return new WaitForSeconds(time);
		//print(Time.time);
		SceneManager.LoadScene ("ShipsWillSink_02");
	}
}
