using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour

{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab1;
    public Transform[] spawnPoints;
    public GameObject gameOverPanel;
    public Button tryAgainButton;
    public bool GameOvers = false;
    private void Start()
    {

        gameOverPanel.SetActive(false);
        tryAgainButton.onClick.AddListener(RestartGame);
        InvokeRepeating("SpawnEnemy", 2f, 3f);
    }
    private void Update()
    {
        if (GameOvers && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    private void SpawnEnemy()
    {
        if (!GameOvers && spawnPoints.Length > 0)
        {


            int spawnIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(enemyPrefab, spawnPoints[spawnIndex].position, Quaternion.identity);
            Instantiate(enemyPrefab1, spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
    public void GameOver()
    {
        if (!GameOvers) { 
        Debug.Log("Game Over!");
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
        GameOvers = true;
    }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;


        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
