using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public GameObject bulletMAchineGun;
    public GameObject colisionCaidaMina;

    Rigidbody2D rb;
    public float jumpForce;
    Vector2 moveDir;
    public float moveSpeed;

    private bool minaPisada;
    private bool vueltaOrigen;
    bool coliderBajadaInstanciado;

    Vector3 posPrevia;
    public float tiempoVuelo;

    private float tiempoVueloActual;

    public float caidaPuercoespin;

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
        
    }


    // Update is called once per frame
    void Update()
    {
        moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButton("Jump"))
        {
            fireMachinegun();
        }

        if (minaPisada)  
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "minaPuercoespin")
        {
            Debug.Log("Mina pisada");
            minaPisada = true;

            tiempoVueloActual = tiempoVuelo;

            posPrevia = collision.transform.position;


            //Antes de destruir activar animacion puerco espin

            //Destroy(collision.gameObject);

            //Activo el objeto para colisionar en la caida

            if(!coliderBajadaInstanciado)
            {
                GameObject colisionCaida = Instantiate(colisionCaidaMina);

                colisionCaida.transform.position = this.transform.position;

                coliderBajadaInstanciado = true;
            }
            
        }
        else if(collision.gameObject.tag == "BajadaMina")
        {
            vueltaOrigen = true;

            //Destroy(collision.gameObject);

            coliderBajadaInstanciado = false;
        }
    }
}
