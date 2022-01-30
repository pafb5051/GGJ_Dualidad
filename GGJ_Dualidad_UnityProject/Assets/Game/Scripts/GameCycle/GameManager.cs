using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public InputHandler inputHandler;
    public CameraController cameraController;
    public PlayerController playerController;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    private void OnDestroy()
    {
        if (_instance != null && _instance != this)
        {
            _instance = null;
        }
    }

    private void Start()
    {
        inputHandler = InputHandler.Instance;
        cameraController = CameraController.Instance;
        StartGame();
    }


    public void StartGame()
    {
        inputHandler.SetActive(true);
        cameraController._started = true;
    }

    public void EndGame()
    {
        inputHandler.SetActive(false);
    }

}

