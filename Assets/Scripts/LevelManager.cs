using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void Restart()
    {
        string currentScaneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScaneName);
    }
}
