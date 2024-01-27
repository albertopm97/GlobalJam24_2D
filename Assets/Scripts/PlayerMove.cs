using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    Rigidbody2D rb;
    public float moveSpeed;
    public float jumpForce;
    public int maxJumps;
    int numJumps;
    bool canJump;
    Vector2 direction;
    SpriteRenderer spriteRenderer;
    public int score;

    private void Awake()
    {
        numJumps = maxJumps;
        moveSpeed = 5f;
        jumpForce = 7f;
        canJump = true;
        rb = GetComponent<Rigidbody2D>();
        score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, Input.GetAxis("Vertical") * moveSpeed);

        //rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
    }
}
