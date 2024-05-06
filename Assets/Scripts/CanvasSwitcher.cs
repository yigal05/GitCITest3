using UnityEngine;
using UnityEngine.UI;

public class CanvasSwitcher : MonoBehaviour
{
    public GameObject signUpCanvas; // Referencia al Canvas de SignUp_UI
    public GameObject loginCanvas;  // Referencia al Canvas de Login_UI

    public void SwitchCanvas()
    {
        signUpCanvas.SetActive(false); // Desactiva el Canvas de SignUp_UI
        loginCanvas.SetActive(true);   // Activa el Canvas de Login_UI
    }
}
