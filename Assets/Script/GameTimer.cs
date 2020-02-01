using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameTimer : MonoBehaviour
{   
    /// <summary>
    /// Questo enum determina se il timer va avanti o indietro
    /// </summary>
    public enum TimerType
    {
        FORWARD,
        REVERSE
    }

    public TimerType timerTyper;
    public bool play = true;
    public float timeAmount;
    public float timeToReach; 
    public float timeRandomOffset;
    public TextMeshPro textMesh;
    

    [Space(10)]
    public UnityEvent OnTimeExpire, OnTimeReached;


    // Update is called once per frame
    void Update()
    {
        if(play)
        {
            //Se il timer cresce
            if(timerTyper == TimerType.FORWARD)
            {
                timeAmount += Time.deltaTime;

                //Se il timer raggiunge un valore maggiore o uguale del time to reach + un valore random tra time random offset (+ e -)...
                if (timeAmount >= timeToReach + Random.Range(-timeRandomOffset, timeRandomOffset))
                {
                    OnTimeReached.Invoke();
                }

            }
            else //Se il timer decresce
            {
                timeAmount -= Time.deltaTime;

                if (timeAmount < 0)
                {
                    OnTimeExpire.Invoke();
                }
            }
        }
        textMesh.text = "TIMER:" + Mathf.Round(timeAmount).ToString();
    }

    public void Stop()
    {
        play = false;
    }

    /// <summary>
    /// Aggiunge secondi al timer
    /// </summary>
    /// <param name="time"></param>
    public void AddTime(float time)
    {
        timeAmount += time;
    }

    public float GetTimeAmount()
    {
        return timeAmount;
    }
}
