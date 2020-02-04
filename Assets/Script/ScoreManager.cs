using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro scoreText;

    private Transform currentPlayerPosition;
    public Transform currentPlayer1Position;
    public Transform currentPlayer2Position;
    private float prevPlayerPosition;
    public int playerScore;

    [SerializeField]
    private int scoreMultiply;
    private float playerDelta;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerPosition = PlayerPrefs.GetInt("Player", 1) == 1 ? currentPlayer1Position : currentPlayer2Position;
        prevPlayerPosition = currentPlayerPosition.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //playerDelta += currentPlayerPosition.position.y - prevPlayerPosition;

        /*if(currentPlayerPosition.position.y > prevPlayerPosition + 2)
        {
            playerScore += 1 * scoreMultiply;
            //playerDelta = 0;
        }
        else if (currentPlayerPosition.position.y < prevPlayerPosition - 2)
        {
            playerScore -= 1 * scoreMultiply;
            //playerDelta = 0;
        }
        else
        {
            return;
        }*/
        playerScore = (int)currentPlayerPosition.position.y * scoreMultiply;
        playerScore = Mathf.Clamp(playerScore, 0, 1000000000);
        scoreText.text = "SCORE: " + playerScore;
        prevPlayerPosition = currentPlayerPosition.position.y;
    }
}
