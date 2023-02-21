using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject gameoverScreen;
    public GameObject gameStartScreen;
    public bool isLevelFinish;
    public bool isSwipeToStart;
    public PlayerController playerController;

    private void Awake()
    {
        instance = this;
        StopGame();
        gameoverScreen.SetActive(false);
        //playerController.AddBoatStart();
    }

    private void Update()
    {
        SwipeToStart();
    }

    private void SwipeToStart()     // swip method to control the player
    {
        if (/*!isSwipeToStart && */Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            StartGame();
        }
        else if (/*!isSwipeToStart &&*/ Input.GetMouseButton(0))
        {
            StartGame();
        }

        isSwipeToStart = true;
    }

    public void StartGame()        // start game method
    {
        Time.timeScale = 1.0f;
        gameStartScreen.SetActive(false);   // actibe the start screen 
    }

    public void LoseGame()      // lose game method
    {
        gameoverScreen.SetActive(true); // active the game over screen
        StopGame();
    }

    public void StopGame()      // stop game method
    {
        Time.timeScale = 0.0f;
    }
}
