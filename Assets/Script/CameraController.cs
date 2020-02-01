using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        Vector3 camPosition = new Vector3(0, player.transform.position.y, -1);
        transform.position = camPosition;
    }
}
