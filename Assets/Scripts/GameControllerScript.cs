using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    [SerializeField] private PlayerController player = null; //workshop 15-16, word workshop 4.2

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updatePlayerScore(int amount)
    {
        player.updateScore(amount);
    }

    public void updatePlayerHealth(int amount) 
    {
        player.updateHealth(amount);
    }
}
