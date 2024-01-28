using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instancia;

    public enum estadoDelJuego
    {
        Fin, GamePlay
    }

    public estadoDelJuego estadoDelJuegoActual;
    public estadoDelJuego estadoDelJuegoPrevio;

    public bool juegoFinalizado = false;
    public Text jugadorGanador;
    public bool mejorandoEquipamiento = false;
    public int jugadorSubiendoNivel;

    [Header("UI")]
    public GameObject menuFinJuego;

    [Header("Cronometro")]

    public GameObject referenciaJugador1;
    public GameObject referenciaJugador2;

    //Al comienzo de la partida desactivamos los menus 
    void Awake()
    {
        //Vamos a aplicar el patron singleton para evitar que haya mas de 1 instancia del gamemanager al mismo tiempo
        if(instancia == null)
        {
            instancia = this;
        }
        else
        {
            Debug.LogWarning("Borrada instancia extra del Game Manager");
        }

        estadoDelJuegoActual = estadoDelJuego.GamePlay;
    }

    void Update()
    {
        switch (estadoDelJuegoActual)
        {
            case estadoDelJuego.GamePlay:

                Time.timeScale = 1f;
                break;

            case estadoDelJuego.Fin:

                if (!juegoFinalizado)
                {
                    juegoFinalizado = true;
                    Time.timeScale = 0f;
                    Debug.Log("FIN DEL JUEGO");
                    mostrarPantallaFinal();
                }
                break;

            default:
                Debug.LogWarning("Estado que no existe");
                break;
        }
    }

    public void cambiarEstadoActual(estadoDelJuego nuevoEstado)
    {
        estadoDelJuegoActual = nuevoEstado;
    }


    public void mostrarPantallaFinal() 
    {
        menuFinJuego.SetActive(true);
    }
}
