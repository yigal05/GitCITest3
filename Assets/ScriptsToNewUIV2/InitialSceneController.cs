using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitialSceneController : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("isLoggedIn", 0) == 1)
        {
            // Usuario autenticado, cargar la segunda escena
            SceneManager.LoadScene("UI2");
        }
        else
        {
            // Usuario no autenticado, permanecer en la escena de inicio
            SceneManager.LoadScene("LogRegScene");
        }
    }
}
