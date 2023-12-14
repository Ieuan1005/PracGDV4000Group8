using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AsteroidGenerator : MonoBehaviour
{
    [SerializeField] private GameControllerScript hostGameController = null;
    [SerializeField] private GameObject[] asteroidTypes = new GameObject[2] { null, null }; // add more for the other enemies when needed
    [SerializeField] private float asteroidSpawnTime = 1.0f;
    private float countdown = 0.0f;

    System.Random rndGen;

    // Start is called before the first frame update
    void Start()
    {
        rndGen = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;

        if (countdown <= 0.0f)
        {
            generateAsteroid();
            countdown = asteroidSpawnTime;
        }

        void generateAsteroid()
        {
            int typeIndex = rndGen.Next(2); //update to the amount of enemies

            //GameObject asteroid = Instantiate(asteroidTypes[typeIndex], new Vector2(10f, (float)(rndGen.Next(10) - 5)), Quaternion.identity); use for side shooter
            GameObject asteroid = Instantiate(asteroidTypes[typeIndex], new Vector2((float)(rndGen.Next(10) - 5), 7f), Quaternion.identity); //use for vertical shooter note need to set asteroids to move down instead of sideways

            var asteroidControllerComponent = asteroid.GetComponent<AsteroidController>();
            asteroidControllerComponent.HostGameController = this.hostGameController;

        }
    }

}
