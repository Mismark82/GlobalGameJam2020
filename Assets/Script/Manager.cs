using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public enum GameStatus { Play, GameOver};
public class Manager : MonoBehaviour
{
    public GameStatus status;
    public PlayerMovement player;
    public GameTimer gameTimerScript, timerConsole;
    public Animation gameOverAnimation;
    public BSODMessage message;
    public TextMeshProUGUI bsodText;
    public GameObject consoleGameobject;
    public Transform gameOverPoint;
    public bool gameOver = false;
    public Vector2[] difficulty;
    public DifficultyScript difficultyScript;
    public bool timerOff = false;
    int level = 0;

    public void Start()
    {
        InvokeRepeating("SetDifficulty", 60f, 60f);
        gameOver = false;
        bsodText.text = message.messaggio[Random.Range(0, message.messaggio.Count)];
    }

    public void GameOver()
    {
        player.Pause();
        gameTimerScript.Stop();
        gameOverAnimation.Play();
        gameOver = true;
    }

    public void Update()
    {
        if (player.isGRound && timerOff)
        {
            ConsoleON();
        }

        if (status == GameStatus.GameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }

        if(player.transform.position.y < gameOverPoint.position.y && !gameOver)
        {
            GameOver();
        }
    }

    /// <summary>
    /// Questa funzione mette in pausa il player, apre la console e setta la nuova frase alafanumerica da scrivere
    /// </summary>
    public void ConsoleON()
    {
        if (player.isGRound && timerOff)
            timerConsole.Stop();
        player.Pause();
        //Console ON
        consoleGameobject.SetActive(true);
        timerOff = false;
    }

    /// <summary>
    /// Questa funzione chiude la console
    /// </summary>
    public void ConsoleOFF()
    {
        difficultyScript.SetTimer();
        timerConsole.Resume();
        player.Go();
        SequenceCheckerScript script = (SequenceCheckerScript)consoleGameobject.GetComponentInChildren<SequenceCheckerScript>();
        script.Setup();
        consoleGameobject.SetActive(false);
    }

    public void SetDifficulty()
    {
        if(level == 0)
        {
            difficultyScript.minTime = difficulty[0].x;
            difficultyScript.maxTime = difficulty[0].y;
        }
        else if (level == 1)
        {
            difficultyScript.minTime = difficulty[1].x;
            difficultyScript.maxTime = difficulty[1].y;
        }
        else
        {
            difficultyScript.minTime = difficulty[2].x;
            difficultyScript.maxTime = difficulty[2].y;
        }
    }

    public void TimerOff()
    {
        timerOff = true;
    }
}
