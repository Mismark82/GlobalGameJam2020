using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro scoreText;

    public Transform currentPlayerPosition;
    private float prevPlayerPosition;
    private int playerScore;

    [SerializeField]
    private int scoreMultiply;
    private float playerDelta;

    // Start is called before the first frame update
    void Start()
    {
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
