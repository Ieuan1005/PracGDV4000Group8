using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private int points = 10;
    [SerializeField] private int damage = 30;
    [SerializeField] private int health = 100;
    [SerializeField] private float moveSpeed = 1.0f;
    [SerializeField] private int enemyNumber;
    //[SerializeField] private float rotationSpeed = 90.0f;
    private GameControllerScript hostGameController = null;
    Transform target;
    Vector2 moveDirection;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            target = GameObject.Find("Player").transform; //used so game doesnt crash when the player dies and the game object is destroyed
        }
        catch
        {
            Debug.Log("Player has died");
        }
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        getDestroyed();
        deleteOffScreen();
    }

    void movement()
    {

        if (enemyNumber == 1)
        {
            if (target)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
                transform.up = target.transform.position - transform.position;

                /* Vector3 direction = (target.position - transform.position).normalized; //old way used to chase player but was no where near as good, smooth or functional
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
                moveDirection = direction;
                rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed; */
            }
        }
        if (enemyNumber == 2)
        {
            //transform.Rotate(0.0f, 0.0f, rotationSpeed * Time.deltaTime); This code is old code to make the meteors fall down from the top of the sceen
            transform.Translate(Vector2.down * moveSpeed * Time.deltaTime, Space.World); //use Vector2.left for side ways
        }
        /*else
        {
            Debug.Log("Select an enemyNumber");
        }*/
    }

    /*private void FixedUpdate()
    {
        if (target)
        {
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * moveSpeed;
        }
        
    }*/

    public GameControllerScript HostGameController
    {
        get { return hostGameController; }
        set { hostGameController = value; }
    }

    void OnTriggerEnter2D(Collider2D collision) //player is set to trigger, collisions is detected, need to add destroy(gameobject)?
    { //page 7/11 workshop 4.2 implement

        if (collision.tag == "Player")
        {
            Debug.Log("crash player");
            if (hostGameController)
                hostGameController.updatePlayerHealth(damage);
            Destroy(gameObject);
            //Destroy(collision.gameObject);
        }

        /* if (collision.gameObject.CompareTag("Meteor"))
           {
               Debug.Log("crash meteor");
               Destroy(gameObject);
               Destroy(collision.gameObject);
           } */

        if (collision.gameObject.CompareTag("Bullet"))
        {
            Debug.Log("Meteor was hit by a bullet");
            if (hostGameController)
                hostGameController.updatePlayerScore(points);
            //Destroy(gameObject);
            Destroy(collision.gameObject);
            health = health - 70;
        }
    }

    void getDestroyed()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void deleteOffScreen()
    {
        if (transform.position.x < -9.0f || transform.position.x > 9.0f)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < -5.0f)
        {
            Destroy(gameObject);
        }
    }


    public int Points
    {
        get { return points;  }
    }
}
