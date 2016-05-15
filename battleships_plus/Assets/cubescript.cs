using UnityEngine;
using System.Collections;

public class cubescript : MonoBehaviour {

  public Vector3 spinspeed = new Vector3(0, 0, 0);
  public Vector3 spinAxis = new Vector3(0, 1, 0);

  // Use this for initialization
  void Start () {
    SetSize(1.9f);
  }
	
	// Update is called once per frame
	void Update () {
    
    if (this.transform.position.z < -.2f)
    {
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
      this.transform.Translate(0, 0, ((Random.value - Random.value) * .02f)); 
    }
    else if (this.transform.position.z > .2f)
    {
      this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
      this.transform.Translate(0, 0, ((Random.value - Random.value) * .02f));
    }
    else
    {
      this.transform.Translate(0, 0, ((Random.value - Random.value) * .02f));
    }
	}

  public void SetSize(float size)
  {
    this.transform.localScale = new Vector3(size, size, size);
  }

  public void SetMovementSpeed(float value)
  {
    this.transform.localScale = new Vector3(0, 0, value);
  }
}


