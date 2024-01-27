using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controFuerte1 : MonoBehaviour
{
    public Transform player = null;

    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < transform.position.x)
        {
            //spriteRendere  POR AQUI VOY
        }
    }
}
