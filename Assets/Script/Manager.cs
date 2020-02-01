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
    public GameTimer gameTimerScript;
    public Animation gameOverAnimation;
    public BSODMessage message;
    public TextMeshProUGUI bsodText;
    public GameObject consoleGameobject;
    public TextMeshProUGUI stringaAlfanumerica;
    public TMP_InputField inputField;
    public bool consoleOn = false;
    string stringa1, stringa2;

    public void Start()
    {
        bsodText.text = message.messaggio[Random.Range(0, message.messaggio.Count)];
    }

    public void GameOver()
    {
        player.Pause();
        gameTimerScript.Stop();
        gameOverAnimation.Play();
    }

    public void Update()
    {
        if(status == GameStatus.GameOver && Input.anyKeyDown)
        {
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            ConsoleON();
        }
    }

    /// <summary>
    /// Questa funzione mette in pausa il player, apre la console e setta la nuova frase alafanumerica da scrivere
    /// </summary>
    public void ConsoleON()
    {
        player.Pause();
        string stringa1 = "PROVA";
        consoleGameobject.SetActive(true);
        stringaAlfanumerica.text = stringa1;
        inputField.Select();
        consoleOn = true;
    }

    /// <summary>
    /// Questa funzione chiude la console
    /// </summary>
    public void ConsoleOFF()
    {
        player.Go();
        consoleGameobject.SetActive(false);
        consoleOn = false;
    }

    /// <summary>
    /// Questa funzione viene chiamata ad ogni "On Value Changed" del input field...controlla che
    /// l'ultima lettera aggiunta alla stringa sia la medesima della stringa alfanumerica visualizzata nella console
    /// </summary>
    public void CheckPhrase()
    {
        string stringa2 = inputField.text;
        string lettera = stringa2.Substring(stringa2.Length - 1);
        if(lettera == stringa1.Substring(stringa2.Length-1))
        {
            //Controlliamo se la lunghezza della stringa1 è uguale alla stringa 2...se si, abbiamo completato la frase
            if(stringa1.Length == stringa2.Length)
            {
                //BRAVO! Aggiungi il tempo al timer e chiudi la console!
                //gameTimerScript.AddTime();
                ConsoleOFF();
            }
            return;
        }
        else
        {
            //Errore!!!
        }

    }
}
