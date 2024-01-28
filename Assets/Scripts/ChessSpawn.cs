using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessSpawn : MonoBehaviour
{
    public GameObject chess;
    public float chessSpawnTime;

    public float tiempoActual;

    // Start is called before the first frame update
    void Awake()
    {
        tiempoActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tiempoActual -= Time.deltaTime;

        if(tiempoActual <= 0)
        {
            GameObject cofre = Instantiate(chess);

            cofre.transform.position = transform.position;

            tiempoActual = chessSpawnTime;
        }
    }
}
