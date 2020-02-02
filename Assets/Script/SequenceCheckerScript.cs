using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.Events;



public class SequenceCheckerScript : MonoBehaviour
{

    // Stringhe per i possibili codici di errore
    
    private readonly string[] errorStrings = { 
                                                "Kernel Panic! Oh no! I'm the Kernel. I'm panicking! HEEEEEELP!",
                                                "Illegal pointer at address 0xF00D.",
                                                "Die Jedi Dogs! Oh, what did I say?",
                                                "Cookies number less than minimum reserve.",
                                                "Take a break now. Devs need to sleep. I'm worried for you.",
                                                "This game will never deliver in time.",
                                                "Write error. Abort, Retry, Fail?",
                                                "Unrecognized rgb color #COFFEE, did you mean #C0FFEE?",
                                                "Software failure. Guru Meditation #00000004.#0000AAC0",
                                                "Does not compute.",
                                                "I've got this terrible pain in all the diodes down my left side.",
                                                "I think you ought to know I'm feeling very depressed.",
                                                "SPAAAAAAAAAAAAAAACEEEE!!!!!",
                                                "The cake is a lie!",
                                                "Seriously: this is not a bug. It's an undocumentend feature.",
                                                ""
                                            };

    // Variabili private
    private bool end = false;
    private string sequenceString;
    private int numberOfChars;

    [SerializeField]
    private float time = 10f;



    // Variabili inspectable da editor
    [SerializeField]
    private Text promptField;
    [SerializeField]
    private Text timeField;
    [SerializeField]
    private InputField inputField;
    [SerializeField]
    private Text errorField;
    //public List<KeyCode> sequenceToCheck = new List<KeyCode>();
    [SerializeField]
    [Tooltip("Player Code é usato per inizializzare il random, verrà fuori dal nome inserito dal giocatore")]
    private int playerCode = 574;
    [SerializeField]
    [Tooltip("Il numero di secondi dall'inizio della partita")]
    public int secondsOfPlay = 0;
    [SerializeField]
    [Tooltip("Il valore minimo della lunghezza della stringa quando la partita è iniziata da meno di 60 secondi")]
    private int range1 = 4;
    [SerializeField]
    [Tooltip("Il valore minimo della lunghezza della stringa quando la partita è iniziata da più di 60 secondi, ma meno di 120")]
    private int range2 = 5;
    [SerializeField]
    [Tooltip("Il valore minimo della lunghezza della stringa quando la partita è iniziata da più di 120 secondi")]
    private int range3 = 6;

    //public delegate void StringRecognized();
    //public static event StringRecognized OnRecognize;
    public UnityEvent onRecognize;

    //public delegate void TimeIsUp();
    //public static event TimeIsUp OnTimeUp;
    public UnityEvent onTimeUp;

    // Start is called before the first frame update
    void Start()
    {
        // Randomize initial state on script start
        int millis = System.DateTime.UtcNow.Millisecond + 1;
        int secs = System.DateTime.UtcNow.Second + 1;
        int initState = secs * millis * playerCode;

        UnityEngine.Random.InitState(initState);

        // Will have to be commented later on
        Setup();
    }

    
    // Update is called once per frame
    void Update()
    {
        if (inputField.text != null && !end)
        {
            if (promptField.text.Equals(inputField.text.ToUpper()))
            {
                // String is correct, instant validation and return
                Debug.Log("Code is correct, fixing the software...");
                end = true; 
                inputField.text = "";
                onRecognize.Invoke();
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) && !end)
        {
            if (!promptField.text.Equals(inputField.text.ToUpper()))
            {
                // Stringa corretta, non premere invio e andare avanti
                // Debug.Log("Wrong Code, please retry: code is:\n " + sequenceString);
                inputField.placeholder.GetComponent<Text>().text = "Wrong code, please retry";
                inputField.text = "";
            }
        }

        if (!end)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                time = 0;
                timeField.text = time.ToString("0");
                end = true;
                onTimeUp.Invoke();
                Debug.Log("Time up!");
                time = 10f;
            }
            else
            {
                timeField.text = time.ToString("0");
            }
           
        }
        /*
        KeyCode charToControl = KeyCode.None;
        // Controllare la sequenza qui
        if (m_sequencePosition < sequenceToCheck.Count)
        {
            charToControl = sequenceToCheck[m_sequencePosition];
            field.text = charToControl.ToString();
        }
        if (Input.GetKeyUp(charToControl))
        {
            Debug.Log("Inserito carattere corretto :" + charToControl);
            m_sequencePosition++;
        }
        if (m_sequencePosition == sequenceToCheck.Count && !end)
        {
            // la sequenza é terminata e devo chiudere il minigioco
            Debug.Log("Sequenza completata correttamente");
            field.text = "FINE";
            end = true;
        }
        */
    }

    public void Setup() 
    {
        SetFocusOnInput();
        SelectCharNumber();
        RandomizeString();
        RandomizeErrorString();
        promptField.text = sequenceString;
        inputField.text = "";
        end = false;
        time = 10f;
    }

    private void RandomizeErrorString()
    {
        string error = "ERROR!\nYour code has encountered a critical bug.\n";
        error += errorStrings[UnityEngine.Random.Range(0,errorStrings.Length)] + "\n";
        error += "Jump to the safe memory area below or the system stability will be compromised!\n";
        errorField.text = error;
    }

    private void SetFocusOnInput()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }

    private int SelectCharNumber()
    {
        //numberOfChars = UnityEngine.Random.Range(0, 2);
        if (secondsOfPlay < 30)
        {
            // Meno di 30 secondi
            numberOfChars = range1;
        }
        else if (secondsOfPlay < 60)
        {
            // Meno di 60 secondi
            numberOfChars = range2;
        }
        else
        {
            // più di 60 secondi
            numberOfChars = range3;
        }
        return numberOfChars;
    }

    private void RandomizeString()
    {
        char[] array = new char[numberOfChars];
        
        // Populate Char Array
        for (int i = 0; i < numberOfChars; i++)
        {
            int x = UnityEngine.Random.Range(48, 63);
            //array[i] = (char)UnityEngine.Random.Range(48, 57);
            if (x > 57) 
            { 
                x += 7;
            }
            array[i] = (char)x;
            
        }

        // Compose the resulting string
        sequenceString = string.Concat<char>(array);
    }

    public float GetTime
    {
        get { return time; }
    }
}
