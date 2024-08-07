using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool ballSpawnTrigger1, ballSpawnTrigger2;

    // Start is called before the first frame update
    void Start()
    {
        ballButtonController = ballButton.GetComponent<ButtonController>();
        clearButtonController = clearButton.GetComponent<ButtonController>();
        clear = false;
        ballSpawnTrigger1 = false;
        ballSpawnTrigger2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ballButtonController.Switch && !ballSpawnTrigger1)
        {
            ballSpawnTrigger1 = true;
            ballSpawnTrigger2 = true;
            StartCoroutine(ResetBallSpawnTrigger1());
        }
        if (clearButtonController.Switch)
        {
            clear = true;
        }
        if (ballSpawnTrigger2)
        {
            SpawnBall();
        }
        if (clear)
        {
            ballButtonController.Switch = true;
            clearButtonController.Switch = true;
        }


    }
    private IEnumerator ResetBallSpawnTrigger1()
    {
        yield return new WaitForSeconds(3f);
        ballSpawnTrigger1 = false;
    }

    private void SpawnBall()
    {
        Instantiate(ballObject, ballSpawnPosition.position, ballSpawnPosition.rotation);
        ballSpawnTrigger2 = false;
    }

}