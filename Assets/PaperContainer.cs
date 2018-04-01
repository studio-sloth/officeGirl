using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaperContainer : MonoBehaviour {

    public GameObject paperPrefab;
    public List<GameObject> papers;
    Vector3 topPosition;
    public float paperUpStep = 5f;
    public Slider jobCompleteUI;


    // Use this for initialization
    void Start () {
        topPosition = transform.position;	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) addPaper();
        if (Input.GetKeyDown(KeyCode.E)) removePaper();
        if (Input.GetKeyDown(KeyCode.C)) setupContainer(7);

    }

    public void addPaper()
    {
        var instantiatedPaper = Instantiate<GameObject>(paperPrefab);
        instantiatedPaper.transform.position=(topPosition);

        PaperController papCont = instantiatedPaper.GetComponent<PaperController>();
        papCont.container = this;



        topPosition += Vector3.up * paperUpStep;
        papers.Add(instantiatedPaper);

    }

    public void removePaper()
    {
        if (papers.Count == 0) return;

        var paperToRemove = papers[papers.Count - 1];
        Destroy(paperToRemove);
        papers.RemoveAt(papers.Count - 1);
        topPosition -= Vector3.up * paperUpStep;

    }
    private void OnDisable()
    {
        setupContainer(0);
        jobCompleteUI.gameObject.SetActive(false);
    }

    public void setupContainer(int numberOfPaper)
    {
        //Destroy all current paper
        foreach(var p in papers)
        {
            Destroy(p);
        }

        papers.Clear();
        topPosition = transform.position;

        //Create paper objects with the count of number of paper.
        for (var n=0;n<numberOfPaper;n++)
        {
            addPaper();
        }
    }
}
