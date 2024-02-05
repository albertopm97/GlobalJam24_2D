using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barreraController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "balaSandia" || collision.gameObject.tag == "balaBazooka")
        {
            Destroy(collision.gameObject);
        }
    }
}
