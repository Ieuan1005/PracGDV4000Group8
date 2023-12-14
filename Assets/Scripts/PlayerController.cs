using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] float speed = 1.0f;
    [SerializeField] private GameControllerScript hostGameController = null;
    [SerializeField] private Text scoreField;
    [SerializeField] private Text healthField;
    [SerializeField] private Text livesField;
    [SerializeField] private Text speedUpField;
    [SerializeField] private Text shieldField;
    [SerializeField] private int health = 100;
    [SerializeField] private GameObject shield;
    [SerializeField] private GameObject flames;
    [SerializeField] private float speedUpDuration = 1.5f;
    [SerializeField] private float speedUpCooldown = 10f;
    [SerializeField] private int shieldDuration = 3;
    [SerializeField] private int shieldCooldowntime = 20;
    private SpriteRenderer _spriteRenderer;
   
    private int enemyBulletDamage = 40;

    private int score = 0;
    
    private int lives = 3;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    
    private bool speedUpAvailable;

    private bool shielded;
    private bool shieldAvailable;
    void Start()
    {
        health = 100;
        shielded = false;
        shieldAvailable = true;
        score = 0;
        lives = 3;
        speedUpAvailable = true;
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        killPlayer();
        StartCoroutine(speedUp());
        updateLives();
        updateSpeedUp();
        healthCheck();
        rotatePlayer();
        checkShield();
        updateShield();
    }
      
    void movePlayer()
    {
        float leftRightInput = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float upDownInput = Input.GetAxis("Vertical") * speed * Time.deltaTime;

        /*Vector2 horizontalMoveVector = Vector2.right * leftRightInput;
        Vector2 vertcalMoveVector = Vector2.up * upDownInput;

        Vector2 moveVector = (horizontalMoveVector + vertcalMoveVector) * speed * Time.deltaTime; //dont need for now as other works fine and also clamps the movement to the playable area

        transform.Translate(moveVector);*/

        float boundX = Mathf.Clamp(transform.position.x + leftRightInput, minX, maxX);
        float boundY = Mathf.Clamp(transform.position.y + upDownInput, minY, maxY);

        transform.position = new Vector2(boundX, boundY);
    }

    void rotatePlayer()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }

    public void updateScore(int amount)
    {
        score += amount;
        scoreField.text = score.ToString();
    }

    public void updateHealth(int amount)
    {
        if (!shielded)
        {
            health -= amount;
            healthField.text = health.ToString();
        }
    }

    public void updateLives()
    {
        livesField.text = lives.ToString();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            Debug.Log("Player hit by enemy bullet");
            Destroy(collision.gameObject);
            if (!shielded)
            {
                if (hostGameController)
                    hostGameController.updatePlayerHealth(enemyBulletDamage);
            }
        }
    }

    void healthCheck() //bad but quick fix for the health text not updating correctly as the health value updates but the UI doesnt? shouldnt cause any issues so use for now
    {
        if (healthField.text == "0")
        {
            healthField.text = "100";
        }
        if (healthField.text == "-10")
        {
            healthField.text = "100";
        }
        if(healthField.text == "-20")
        {
            healthField.text = "100";
        }
        if (healthField.text == "-30")
        {
            healthField.text = "100";
        }
    }

    void killPlayer()
    {
        if (health <= 0)
        {
            health = 100;
            lives--;
            transform.position = new Vector2(0f, -3.0f); //respawns the player at the bottom of the screen
        }

        if (lives <= 0) 
        {
            Destroy(gameObject);
        }

    }

    private IEnumerator speedUp() //look at asteroid generator script to try and use the countdown code here?
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (speedUpAvailable == true)
            {
                Debug.Log("CTRL Key Pressed");
                speed *= 1.5f;
                speedUpAvailable = false;
                flames.SetActive(true);
                yield return new WaitForSeconds(speedUpDuration);
                speed = 5f;
                flames.SetActive(false);
                Debug.Log("Speed Returned to 3, 10s cooldown started");
                yield return new WaitForSeconds(speedUpCooldown);
                speedUpAvailable = true;
                Debug.Log("SpeedUp is available");
                
            }

            if (speedUpAvailable == false)
            {
                Debug.Log("SpeedUp is on cooldown " + speedUpCooldown);
            }
        }
    }

    void checkShield()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl)&&shieldAvailable)
        {
            shield.SetActive(true);
            shielded = true;
            shieldAvailable = false;
            Invoke("NoShield", shieldDuration);
        }
    }

    void NoShield()
    {
        shield.SetActive(false);
        shielded = false;
        Invoke("shieldCooldown", shieldCooldowntime);
    }

    void shieldCooldown()
    {
        shieldAvailable = true;
    }

    void updateSpeedUp()
    {
        if (speedUpAvailable)
        {
            speedUpField.text = "Ready";
        }

        else
        {
            speedUpField.text = "Not Ready";
        }
    }

    void updateShield()
    {
        if (shieldAvailable)
        {
            shieldField.text = "Ready";
        }

        else
        {
            shieldField.text = "Not Ready";
        }
    }
    
}
