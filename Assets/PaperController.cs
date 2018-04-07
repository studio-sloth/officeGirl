using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaperController : MonoBehaviour {

    [HideInInspector]
    public PaperContainer container;
    public int timeToComplete = 5000;

    public static bool isWorking=false;
        

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator collected()
    {
        isWorking = true;

        float timer = 0;

        container.jobCompleteUI.gameObject.SetActive(true);
        container.jobCompleteUI.value = 0;
        

        while (timer<timeToComplete)
        {
            timer += Time.deltaTime;
            //print(timer);
            container.jobCompleteUI.value = timer / timeToComplete;

            yield return null;
        }

        container.removePaper();
        container.jobCompleteUI.gameObject.SetActive(false);
        //Destroy(gameObject);
        isWorking = false;
        container.gameController.jobCompleted();

        yield break ;


    }

    public void OnMouseDown()
    {
        if(!isWorking)
            StartCoroutine(collected());
    }
}
