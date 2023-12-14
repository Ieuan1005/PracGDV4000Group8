using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public string target;
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot - 90);
    }
    void Update()
    {
        deleteOffScreen();
    }

    void deleteOffScreen()
    {
        if (transform.position.x < -9.0f || transform.position.x > 9.0f)
        {
            Destroy(gameObject);
        }

        if (transform.position.y < -5.0f || transform.position.y > 5.0f)
        {
            Destroy(gameObject);
        }
    }

    /*private void OnTriggerEnter2D(Collider2D collision) //page 7/11 workshop 4.2 implement 
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit player");
        }
    Dont think this is needed anymore
        if (collision.gameObject.CompareTag("Meteor"))
        {
            Destroy(gameObject);
            Debug.Log("hit meteor");
        }
    } */

  /*  void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(Bullet.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }

    ALL OF ABOVE IS HANDLED BY METEOR OR COLLISION MATRIX */

    // Update is called once per frame
    

    
}
