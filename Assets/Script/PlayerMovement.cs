using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float limiteXMin, limiteXMax;
    public GlitchEffect glitchEffect;
    public DigitalGlitch digitalGlitch;
    float horizontalMove = 0f;
    AudioSource aSource;
    bool jump = false, pausa = false;
    Rigidbody2D rb2d;
    Animator animatore;
    bool screamBool = false;
    Vector2 appoVelocity = new Vector2(0,0);

    public void Start()
    {
        aSource = GetComponent<AudioSource>();
        animatore = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        digitalGlitch.enabled = false;
        glitchEffect.enabled = false;
    }

    //Mette in pausa il gioco con il glich
    public void Pause()
    {
        //print("stoppato");
        digitalGlitch.enabled = true;
        glitchEffect.enabled = true;
        appoVelocity = rb2d.velocity;
        rb2d.bodyType = RigidbodyType2D.Static;
        //rb2d.Sleep();
    }
    
    //Fa ripartire il gioco
    public void Go()
    {
        //print("svegliato");
        digitalGlitch.enabled = false;
        glitchEffect.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.velocity = appoVelocity;
        //rb2d.WakeUp();
    }

    

    // Update is called once per frame
    void Update()
    {
        //ATTENZIONE: Questo serve per testare la pausa! Va tolto nel gioco definitivo!!!
        if(Input.GetKeyDown(KeyCode.P))
        {
            if(pausa)
            {
                Go();
                pausa = false;
            }
            else
            {
                Pause();
                pausa = true;
            }
        }

        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        //GESTIONE ANIMAZIONI

        //Camminata
        if(controller.GetGrounded)
        {
            animatore.SetFloat("Walking", Mathf.Abs(horizontalMove));
            animatore.SetBool("TouchDown", true);
            screamBool = false;
        }
        else
        {
            animatore.SetFloat("Walking", 0f);
            animatore.SetBool("TouchDown", false);
        }

        //Salto
        animatore.SetFloat("VelocityY", rb2d.velocity.y);

    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, limiteXMin, limiteXMax), transform.position.y);
    }

    public bool isGRound
    {
        get { return controller.GetGrounded; }
    }
}
