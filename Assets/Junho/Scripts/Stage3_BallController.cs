using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage3_BallController : MonoBehaviour
{
    public GameObject ballButton;
    public GameObject clearButton;

    public GameObject ballObject;

    public Transform ballSpawnPosition;

    private ButtonController ballButtonController;
    private ButtonController clearButtonController;
    private bool clear;
    private bool ballSpawnTrigger;

    // Start is called before the first frame update
    void Start()
    {
        ballButtonController = ballButton.GetComponent<ButtonController>();
        clearButtonController = clearButton.GetComponent<ButtonController>();
        clear = false;
        ballSpawnTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballSpawnTrigger)
        {
            SpawnBall();
        }
        if (clear)
        {
            ballButtonController.Switch = true;
            clearButtonController.Switch = true;
        }

    }
    private void SpawnBall()
    {
        Instantiate(ballObject, ballSpawnPosition.position, ballSpawnPosition.rotation);
        ballSpawnTrigger = false; // Reset switch to avoid continuous spawning
    }

}