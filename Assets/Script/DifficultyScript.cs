using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyScript : MonoBehaviour
{
    GameTimer timer;
    public float minTime, maxTime;

    public void Start()
    {
        timer = GetComponent<GameTimer>();
        SetTimer();
    }

    public void SetTimer()
    {
        timer.timeAmount = 0;
        timer.timeToReach = Random.Range(minTime, maxTime);
    }
}
