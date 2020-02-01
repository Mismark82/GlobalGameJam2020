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
    bool jump = false, pausa = false;
    Rigidbody2D rb2d;
    Vector2 appoVelocity = new Vector2(0,0);

    public void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        digitalGlitch.enabled = false;
        glitchEffect.enabled = false;
    }

    public void Pause()
    {
        print("stoppato");
        digitalGlitch.enabled = true;
        glitchEffect.enabled = true;
        appoVelocity = rb2d.velocity;
        rb2d.bodyType = RigidbodyType2D.Static;
        //rb2d.Sleep();
    }
    
    public void Go()
    {
        print("svegliato");
        digitalGlitch.enabled = false;
        glitchEffect.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Dynamic;
        rb2d.velocity = appoVelocity;
        //rb2d.WakeUp();
    }

    

    // Update is called once per frame
    void Update()
    {
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
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
        transform.position = new Vector2(Mathf.Clamp(transform.position.x, limiteXMin, limiteXMax), transform.position.y);
    }
}
