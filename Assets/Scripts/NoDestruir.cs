using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoDestruir : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        // Asegura que este objeto no se destruirá al cargar una nueva escena
        DontDestroyOnLoad(gameObject);
    }
}
