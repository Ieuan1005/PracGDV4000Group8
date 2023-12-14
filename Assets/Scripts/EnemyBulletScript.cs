using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
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

    /*void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy bullet");
            Destroy(gameObject);
        }
    }*/
}
