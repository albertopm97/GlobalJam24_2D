using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSpawn : MonoBehaviour
{

    public static ChessSpawn instance;

    //prefab para spawn
    public GameObject chess;

    //Timer
    public float chessSpawnTime;

    public float tiempoActual;

    //Puntos para spawn de cofres
    public Transform p1;
    public Transform p2;

    //bool de los cofres
    public static bool hayCofre1;
    public static bool hayCofre2;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            print("Instancia extra del spawner de cofres borrada");
        }

        tiempoActual = 0;
        hayCofre1 = false;
        hayCofre2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual -= Time.deltaTime;

        if(tiempoActual <= 0)
        {
            if(!hayCofre1)
            {
                GameObject cofre = Instantiate(chess);

                cofre.transform.position = p1.position;

                hayCofre1 = true;
            }

            if (!hayCofre2)
            {
                GameObject cofre2 = Instantiate(chess);

                cofre2.transform.position = p2.position;

                hayCofre2 = true;
            }

            tiempoActual = chessSpawnTime;
        }
    }
}
