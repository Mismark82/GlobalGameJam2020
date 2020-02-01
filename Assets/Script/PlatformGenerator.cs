using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public float yOffset;
    public float xOffset;
    public float xClamp;

    public GameObject platformPrefab;
    public GameObject startingPlatform;                                                                             

    private Vector2 lastPlatform;
    private float cameraOffset = 8.0f;

    private void Awake()
    {
        lastPlatform = startingPlatform.transform.position;    
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.y + cameraOffset > lastPlatform.y)
        {
            //get next x position (clamping to xClamp)
            float tempXAxis = Random.Range(lastPlatform.x - xOffset, lastPlatform.x + xOffset);
            float xAxis = Mathf.Clamp(tempXAxis, -xClamp, xClamp);

            GameObject tempPlatform = Instantiate(platformPrefab, 
                                                  new Vector3(xAxis, lastPlatform.y + yOffset, 0.0f), 
                                                  Quaternion.identity);
            lastPlatform = tempPlatform.transform.position;
        }
    }
}
