using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScenesScript : MonoBehaviour
{
    private TMPro.TMP_InputField nameInputField;
    private bool isInserting = false;
    // Start is called before the first frame update
    void Start()
    {
        nameInputField = gameObject.GetComponentInChildren<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) )
        {
            // Premuto enter
            if (!isInserting)
            {
                PlayerPrefs.SetString("Name", nameInputField.text);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameSCene");
            }
            else
            {
                Debug.Log(nameInputField.text);
                isInserting = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.L) && !isInserting)
        {
            SceneManager.LoadScene("LeaderboardScene");
        }


        if (Input.GetKeyDown(KeyCode.Escape) && !isInserting)
        {
            isInserting = true;
            nameInputField.Select();
            Debug.Log("Escape Pressed");
            nameInputField.text = "";

        }

        if (isInserting)
        {
            nameInputField.ActivateInputField();
        }

    }
}
