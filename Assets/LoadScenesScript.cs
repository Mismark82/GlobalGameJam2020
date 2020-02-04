using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadScenesScript : MonoBehaviour
{
    public string[] scores;
    public HighScoreManager managerScore;
    private TMPro.TMP_InputField nameInputField;
    private bool isInserting = false;
    private bool isOnLeaderboard = false;
    public GameObject oggettiDaSpegnere1, oggettiDaSpegnere2;
    public GameObject leaderBoardObj;
    // Start is called before the first frame upd[]ate
    void Start()
    {
        var ladder = managerScore.ReturnLadderBoard();
        var testi = leaderBoardObj.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < 5; i++)
        {
            scores[i] = ladder[i].playerName + "  " + ladder[i].playerScore;
            testi[i].text = scores[i];
        }
        nameInputField = gameObject.GetComponentInChildren<TMP_InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) && !isOnLeaderboard)
        {
            // Premuto enter
            if (!isInserting)
            {
                PlayerPrefs.SetString("Name", nameInputField.text);
                PlayerPrefs.Save();
                SceneManager.LoadScene("GameScene");
            }
            else
            {
                Debug.Log(nameInputField.text);
                isInserting = false;
            }
        }

        //Accendiamo la leaderboard
        if (Input.GetKeyDown(KeyCode.H) && !isInserting)
        {
            if(!isOnLeaderboard)
            {
                oggettiDaSpegnere1.SetActive(false);
                oggettiDaSpegnere2.SetActive(false);
                leaderBoardObj.SetActive(true);
                isOnLeaderboard = true;
            }
            else
            {
                oggettiDaSpegnere1.SetActive(true);
                oggettiDaSpegnere2.SetActive(true);
                leaderBoardObj.SetActive(false);
                isOnLeaderboard = false;
            }
        }


        if (Input.GetKeyDown(KeyCode.L) && !isInserting)
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
