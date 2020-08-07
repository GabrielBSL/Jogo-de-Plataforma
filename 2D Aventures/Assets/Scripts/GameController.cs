using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public int totalScore;
    public Text scoreText;

    public GameObject gameOver;

    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Scene currentScene = SceneManager.GetActiveScene();
        string currentName = currentScene.name;

        if (currentName == "Main_Menu")
        {
            PlayerPrefs.SetInt("Score", 0);
        }

        else
        {
            totalScore = PlayerPrefs.GetInt("Score");
            scoreText.text = totalScore.ToString();
        }
    }

    public void UpdateScore(int score)
    {
        totalScore += score;
        scoreText.text = totalScore.ToString();
    }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void LoadScene(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void NextScene(string levelName)
    {
        PlayerPrefs.SetInt("Score", totalScore);
        SceneManager.LoadScene(levelName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
