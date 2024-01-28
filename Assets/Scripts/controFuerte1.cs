using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controFuerte1 : MonoBehaviour
{
    public Transform player = null;

    SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            sr.sortingLayerName = "TrasJugador";
        }
        else
        {
            sr.sortingLayerName = "Frontal";
        }

        if (player.transform.position.y < transform.position.y)
        {
            sr.sortingLayerName = "Frontal";
        }
        else
        {
            sr.sortingLayerName = "TrasJugador";
        }


    }
}
