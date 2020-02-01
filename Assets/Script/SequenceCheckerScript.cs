using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SequenceCheckerScript : MonoBehaviour
{

    // Variabili private
    private bool end = false;
    private string sequenceString;

    // Variabili inspectable da editor
    [SerializeField]
    private Text promptField;
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
    private int secondsOfPlay = 0;
    [SerializeField]
    [Tooltip("Il valore minimo della lunghezza della stringa quando la partita è iniziata da meno di 60 secondi")]
    private int rangeUnder60 = 8;
    [SerializeField]
    [Tooltip("Il valore minimo della lunghezza della stringa quando la partita è iniziata da più di 60 secondi, ma meno di 120")]
    private int range61_120 = 15;
    [SerializeField]
    [Tooltip("Il valore minimo della lunghezza della stringa quando la partita è iniziata da più di 120 secondi")]
    private int rangeOver120 = 21;

    // Start is called before the first frame update
    void Start()
    {

        // Randomize Initial State
        int millis = System.DateTime.UtcNow.Millisecond + 1;
        int secs = System.DateTime.UtcNow.Second + 1;
        int initState = secs * millis * playerCode;

        Random.InitState(initState);


        int numberOfChars = Random.Range(0,2);
        if (secondsOfPlay < 60)
        {
            // Meno di 60 secondi
            numberOfChars += rangeUnder60;
        }
        else if (secondsOfPlay < 120)
        {
            // Meno di 120 secondi
            numberOfChars += range61_120;
        }
        else
        {
            numberOfChars += rangeOver120;
        }
        RandomizeString(numberOfChars);
        promptField.text = sequenceString;
    }

    // Update is called once per frame
    void Update()
    {
        if (inputField.text != null && !end)
        {
            if (promptField.text.Equals(inputField.text))
            {
                // Stringa corretta, non premere invio e andare avanti
                Debug.Log("Code is correct, fixing the software...");
                end = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.Return) && !end)
        {
            if (!promptField.text.Equals(inputField.text))
            {
                // Stringa corretta, non premere invio e andare avanti
                // Debug.Log("Wrong Code, please retry: code is:\n " + sequenceString);
                inputField.placeholder.GetComponent<Text>().text = "Wrong code, please retry";
                inputField.text = "";
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

    void RandomizeString(int numberOfChars)
    {
        char[] array = new char[numberOfChars];
        
        // Populate Char Array
        for (int i = 0; i < numberOfChars; i++)
        { 
            array[i] = (char)Random.Range(48, 57); 
        }

        // Compose the resulting string
        sequenceString = string.Concat<char>(array);
    }

}
