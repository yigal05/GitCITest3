using UnityEngine;

public class CanvasSwitcherBack : MonoBehaviour
{
    public GameObject signUpCanvas; // Referencia al Canvas de SignUp_UI
    public GameObject loginCanvas;  // Referencia al Canvas de Login_UI

    public void SwitchCanvas()
    {
        signUpCanvas.SetActive(true);  // Activa el Canvas de SignUp_UI
        loginCanvas.SetActive(false);  // Desactiva el Canvas de Login_UI
    }
}
