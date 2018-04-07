using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeCupController : MonoBehaviour {

    public GameObject coffeeFillObject;
    public GameObject coffeCup;
    public float fillSpeed = 1;
    public float yOffset = 0.3f;
    public float topOffset = 0.2f;
    public float fillStep = 0.2f;
    public float coffeDrinkSpeed = 0.1f;

    Vector2 bottom;
    Vector2 top;
    public float fillRate = 0;

    


	// Use this for initialization
	void Start () {
        var cupSprite = coffeCup.GetComponent<SpriteRenderer>().sprite;
        var bounds = cupSprite.bounds;
        var min=bounds.min;
        var max = bounds.max;

        bottom = coffeCup.transform.position+ new Vector3(bounds.center.x, min.y,0)*coffeCup.transform.localScale.x;
        top = coffeCup.transform.position + new Vector3(bounds.center.x, max.y, 0) * coffeCup.transform.localScale.x-Vector3.up* topOffset;


    }

    private void Update()
    {
        fillRate -= Time.deltaTime * coffeDrinkSpeed;
        setFill();
    }

    //Sets fill of coffe cup with fillRate
    void setFill()
    {
        //var fillSprite = coffeeFillObject.GetComponent<SpriteRenderer>().sprite;
        //var bounds = fillSprite.bounds;
        
        //var max = bounds.max;
        //var yOffset = max.y - bounds.center.y;
        //yOffset *= coffeeFillObject.transform.localScale.y;

        fillRate = Mathf.Clamp(fillRate, 0, 1);
        var aimPos=Vector2.Lerp(bottom, top, fillRate)-Vector2.up* yOffset;
        coffeeFillObject.transform.position = aimPos;
            

    }

    IEnumerator fillTo(float aimFill)
    {
        aimFill = Mathf.Clamp(aimFill, 0, 1);
        var currentFill = fillRate;
        float r = 0;
        while (r < 1)
        {
            r += Time.deltaTime * fillSpeed ;
            fillRate = Mathf.Lerp(currentFill, aimFill, r);
            setFill();

            yield return 0;
        }

        fillRate = aimFill;
        setFill();   
        
        yield break;
    }

    private void OnMouseDown()
    {
        var aimFill=fillRate + fillStep;
        StartCoroutine( fillTo(aimFill));
    }
}
