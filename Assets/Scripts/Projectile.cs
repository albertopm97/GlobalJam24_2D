using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject proyectile;
    public GameObject target;
    public float speed = 10f;
    private float towerX; 
    private float targetX;
    private float dist;
    private float nextX;
    private float baseY;
    private float height;
    void Start()
    {
        proyectile=GameObject.FindGameObjectWithTag("Proyectile");
    }
    
    // Update is called once per frame
    void Update()
    {
        /*
        proyectileX = tower.transform.position.x;
        dist = Mathf.Abs(10);
        nextX = Mathf.MoveTowards(transform.position.x, , speed * Time.deltaTime);
        baseY = transform.position.y;
        height = tower.transform.position.y - target.transform.position.y;
        transform.position = new Vector2(nextX, baseY + height * (nextX - towerX) / dist);

        targetX = target.transform.position.x;
        dist = targetX - playerX;
        nextX = Mathf.MoveTowards(transform.position.x, targetX, speed * Time.deltaTime);
        baseY = Mathf.Lerp(player.transform.position.y, target.transform.position.y, (nextX - playerX) / dist);
        height = 2 * (nextX - playerX) * (nextX - targetX) / (-0.25f * dist * dist);

        movePosition = new Vector3(nextX, baseY + height, transform.position.z);

        transform.rotation = LookAtTarget(movePosition - transform.position);
        transform.position = movePosition;*/
    }
}