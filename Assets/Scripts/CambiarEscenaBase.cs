using UnityEngine;
using UnityEngine.SceneManagement;

public class CambiarEscena : MonoBehaviour
{
    // Nombre de la escena a la que quieres navegar
    public string nombreDeLaEscena;

    // M�todo para cargar la escena
    public void CargarEscena()
    {
        // Cargar la escena seg�n su nombre
        SceneManager.LoadScene(nombreDeLaEscena);
    }
}
