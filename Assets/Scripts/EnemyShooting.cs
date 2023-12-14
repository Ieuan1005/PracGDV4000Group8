using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyShooting : MonoBehaviour
{
    public Transform player;
    public GameObject bullet;

    private float shotCooldown;
    public float startShotCooldown;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            player = GameObject.Find("Player").transform; //used so game doesnt crash when the player dies and the game object is destroyed
        }
        catch
        {
            Debug.Log("Player has died");
        }

        shotCooldown = startShotCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = new Vector2(player.position.x - transform.position.x, player.position.y - transform.position.y);
        transform.up = direction;

        if (shotCooldown <= 0 )
        {
            Instantiate(bullet, transform.position, transform.rotation);
            shotCooldown = startShotCooldown;
        }
        else
        {
            shotCooldown -= Time.deltaTime;
        }
    }
}
