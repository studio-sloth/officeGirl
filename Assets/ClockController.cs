using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour {

    public float startTime=8;
    public float finalTime = 18;
    public float speed = 1;

    
    float initialDegree;
    public GameObject hourHand;

    float time = 0;
    public GameController gameController;

	// Use this for initialization
	void Start () {
        time = startTime;
        initialDegree = 369/12*startTime;
        
	}
	
	// Update is called once per frame
	void Update () {



        if (time > finalTime)
        {
            time = startTime + time - finalTime;
            gameController.finishGame();
        }

        time += Time.deltaTime;

        var degree = 360 / 12 * (time % 12);
        //print(degree);
        hourHand.transform.localRotation = Quaternion.Euler(0, 0, initialDegree - degree);

        //print(time);
	}

    public float getTime()
    {
        return time;
    }
}
