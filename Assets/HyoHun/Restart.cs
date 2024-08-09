using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private StarterAssetsInputs _input;

    private void Awake()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        RestartScene();
    }


    private void RestartScene()
    {
        if (_input.restart)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
