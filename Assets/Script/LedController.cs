using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LedController : MonoBehaviour
{
    Image immagine;
    // Start is called before the first frame update
    void Start()
    {
        immagine = GetComponent<Image>();
        StartCoroutine(routineLed());
    }

    IEnumerator routineLed()
    {
        while(true)
        {
            var colore = new Color(1, 1, 1, Random.Range(0.0f, 1.0f));
            immagine.color = colore;
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        }

    }

}
