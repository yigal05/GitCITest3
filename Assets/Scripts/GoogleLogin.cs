using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using Firebase;
using Firebase.Extensions;
using Firebase.Auth;
using UnityEngine.UIElements;
using Google;
using System.Net.Http;


public class GoogleLogin : MonoBehaviour
{
    public string GoogleWebAPI = "737799834196-lg6mapd3763f7abnsfm2v242qbpk77el.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    FirebaseAuth auth;
    FirebaseUser user;




    private Label lb_warningSignUp;
    private Button bt_googleLoginButton;
    private VisualElement signUpScreen;


    

    private void Awake()
    {
        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebAPI,
            RequestIdToken = true
        };


        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        signUpScreen = root.Q<VisualElement>("registerScreen");
        lb_warningSignUp = root.Q<Label>("warningSignUp");
        bt_googleLoginButton = root.Q<Button>("googleLoginButton");
        bt_googleLoginButton.RegisterCallback<ClickEvent>(Prueba);


    }

    private void Start()
    {
        // Inicializar Firebase Auth
        auth = FirebaseAuth.DefaultInstance;
    }

    void GoogleSignInClick(ClickEvent evt)
    {
        GoogleSignIn.Configuration = configuration;
        GoogleSignIn.Configuration.UseGameSignIn = false;
        GoogleSignIn.Configuration.RequestIdToken = true;
        GoogleSignIn.Configuration.RequestEmail = true;

        GoogleSignIn.DefaultInstance.SignIn().ContinueWith(OnGoogleAuthenticatedFinished);
    }

    void OnGoogleAuthenticatedFinished(Task<GoogleSignInUser> task)
    {
        if (task.IsFaulted)
        {
            Debug.LogError("Fault");
        }
        else if (task.IsCanceled)
        {
            Debug.LogError("Login Cancel");
        }
        else
        {
            Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);

            auth.SignInWithCredentialAsync(credential).ContinueWithOnMainThread(task =>
            {
                if (task.IsCanceled)
                {
                    Debug.LogError("SignInWithCredentialASync was canceled");
                    return;
                }
                if (task.IsFaulted)
                {
                    Debug.LogError("SignInWithCredentialASync encountered an error");
                    return;
                }

                user = auth.CurrentUser;

                lb_warningSignUp.text = "Inicio de sesión exitoso";


            });
        }
    }

    void Prueba(ClickEvent evt)
    {
        lb_warningSignUp.text = "Inicio de sesión exitoso";
    }

}

