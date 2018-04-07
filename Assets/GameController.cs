using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public ClockController clock;
    public GameObject mainMenu;
    public PaperContainer paperCont;

    public int numberOfWorkToStart=10;

    int numberOfCompletedJob=0;
    
    


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        clock.enabled = true;
        mainMenu.SetActive(false);

        paperCont.enabled = true;
        paperCont.setupContainer(numberOfWorkToStart);
    }

    public void jobCompleted()
    {
        numberOfCompletedJob++;
    }

    public void finishGame()
    {
        clock.enabled = false;
        mainMenu.SetActive(true);
        paperCont.enabled = false;
    }
}
