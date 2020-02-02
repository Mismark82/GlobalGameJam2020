using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    public Animator anim;

    private int handIndex = 0;
    public string[] indici;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void MoveHand()
    {
        anim.SetTrigger(indici[handIndex]);
        if(handIndex < 5)
        {
            handIndex++;
        }
        else
        {
            handIndex = 0;
        }
    }
 }
