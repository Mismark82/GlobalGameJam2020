using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentManager : MonoBehaviour
{
    public int[] indexes;
    public GameObject[] props;
    private List<GameObject> actualProps;
    private Transform cameraTransform;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        InvokeRepeating("InstantiateProp", 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateProp()
    {
        int side = Random.Range(0, 2);

        if (side == 0)
        {
            GameObject newProps = Instantiate(GetCurrentProps(), new Vector3(10.0f, transform.position.y + Random.Range(-2, 5), 2f), Quaternion.identity);
            newProps.GetComponent<PropsBehaviour>().SetDirection(Vector3.left);
        }
        else
        {
            GameObject newProps = Instantiate(GetCurrentProps(), new Vector3(-10.0f, transform.position.y + Random.Range(-2, 5), 2f), Quaternion.identity);
            newProps.GetComponent<PropsBehaviour>().SetDirection(Vector3.right);
        }
    }
    public GameObject GetCurrentProps()
    {
        if(transform.position.y < indexes[0])
        {
            return props[0];
        }

        if (transform.position.y >= indexes[0] && transform.position.y <= indexes[1])
        {
            return props[1];
        }

        if (transform.position.y >= indexes[1] && transform.position.y <= indexes[2])
        {
            return props[2];
        }

        if (transform.position.y >= indexes[2])
        {
            return props[3];
        }


        return props[0];
    }

    public void StopProps()
    {
        actualProps.RemoveAll(prop => prop == null);

        foreach(GameObject prop in actualProps)
        {
            prop.GetComponent<PropsBehaviour>().StopProps();
        }
    }

    public void ResumeProps()
    {
        actualProps.RemoveAll(prop => prop == null);

        foreach (GameObject prop in actualProps)
        {
            prop.GetComponent<PropsBehaviour>().ResumeProps();
        }
    }
}
