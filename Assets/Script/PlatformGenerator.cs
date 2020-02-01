using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public float yOffset;
    public float xOffset;
    public float xClamp;

    public GameObject[] platformsPrefabsA;
    public GameObject[] platformsPrefabsB;
    public GameObject[] platformsPrefabsC;
    public GameObject[] platformsPrefabsD;
    private int ultimoIndice = 0;


    private GameObject[] currentPlatformsPool;

    public int[] heights;
    public GameObject startingPlatform;                                                                             

    private Vector2 lastPlatform;
    private float cameraOffset = 8.0f;

    private void Awake()
    {
        lastPlatform = startingPlatform.transform.position;
        currentPlatformsPool = platformsPrefabsA;
    }

    // Update is called once per frame
    void Update()
    {
       if(transform.position.y + cameraOffset > lastPlatform.y)
        {
            //get next x position (clamping to xClamp)
            float offset = Random.Range(-xOffset, xOffset);
            offset = offset > 0 ? offset + 2 : offset - 2;
            //float tempXAxis = Random.Range(lastPlatform.x - xOffset, lastPlatform.x + xOffset);
            float tempXAxis = offset;
            float xAxis = Mathf.Clamp(tempXAxis, -xClamp, xClamp);

            GameObject tempPlatform = Instantiate(GetCurrentPlatform(Mathf.Abs(xAxis - lastPlatform.x)), 
                                                  new Vector3(xAxis, lastPlatform.y + yOffset, 0.0f), 
                                                  Quaternion.identity);
            lastPlatform = tempPlatform.transform.position;
        }
    }

    public GameObject GetCurrentPlatform(float distance)
    {
        //Debug.Log("Distance:" + distance);
        if(transform.position.y >= heights[0])
        {
            currentPlatformsPool = platformsPrefabsB;
        }

        if (transform.position.y >= heights[1])
        {
            currentPlatformsPool = platformsPrefabsC;
        }

        if (transform.position.y >= heights[2])
        {
            currentPlatformsPool = platformsPrefabsD;
        }
        if (distance > 3)
        {
            int randomIndex = Random.Range(0, 2);
            ultimoIndice = randomIndex;
            return currentPlatformsPool[randomIndex];
        }
        else
        {
            ultimoIndice = 0;
            return currentPlatformsPool[0];
        }


    }

    public GameObject GetNextPlatform(float distance)
    {
        // Scelta del pool di pedane
        if (transform.position.y >= heights[0])
        {
            currentPlatformsPool = platformsPrefabsB;
        }

        if (transform.position.y >= heights[1])
        {
            currentPlatformsPool = platformsPrefabsC;
        }

        if (transform.position.y >= heights[2])
        {
            currentPlatformsPool = platformsPrefabsD;
        }

        // Selezionare la nuova pedana: dipende dalla distanza x tra quella sotto e quella sopra e le relative dimensioni

        return currentPlatformsPool[0];
    }

    public GameObject GetCurrentPlatform(int indice)
    {
        if (transform.position.y >= heights[0])
        {
            currentPlatformsPool = platformsPrefabsB;
        }

        if (transform.position.y >= heights[1])
        {
            currentPlatformsPool = platformsPrefabsC;
        }

        if (transform.position.y >= heights[2])
        {
            currentPlatformsPool = platformsPrefabsD;
        }

        int randomIndex = indice;
        ultimoIndice = randomIndex;
        return currentPlatformsPool[randomIndex];
    }
}
