using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameStatus { Play, GameOver, Console};
public class Manager : MonoBehaviour
{
    public GameStatus status;
    public InputField inp;
    public AudioClip music, soundError;
    public AudioSource aSource;
    public PlayerMovement player;
    public GameTimer gameTimerScript, timerConsole, hiddenTimer;
    public Animation gameOverAnimation;
    public BSODMessage message;
    public TextMeshProUGUI bsodText;
    public GameObject consoleGameobject;
    public bool gameOver = false;
    public Vector2[] difficulty;
    public DifficultyScript difficultyScript;
    public HandsManager handManager;
    public Animator manateAnim;
    [HideInInspector]
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
        timerConsole.Stop();
        player.Pause();
        gameTimerScript.Stop();
        aSource.clip = soundError;
        aSource.loop = false;
        gameOverAnimation.Play();
        if(consoleGameobject.activeSelf)
        {
            consoleGameobject.SetActive(false);
        }
        gameOver = true;
    }

    public void Update()
    {
        if(consoleGameobject.activeSelf)
        {
            inp.ActivateInputField();
        }

        if (player.isGRound && timerOff)
        {
            ConsoleON();
        }

        if (status == GameStatus.GameOver && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }

        if(consoleGameobject.activeSelf)
        {
            if(Input.anyKeyDown)
            {
                handManager.MoveHand();
            }
        }
    }

    /// <summary>
    /// Questa funzione mette in pausa il player, apre la console e setta la nuova frase alafanumerica da scrivere
    /// </summary>
    public void ConsoleON()
    {
        if (player.isGRound && timerOff)
        {
            timerConsole.Stop();
            player.Pause();
            manateAnim.SetTrigger("Manate");
            //Console ON
            consoleGameobject.SetActive(true);
            consoleGameobject.GetComponentInChildren<SequenceCheckerScript>().secondsOfPlay = (int) Mathf.Round(hiddenTimer.GetTimeAmount());
            consoleGameobject.GetComponentInChildren<SequenceCheckerScript>().Setup();
            timerOff = false;
        }
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
