using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{

    private float Min_X = -2.2f, Max_X = 2.2f; // setting the boundaries
    public bool Can_Move;
    private float move_speed = 2f;

    private Rigidbody2D myBody;
    private bool gameOver; 
     private bool ignoreCollision;
    private bool ignoreTrigger;
     void Awake()
    {
        myBody = GetComponent<Rigidbody2D>(); // getting the reference  to the component rigidbody 
        myBody.gravityScale = 0f; // stopping the box from falling down while moving left to right 

    }

    // Start is called before the first frame update
    void Start()
    {
        //Allowing the box to move 
        Can_Move = true; 
        //randomising the left and right movement 
        if(Random.Range(0,2)>0)
        {
            move_speed *= -1;    
        }
        GamePlayComtroller.instance.currentBox = this; //reference to the current box;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBox();
    }
    void MoveBox()
    {
        if (Can_Move)
        {
            Vector3 temp = transform.position;//getting the position of moving 

            temp.x += move_speed * Time.deltaTime; // for smoothning of the movement 
            if (temp.x > Max_X)
            {
                move_speed *= -1;  //changing directions while speed is comnstant 
            }
            else if (temp.x < Min_X)
            {
                move_speed *= -1; // changing directions while speed is constant
            }

            transform.position = temp;
        }
    }
    public void DropBox()
    {
        Debug.Log("hii");
        Can_Move = false ; //stopping the movement 
       
        myBody.gravityScale = Random.Range(2, 4); // adding the gravity
    }

    void Landed()
    {
        if(gameOver)
        {
            return;
        }

        ignoreCollision = true;
        ignoreTrigger = true;
        GamePlayComtroller.instance.SpawnNewBox();
        GamePlayComtroller.instance.MoveCamera();
    }
    void RestartGame()
    {
        GamePlayComtroller.instance.RestartGame();
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if (ignoreCollision)
            return;
        if (target.gameObject.tag == "Platform"|| target.gameObject.tag == "Box")
        {
            Invoke("Landed", 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (ignoreTrigger)
            return;
        if(target.tag=="GameOver")
        {
            CancelInvoke("Landed");
            gameOver = true;
            ignoreTrigger = true;

            Invoke("RestartGame", 2f);
        }
    }
}
