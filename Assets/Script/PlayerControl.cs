using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerControl : MonoBehaviour
{
    public float powerJump, speed, horizontal;
    public float altezzaDaTerra;
    Rigidbody2D rb2d;
    RaycastHit2D rh2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        IsOnFloor();
        horizontal = Input.GetAxis("Horizontal");
        if(Input.GetKeyDown(KeyCode.Space) && IsOnFloor())
        {
            Salto();
        }

    }

    public bool IsOnFloor()
    {
        bool veroFalso = false;
        rh2d = Physics2D.Raycast(transform.position, -Vector2.up, altezzaDaTerra, 8);
        if (rh2d.collider.name == "Piattaforma")
        {
            veroFalso = true;
        }
        print(veroFalso);
        return veroFalso;
    }

    public void Salto()
    { 
        Vector2 SaltoDir = new Vector2(0, powerJump);
        rb2d.AddForce(SaltoDir);
    }
}
