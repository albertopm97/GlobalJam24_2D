using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] float maxLifeTime = 5;
    //float startingYPosition;
    

    public void Init(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;
        //startingYPosition = transform.position.y;

        Invoke("DestroyBullet", maxLifeTime);
    }


    /*
    private void Update()
    {
        if (transform.position.y < startingYPosition)
        {
            DestroyBullet();
        }
    }
    */


    private void DestroyBullet()
    {
        GameObject.Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Barrera")
        {
            DestroyBullet();
        }
    }
}
