using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    private List<HighScoreRecord> highScoreRecords;

    // Start is called before the first frame update
    void Awake()
    {
        highScoreRecords = new List<HighScoreRecord>();
        highScoreRecords = ReturnLadderBoard();
    }

    public void RegisterScore(HighScoreRecord currentPlayerScore)
    {
        highScoreRecords.Add(currentPlayerScore);
        SortLadderBoard();
        SaveLadderBoard();
    }

    public void RegisterScore(string playerName, int playerScore)
    {
        highScoreRecords.Add(new HighScoreRecord(playerName, playerScore));
        SortLadderBoard();
        SaveLadderBoard();
    }

    public void SortLadderBoard()
    {
        highScoreRecords.Sort((p1, p2) => p2.playerScore.CompareTo(p1.playerScore));
    }

    public List<HighScoreRecord> ReturnLadderBoard()
    {
        List<HighScoreRecord> savedLadderBoard = new List<HighScoreRecord>();

        for (int i = 0; i < 5; i++)
        {
            HighScoreRecord tempHighScore = new HighScoreRecord(
                     PlayerPrefs.GetString("Player" + i, "BOY"),
                     PlayerPrefs.GetInt("Score" + i, 0)
                );


            savedLadderBoard.Add(tempHighScore);
        }

        return savedLadderBoard;
    }

    private void SaveLadderBoard()
    {
        for(int i=0; i<5; i++)
        {
            if(highScoreRecords[i] != null)
            {
                PlayerPrefs.SetString("Player" + i, highScoreRecords[i].playerName);
                PlayerPrefs.SetInt("Score" + i, highScoreRecords[i].playerScore);
            }       
        }
    }
}

