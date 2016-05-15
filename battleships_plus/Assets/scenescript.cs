using UnityEngine;
using System.Collections;

public class scenescript : MonoBehaviour {

  public GameObject myPrefab;

  int size = 10;

	// Use this for initialization
	void Start () {

    for (int y = -10; y < size; y = y + 2)
    {
      for (int x = -10; x < size; x = x + 2)
      {
        var newCube = Instantiate(myPrefab, new Vector3(x, y, 0), Quaternion.identity);
      } 
    }

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
