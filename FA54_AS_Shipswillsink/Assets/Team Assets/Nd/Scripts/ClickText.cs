using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ClickText : MonoBehaviour {
	public string originalText;
	bool option1Clicked = false;
	bool option2Clicked = false;
	bool option3Clicked = false;
	public Color originalColor;
	public Color hoverColor;
	public Color selectedColor;
	public GameObject title;
	public GameObject option1;
	public GameObject option2;
	public GameObject option3;

	void OnMouseEnter () {
		GetComponent<Transform> ().localScale = new Vector3 (1.2f,1.2f,1);
		GetComponent<TextMesh> ().color = hoverColor;
	}

	void OnMouseExit () {
		GetComponent<Transform> ().localScale = new Vector3 (1,1,1);
		GetComponent<TextMesh> ().color = originalColor;
//		if (this.name == "GameStartOption2" && option2Clicked) {
//			GetComponent<TextMesh> ().text = originalText;
//			option2Clicked = false;
//		}
	}

	void OnMouseDown () {
		if (this.name == "GameStartOption1"){
			option1Clicked = true;
			//GetComponent<TextMesh> ().color = selectedColor;
			originalColor = hoverColor;
			GameObject.Find ("GameStartText").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption1").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption2").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption3").GetComponent<StartWaver> ().enabled = false;
			StartCoroutine(WaitForSomeTime(5));
			//GameObject.Find ("GameStartText").GetComponent<FadeSound> ().FadeOut ();  //SoundFadeOut ();
		}
		if (this.name == "GameStartOption2") {
			//GetComponent<TextMesh> ().color = selectedColor;
			//print("Not yet fully implemented.");
			//originalText = GetComponent<TextMesh> ().text;
			originalColor = hoverColor;
			GetComponent<TextMesh> ().text = "Not yet fully implemented.";
			option2Clicked = true;
			GameObject.Find ("GameStartText").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption1").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption2").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption3").GetComponent<StartWaver> ().enabled = false;
			StartCoroutine(WaitForSomeTime(5));
			//this.GetComponent<FadeSound> ().FadeOut ();  //SoundFadeOut ();
		}
		if (this.name == "GameStartOption3") {
			//GetComponent<TextMesh> ().color = selectedColor;
			//print("Not yet fully implemented.");
			//originalText = GetComponent<TextMesh> ().text;
			originalColor = hoverColor;
			option3Clicked = true;
			GameObject.Find ("GameStartText").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption1").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption2").GetComponent<StartWaver> ().enabled = false;
			GameObject.Find ("GameStartOption3").GetComponent<StartWaver> ().enabled = false;
			SceneManager.LoadScene ("GameHelp");
			//StartCoroutine(WaitForSomeTime(5));
			//this.GetComponent<FadeSound> ().FadeOut ();  //SoundFadeOut ();
		}
		option1.GetComponent<BoxCollider> ().enabled = false;
		option2.GetComponent<BoxCollider> ().enabled = false;
		option3.GetComponent<BoxCollider> ().enabled = false;
		GetComponent<Transform> ().localScale = new Vector3 (1,1,1);
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
			MoveText (option3, 4.2f);
		}
		if(option2Clicked){
			MoveText(title, 3.5f);
			MoveText (option1, 2.5f, 1);
			MoveText (option2, 3f);
			MoveText (option3, 4.2f, 1);
		}
	}

	void Start() {
		
	}

	IEnumerator WaitForSomeTime(int time) {
		//print(Time.time);
		yield return new WaitForSeconds(time);
		//print(Time.time);
		if (option1Clicked)
			SceneManager.LoadScene ("ShipsWillSink_02");
		if (option2Clicked)
			SceneManager.LoadScene ("ShipsWillSink_03");
//		if (option3Clicked)
//			SceneManager.LoadScene ("GameHelp");
	}
}
