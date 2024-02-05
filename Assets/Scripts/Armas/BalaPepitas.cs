using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaPepitas : MonoBehaviour
{
    public float bulletSpeed= 160f;
    public float bulletSpread = 0.01f;
    public Vector2 trajectory = new Vector2 (0f,1f);
    private Rigidbody2D rb;
    private float distanceTravelled;
    public float maxDist;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 currentPos = rb.position;

        rb.MovePosition(new Vector2(currentPos.x + bulletSpeed * trajectory.x * Time.deltaTime, currentPos.y + bulletSpeed * trajectory.y * Time.deltaTime));

        distanceTravelled += bulletSpeed * Time.deltaTime;

        if(distanceTravelled > maxDist)
        {
            Destroy(gameObject);
        }
    }
    
    public void setTrajectory(Vector2 trajectory)
    {
        trajectory.Normalize();

        trajectory.x += Random.Range(bulletSpread, -1f * bulletSpread);
        trajectory.y += Random.Range(bulletSpread, -1f * bulletSpread);

        this.trajectory = trajectory;

        float random = Random.Range(1f, 0f);

        this.transform.position += new Vector3(random * trajectory.x, random * trajectory.y, 0f);

        float angle = Mathf.Atan2(trajectory.y, trajectory.x) * 360f/(2f * Mathf.PI) - 90f;
        this.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
 