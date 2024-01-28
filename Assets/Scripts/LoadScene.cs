using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Nombre de la escena que quieres cargar
    public string sceneNameToLoad;

    // M�todo para cargar la escena por su nombre
    public void LoadSceneByName()
    {
        // Verifica que el nombre de la escena no est� vac�o
        if (!string.IsNullOrEmpty(sceneNameToLoad))
        {
            // Carga la escena por su nombre
            SceneManager.LoadScene(sceneNameToLoad);
        }
        else
        {
            Debug.LogWarning("El nombre de la escena no puede estar vac�o.");
        }
    }
}
