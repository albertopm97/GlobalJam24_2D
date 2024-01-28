using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static Player;
using Random = UnityEngine.Random;


public class Player1 : MonoBehaviour
{
    public int salud;

    public int municion;

    public enum TipoTrampa { Mofetida, Pinchoso, SinArma };

    public GameObject pinchoso;
    public GameObject mofetida;

    public GameObject bulletMachineGun;
    public GameObject bulletBazooka;

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float recoilStrength;

    public TipoTrampa trampaActual;

    Rigidbody2D rb;
    CapsuleCollider2D cc;

    public float jumpForce;
    Vector2 moveDir;
    public float moveSpeed;
    public float moveAcceleration;

    public bool minaPisada;

    public float tiempoVuelo;

    private float tiempoVueloActual;

    public float alturaInicial;

    [SerializeField] float chest_coldown;
    private float chest_coldown_atual;

    bool canMove = true;
    [SerializeField] float paralyzedTime = 1f;

    [Header("Animacion")]
    private Animator animator;

    private void Awake()
    {
        salud = 100;

        municion = 0;

        rb = GetComponent<Rigidbody2D>();

        moveDir = Vector2.zero;

        tiempoVuelo = 3f;

        minaPisada = false;

        cc = GetComponent<CapsuleCollider2D>();

        animator = GetComponent<Animator>();

        chest_coldown_atual = chest_coldown;
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        chest_coldown_atual -= Time.deltaTime;

        //moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir.y = -1;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDir.y = 1;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir.x = 1;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDir.x = -1;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            moveDir.y = 0;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            moveDir.x = 0;
        }
        //Activamos la animacion de correr si se mueve el personaje
        //animator.SetFloat("Horizontal", Mathf.Abs(rb.velocity.x));
        //animator.SetFloat("Vertical", Mathf.Abs(rb.velocity.y));

        if (moveDir.x != 0 || moveDir.y != 0)
        {
            animator.SetBool("Andar", true);
        }
        else
        {
            animator.SetBool("Andar", false);
        }

        if (!canMove)
            return;

        rb.velocity = moveDir * moveSpeed * Time.deltaTime;

        //Accion de las armas
        if (Input.GetKeyDown(KeyCode.Space) && municion >= 0)
        {
            switch (trampaActual)
            {
                case TipoTrampa.Mofetida:
                    fireMofetida();
                    break;

                case TipoTrampa.Pinchoso:
                    firePinchoso();
                    break;

                case TipoTrampa.SinArma:
                    break;
            }
        }

        if (minaPisada)
        {
            if (tiempoVueloActual > 0)
            {
                //rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                rb.AddForce(Vector2.up * jumpForce);
                tiempoVueloActual -= Time.deltaTime;
            }
            else if (tiempoVueloActual <= 0)
            {
                rb.gravityScale = 150;
            }
        }

        if (transform.position.y < alturaInicial && minaPisada)
        {
            rb.gravityScale = 0;
            cc.enabled = true;

            minaPisada = false;
        }

        if (salud <= 0)
        {
            GameManager.instancia.cambiarEstadoActual(GameManager.estadoDelJuego.Fin);
        }

        //Flip animation 
        bool flipped = moveDir.x > 0;
        this.transform.rotation = Quaternion.Euler(new Vector3(0f, flipped ? 180f : 0f, 0f));
    }



    private void fireMachinegun()
    {
        GameObject bullet = Instantiate(bulletMachineGun);

        bullet.transform.position = bulletSpawnPoint.position;

        Vector2 trajectory = moveDir;

        bullet.GetComponent<BalaPepitas>().setTrajectory(trajectory);
    }

    private void firePinhazooka()
    {
        GameObject bullet = Instantiate(bulletBazooka);

        bullet.transform.position = bulletSpawnPoint.position;
        Quaternion rotation = Quaternion.Euler(
            0,
            0,
            GetRotationAngle()
        ); ;

        Vector2 trajectory = rotation * moveDir;

        bullet.GetComponent<Bullet>().Init(trajectory);

        rb.AddForce(-trajectory.normalized * recoilStrength);
    }

    private void firePinchoso()
    {
        GameObject mina = Instantiate(pinchoso);

        mina.transform.position = transform.position;
    }

    private void fireMofetida()
    {
        GameObject mina = Instantiate(mofetida);

        mina.transform.position = transform.position;
    }

    float GetRotationAngle()
    {
        return moveDir.x > 0 ? GetUnsignedAngle() : -GetUnsignedAngle();

        /*
        if (moveDir.x > 0)
            return GetUnsignedAngle();
        else if (moveDir.x < 0)
            return -GetUnsignedAngle();
        else
            return 0;
        */
    }

    float GetUnsignedAngle()
    {
        float minRotation = 0;
        float maxRotation = 50;

        float verticalMovement = moveDir.normalized.y;
        float normalizedVerticalMovement = 1f - (verticalMovement + 1) / 2;

        return Mathf.Lerp(minRotation, maxRotation, normalizedVerticalMovement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Chocando con: " + collision.gameObject.name);
        Debug.Log("Chocando con: " + collision.gameObject.tag);

        if (collision.gameObject.tag == "balaBazooka")
        {
            salud -= 20;
        }
        else if (collision.gameObject.tag == "balaSandia")
        {
            salud -= 1;
        }
        else if (collision.gameObject.tag == "Chest")
        {
            if (chest_coldown_atual < 0)
            {
                TipoTrampa[] allWeapons = (TipoTrampa[])Enum.GetValues(typeof(TipoTrampa));
                TipoTrampa randomWeapon = allWeapons[Random.Range(0, allWeapons.Length)];

                trampaActual = randomWeapon;

                Debug.Log("Nueva arma:" + trampaActual);
                chest_coldown_atual = chest_coldown;

                municion = 2;

                Destroy(collision.gameObject);
            }

        }

    }

    void ResetMovement()
    {
        canMove = true;
    }
}
