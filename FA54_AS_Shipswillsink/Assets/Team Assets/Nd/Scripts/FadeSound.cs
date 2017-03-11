using UnityEngine;
using System.Collections;

public class FadeSound : MonoBehaviour {
	public AudioSource source;
	public bool returning = false;
	Object[] myMusic;

	void Awake (){
		myMusic = Resources.LoadAll ("Music", typeof(AudioClip));
		source.clip = myMusic [0] as AudioClip;
		DontDestroyOnLoad (this.gameObject);
	}

	void Update (){
		if (source.isPlaying && returning) {
			GameObject[] bgsource = GameObject.FindGameObjectsWithTag ("bgmusic");
			foreach (GameObject obj in bgsource) {
				if (!obj.GetComponent<FadeSound>().returning){
					Destroy (obj);
				}
			}
		}
		if (!source.isPlaying) {
			source.clip = myMusic [Random.Range (0, myMusic.Length)] as AudioClip;
			source.Play ();
		}
	}
}
