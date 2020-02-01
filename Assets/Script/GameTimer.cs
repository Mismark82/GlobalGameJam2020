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
    public SequenceCheckerScript sequence;
    private bool fired = false;
    

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
                if (!fired && timeAmount >= timeToReach + Random.Range(-timeRandomOffset, timeRandomOffset))
                {
                    fired = true;
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
        if (textMesh!=null)
            textMesh.text = "TIMER:" + Mathf.Round(timeAmount).ToString();
    }

    public void Stop()
    {
        play = false;
    }

    public void Resume()
    {
        play = true; 
        fired = false;
    }

    /// <summary>
    /// Aggiunge secondi al timer
    /// </summary>
    /// <param name="time"></param>
    public void AddTime(float time)
    {
        timeAmount += time;
    }

    public void AddTime()
    {
        timeAmount += sequence.GetTime;
        fired = false;
    }

    public float GetTimeAmount()
    {
        return timeAmount;
    }
}
