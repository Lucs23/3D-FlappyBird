using System;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine.UIElements;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles;
    public float spawnDistance = 250f;
    public float spawnRate = 3f;
    public float speed = 2f;
    public float introSpawns = 2;
    public Color[] colors;
    public GameObject GameUi;

    private Label Score;
    private UIDocument uiDoc;

    private bool startSpawn = false;
    private int lastColor = 0;
    private float distanceChecker;
    private float timer = 0f;
    public int scoreCount = 0;
    void Start()
    {
        uiDoc = GameUi.GetComponent<UIDocument>();
        Score = uiDoc.rootVisualElement.Q("ScoreDisplay") as Label;

        distanceChecker = spawnDistance * introSpawns;
        for (int i = 1; i < introSpawns + 1; i++)
        {
            int randObstacle = UnityEngine.Random.Range(0, obstacles.Length);
            while (obstacles[randObstacle].activeSelf == true)
            {
                randObstacle = UnityEngine.Random.Range(0, obstacles.Length);
            }


            Vector3 temp = obstacles[randObstacle].transform.position;
            temp.z = spawnDistance * i;
            obstacles[randObstacle].transform.position = temp;


            int randColor = UnityEngine.Random.Range(0, colors.Length);
            while (lastColor == randColor)
            {
                randColor = UnityEngine.Random.Range(0, colors.Length);
            }
            Color obstacleColor = colors[randColor];
            lastColor = randColor;


            MeshRenderer[] childComps = obstacles[randObstacle].GetComponentsInChildren<MeshRenderer>();
            for (int j = 0; j < childComps.Length; j++)
            {
                childComps[j].material.SetColor("_BaseColor", obstacleColor);
            }


            obstacles[randObstacle].SetActive(true);
            Movement movementScript = obstacles[randObstacle].GetComponent<Movement>();

            movementScript.SetSpeed(speed);
        }

    }

    void startChecker()
    {
        timer += Time.deltaTime;
        float i = speed * timer;
        float j = distanceChecker - i;
        if (j < spawnDistance)
        {
            startSpawn = true;
            timer = 0;
            print("test");
        }
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("player");
        Collision collisionScript = player.GetComponent<Collision>();
        if (collisionScript.gameOver == false)
        {
            if (startSpawn)
            {
                timer += Time.deltaTime;
                if (timer >= spawnRate)
                {
                    int randObstacle = UnityEngine.Random.Range(0, obstacles.Length); //Get random obstacle.
                    while (obstacles[randObstacle].activeSelf == true) //Get another obstacle if obstacle is already spawned.
                    {
                        randObstacle = UnityEngine.Random.Range(0, obstacles.Length);
                    }
                    int randColor = UnityEngine.Random.Range(0, colors.Length); //Get random color.


                    while (lastColor == randColor) //Get another color if last spawned was the same color.
                    {
                        randColor = UnityEngine.Random.Range(0, colors.Length);
                    }


                    Color obstacleColor = colors[randColor]; //Link random color.
                    lastColor = randColor;



                    obstacles[randObstacle].SetActive(true); //Make it actually spawn.

                    Movement movementScript = obstacles[randObstacle].GetComponent<Movement>();
                    movementScript.SetSpeed(speed);


                    Vector3 temp = obstacles[randObstacle].transform.position; //Get the position of the obstacle.
                    temp.z = spawnDistance; //Change z to the spawn location.
                    obstacles[randObstacle].transform.position = temp; //Set is at the spawn location. 

                    MeshRenderer[] childComps = obstacles[randObstacle].GetComponentsInChildren<MeshRenderer>();
                    for (int i = 0; i < childComps.Length; i++)
                    {
                        childComps[i].material.SetColor("_BaseColor", obstacleColor);
                    }
                    timer = 0;
                }
            }
            else startChecker();
        }

        for (int i = 0; i < obstacles.Length; i++)
        {
            Vector3 temp = obstacles[i].transform.position;
            if (temp.z < -2)
            {
                temp = obstacles[i].transform.position;
                temp.z = 10;
                obstacles[i].transform.position = temp;
                obstacles[i].SetActive(false);
                scoreCount++;
                Score.text = scoreCount.ToString();
            }
        }

    }
}
