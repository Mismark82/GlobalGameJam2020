using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshPro scoreText;

    private Transform currentCameraPosition;
    private float prevCameraPosition;
    private int playerScore;

    [SerializeField]
    private int scoreMultiply;
    private float cameraDelta;

    // Start is called before the first frame update
    void Start()
    {
        currentCameraPosition = Camera.main.transform;
        prevCameraPosition = currentCameraPosition.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        cameraDelta += currentCameraPosition.position.y - prevCameraPosition;
        prevCameraPosition = currentCameraPosition.position.y;

        if(cameraDelta > 1)
        {
            playerScore += 1 * scoreMultiply;
            scoreText.text = "SCORE: " + playerScore;
            cameraDelta = 0;
        }
    }
}
