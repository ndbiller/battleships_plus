using UnityEngine;
using System.Collections;

public class StartWaver : MonoBehaviour {

    public float waveHeight = 0.5f;
    public float waveSpeedHeight = 0.1f;
    public float waveSpeedY = 0.5f;
    public float waveDirectionY = 0.5f;
    public float waveSpeedX = 0.5f;
    public float waveDirectionX = 0.5f;

    float cubeXWait = 0;
    float cubeYWait = 0;

    public float waveWaitSpeed = 0.1f;

    float counter = 0;

    private Vector3 startPosition;
    bool moveCube = false;

    void Start()
    {

        //cubeXWait = gameObject.GetComponent<OnMouse>().cubeXcord;
        cubeXWait = Mathf.Abs(gameObject.transform.position.x);
        cubeXWait = cubeXWait * 0.1f;
        startPosition = transform.position;

        StartCoroutine(startWaveDelay());


    }

    IEnumerator startWaveDelay()
    {
        yield return new WaitForSeconds(cubeXWait);
        moveCube = true;
    }

    void Update()
    {
        if(moveCube)
        {
            counter = counter + waveWaitSpeed;
            //Debug.Log(counter);
            transform.position = startPosition + new Vector3(Mathf.Sin(counter * waveSpeedX) * waveDirectionX, Mathf.Sin(counter * waveSpeedHeight) * waveHeight, Mathf.Sin(counter * waveSpeedY) * waveDirectionY);
        }
    }


    //first try

    //public float horizontalSpeed = 0;
    //public float verticalSpeed = 0;
    //public float amplitude = 0;
    //
    //public float cubePositionWait = 1;
    //
    //bool moveCube = false;
    //
    //private Vector3 tempPosition;
    //
    //
    //void Start()
    //{
    //    cubePositionWait = gameObject.GetComponent<OnMouse>().cubeXcord;
    //    cubePositionWait = cubePositionWait * 0.5f;
    //    StartCoroutine(Wait(cubePositionWait));
    //    tempPosition = transform.position;
    //
    //}
    //
    //void Update()
    //{
    //    if (moveCube)
    //    {
    //        tempPosition.x += horizontalSpeed;
    //        //Debug.Log(Time.realtimeSinceStartup);
    //        tempPosition.y = Mathf.Sin(cubePositionWait++ * verticalSpeed) * amplitude;
    //        transform.position = tempPosition;
    //    }
    //}
    //
    //IEnumerator Wait(float duration)
    //{
    //    //This is a coroutine
    //    //Debug.Log("Start Wait() function. The time is: " + Time.time);
    //    //Debug.Log("Float duration = " + duration);
    //    yield return new WaitForSeconds(duration);   //Wait
    //    moveCube = true;
    //    //Debug.Log("End Wait() function and the time is: " + Time.time);
    //}

}
