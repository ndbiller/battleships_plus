using UnityEngine;
using System.Collections;

public class GameStates : MonoBehaviour {
	public bool enemyTurn;
	public bool playerTurn;
	public bool shotFiredAI;

	void Start(){
		enemyTurn = true;
		playerTurn = false;
		shotFiredAI = false;
	}

	void Update() {
		if (enemyTurn && !shotFiredAI) {
			GameObject.Find ("GameControllerNd").GetComponent<EnemyAI> ().ShootAtShips ();
			shotFiredAI = true;
		}
	}

	public void ChangeTurn(){
		enemyTurn = !enemyTurn;
		playerTurn = !playerTurn;
		if (enemyTurn) {
			shotFiredAI = false;
		}
		GameObject.Find ("GameControllerNd").GetComponent<SwitchCamera> ().ChangeCamera ();
	}
}
