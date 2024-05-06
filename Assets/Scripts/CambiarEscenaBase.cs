using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    // Nombre de la escena a la que quieres navegar
    public string nombreDeLaEscena;

    // Método para cargar la escena
    public void CargarEscena()
    {
        // Cargar la escena según su nombre
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
