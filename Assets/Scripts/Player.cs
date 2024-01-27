using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject bulletMAchineGun;

    Rigidbody2D rb;
    Vector2 moveDir;
    public float moveSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        moveDir = Vector2.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            fireMachinegun();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
    }

    private void fireMachinegun()
    {
        GameObject bullet = Instantiate(bulletMAchineGun);

        bullet.transform.position = this.transform.position;

        Vector2 trajectory = moveDir;

        bullet.GetComponent<BalaPepitas>().setTrajectory(trajectory);
    }
}
