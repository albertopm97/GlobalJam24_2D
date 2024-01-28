using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player1 : MonoBehaviour
{
    public enum  TipoArma {Pepitas, Bazooka};

    public GameObject bulletMAchineGun;
    public GameObject bulletBazooka;
    public GameObject colisionCaidaMina;

    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float recoilStrength;

    public TipoArma armaActual;

    Rigidbody2D rb;
    public float jumpForce;
    Vector2 moveDir;
    public float moveSpeed;
    public float moveAcceleration;

    private bool minaPisada;
    private bool vueltaOrigen;
    bool coliderBajadaInstanciado;

    Vector3 posPrevia;
    public float tiempoVuelo;

    private float tiempoVueloActual;

    public float caidaPuercoespin;

    bool canMove = true;
    [SerializeField] float paralyzedTime = 1f;
    [SerializeField] float chest_coldown;
    private float chest_coldown_atual;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        moveDir = Vector2.zero;

        minaPisada = false;

        vueltaOrigen = false;

        coliderBajadaInstanciado = false;

        tiempoVuelo = 3f;

    }
    // Start is called before the first frame update
    void Start()
    {
        chest_coldown_atual = chest_coldown;
    }


    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            switch (armaActual)
            {
                case TipoArma.Bazooka:
                    firePinhazooka();
                    break;

                case TipoArma.Pepitas:
                    fireMachinegun();
                    break;
            }
        }
        chest_coldown_atual -= Time.deltaTime;

        /*if (minaPisada)  
        {
            if (tiempoVueloActual > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                tiempoVueloActual -= Time.deltaTime;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, posPrevia, caidaPuercoespin);

                if (vueltaOrigen)
                {
                    minaPisada = false;
                }
            }
        }*/
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        rb.velocity = moveDir * moveSpeed * Time.deltaTime;
        //rb.velocity += moveDir * moveAcceleration * Time.deltaTime;
        //rb.velocity.magnitude
    }

    private void fireMachinegun()
    {
        GameObject bullet = Instantiate(bulletMAchineGun);

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

        if (collision.gameObject.tag == "minaPuercoespin")
        {
            Debug.Log("Mina pisada");
            minaPisada = true;

            tiempoVueloActual = tiempoVuelo;

            posPrevia = collision.transform.position;


            //Antes de destruir activar animacion puerco espin

            //Destroy(collision.gameObject);

            //Activo el objeto para colisionar en la caida

            if (!coliderBajadaInstanciado)
            {
                GameObject colisionCaida = Instantiate(colisionCaidaMina);

                colisionCaida.transform.position = this.transform.position;

                coliderBajadaInstanciado = true;
            }

        }
        else if (collision.gameObject.tag == "BajadaMina")
        {
            vueltaOrigen = true;

            //Destroy(collision.gameObject);

            coliderBajadaInstanciado = false;
        }
        else if (collision.gameObject.tag == "Paralyzer")
        {
            canMove = false;
            rb.velocity = Vector2.zero;
            //Handheld.Vibrate();
            Invoke("ResetMovement", paralyzedTime);
        }

        else if (collision.gameObject.tag == "Chest")
        {
            if (chest_coldown_atual < 0) 
            {
                TipoArma[] allWeapons = (TipoArma[])Enum.GetValues(typeof(TipoArma));
                TipoArma randomWeapon = allWeapons[Random.Range(0, allWeapons.Length)];

                armaActual = randomWeapon;

                Debug.Log("Nueva arma:" + armaActual);
                chest_coldown_atual = chest_coldown;

            }

        }
    }

    void ResetMovement()
    {
        canMove = true;
    }
}
