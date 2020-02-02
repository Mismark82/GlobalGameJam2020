using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropsBehaviour : MonoBehaviour
{
    public float speed;

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 20.0f);
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;

        if(direction == Vector3.right)
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }

    public void StopProps()
    {
        //fermare il prop
    }

    public void ResumeProps()
    {
        //far ripartire il props
    }

}
