using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCamera : MonoBehaviour {

    public float speed = 1f;
    public SpriteRenderer movableArea;
    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        Swipe();

        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        if (ver > 0) moveUp();
        if (ver < 0) moveDown();
        if (hor > 0) moveRight();
        if (hor < 0) moveLeft();




    }

    void moveUp()
    {
        move(Vector2.up);
    }

    void moveDown()
    {
        move(Vector2.down);
    }

    void moveRight()
    {
        move(Vector2.right);
    }

    void moveLeft()
    {
        move(Vector2.left);
    }

    //Moves camera in movable area.
    void move(Vector2 direction)
    {

        var max =(Vector2) movableArea.sprite.bounds.max;
        var min = (Vector2)movableArea.sprite.bounds.min;

        max *= movableArea.transform.localScale.x;
        min *= movableArea.transform.localScale.x;

        var vertExtent = cam.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;

        var camPos = (Vector2)transform.position+ (direction * speed);
        var camMin = new Vector2(camPos.x - horzExtent, camPos.y - vertExtent);
        var camMax= new Vector2(camPos.x + horzExtent, camPos.y + vertExtent);

       
        if (camMax.x > max.x || camMax.y > max.y)
        {
            //Debug.Log("cam max " + camMax);
            //Debug.Log("max " + max);
            return;
        }
        if (camMin.x < min.x || camMin.y < min.y)
        {
            //Debug.Log("cam min " + camMin);

            //Debug.Log("min " + min);
            return;

        }
        
        transform.position +=(Vector3)(direction * speed);
    }

    

    //inside class
    Vector2 firstPressPos;
    Vector2 secondPressPos;
    Vector2 currentSwipe;

    public void Swipe()
    {
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);
            if (t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x, t.position.y);
            }

            if (t.phase == TouchPhase.Moved)
            {
                //save ended touch 2d point
                secondPressPos = new Vector2(t.position.x, t.position.y);

                //create vector from the two points
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);

                //normalize the 2d vector
                currentSwipe.Normalize();

                Vector2 direction = new Vector2();

                //swipe upwards
                if (currentSwipe.y > 0  && currentSwipe.x > -0.5f &&  currentSwipe.x < 0.5f)
             {
                    //Debug.Log("up swipe");
                    //direction.y = 1;
                    moveDown();

                }
                //swipe down
                if (currentSwipe.y < 0  && currentSwipe.x > -0.5f  && currentSwipe.x < 0.5f)
             {
                    //Debug.Log("down swipe");
                    //direction.y = -1;
                    
                    moveUp();
                }
                //swipe left
                if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
             {
                    //Debug.Log("left swipe");
                    //direction.x = -1;
                    moveRight();

                }
                //swipe right
                if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
             {
                    //Debug.Log("right swipe");
                    //direction.x = 1;
                    
                    moveLeft();
                }
                
            }

            
            
        }
    }
}
