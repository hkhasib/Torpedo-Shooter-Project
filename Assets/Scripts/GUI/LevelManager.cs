using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : TorpedoShooter
{
    
    public static int score = 0, playerLife=20;
    public static bool gamePause = false, levelComplete = false, gameOver = false;
    public Text scoreText, lifeText;
    public GameObject pauseMenuPanel, gameOverPanel, levelCompletePanel;
    void Start()
    {
        pauseMenuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        scoreText.text = "Score: " + score;
        lifeText.text = "Health: " + playerLife;
        if ((gamePause && !gameOver)&&!levelComplete)
        {
            enablePauseMenu();
            
        }
        else if(!gamePause)
        {
            disablePauseMenu();
        }
        if(levelComplete)
        {
            levelCompleteMenu();
        }
        if (gameOver)
        {
            pause();
            gameOverPanel.SetActive(true);
        }
    }


    IEnumerator waitToFinish()
    {
        yield return new WaitForSeconds(2.5f);
        levelComplete= true;
    }

    public void enablePauseMenu()
    {
        gamePause = true;
        pauseMenuPanel.SetActive(true);
        pause();
    }
    public void disablePauseMenu()
    {
        gamePause= false;
        pauseMenuPanel.SetActive(false);
        resume();
    }

    void pause()
    {
        Time.timeScale= 0;
    }
    private void resume()
    {
        Time.timeScale = 1;
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void restartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameOver = false;
        gameOverPanel.SetActive(false);
        resume();
        
    }

    void levelCompleteMenu()
    {
        levelCompletePanel.SetActive(true);
        pause();
        
    }

    public void levelCompleteMenuAction()
    {
        levelComplete = false;
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("Menu");
        }
        levelCompletePanel.SetActive(false);
        
        resume();
    }
}
