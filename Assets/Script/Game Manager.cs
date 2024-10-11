using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour

{
    public GameObject GameOverObject;
   public bool GameOvers = false;

  public void Update()
    {
        if (GameOvers && Input.GetKeyDown(KeyCode.Mouse1))
        {
           TryAgain();
        }
    }

    public void GameOver()
    {
        GameOvers = true;
        Debug.Log("Game Over!");
    }

  public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}