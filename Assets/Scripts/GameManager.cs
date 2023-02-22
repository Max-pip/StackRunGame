using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameoverScreen;
    [SerializeField] private GameObject _gameStartScreen;
    [SerializeField] private PlayerController playerController;

    private void Awake()
    {
        StopGame();
        _gameoverScreen.SetActive(false);
    }

    private void Update()
    {
#if UNITY_EDITOR
        SwipeToStart();
#endif

#if UNITY_ANDROID
        SwipeToStartMobile();
#endif
    }

    private void SwipeToStart()
    {
        /*if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            StartGame();
        }
        else*/ if (Input.GetMouseButton(0))
        {
            StartGame();
        }
    }

    private void SwipeToStartMobile()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            StartGame();
        }
    }

    public void StartGame() 
    {
        Time.timeScale = 1.0f;
        _gameStartScreen.SetActive(false); 
    }

    public void StopGame() 
    {
        Time.timeScale = 0.0f;
    }
}
