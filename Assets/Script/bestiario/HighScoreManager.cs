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
    }

    public void RegisterScore(HighScoreRecord currentPlayerScore)
    {
        highScoreRecords.Add(currentPlayerScore);
        SortLadderBoard();
    }

    public void SortLadderBoard()
    {
        highScoreRecords.Sort((p1, p2) => p2.playerScore.CompareTo(p1.playerScore));
    }

    public List<HighScoreRecord> ReturnLadderBoard()
    {
        return highScoreRecords;
    }
}

public class HighScoreRecord
{
    public string playerName;
    public int playerScore;

    public HighScoreRecord(string playerName, int playerScore)
    {
        this.playerName = playerName;
        this.playerScore = playerScore;
    }
}
