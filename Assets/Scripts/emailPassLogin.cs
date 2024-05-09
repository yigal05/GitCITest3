using System.Collections;
using UnityEngine;
using Firebase.Extensions;
using Firebase.Auth;
using Firebase;
using UnityEngine.UIElements;
using Google;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class emailPassLogin : MonoBehaviour
{

    //Google
    public string GoogleWebAPI = "737799834196-lg6mapd3763f7abnsfm2v242qbpk77el.apps.googleusercontent.com";

    private GoogleSignInConfiguration configuration;
    DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
    FirebaseAuth auth;
    FirebaseUser user;


    //UITOOLKIT REFERENCES

    //pantallas
    private VisualElement loginScreen;
    private VisualElement signUpScreen;
    
    //botones Login
    private TextField email;
    private TextField password;
    private Button bt_goDatabase;
    private Button bt_logging;
    private Label lb_GoSignup;
    private Label lb_warning;
    
    //botones de register
    private TextField emailSignUp;
    private TextField passwordSignUp;
    private TextField passwordConfirm;
    private Button bt_signUp;
    private Label lb_goLogin;
    private Label lb_warningSignUp;
    private Button bt_googleLoginButton;

    public VisualTreeAsset foods;
    public UIDocument interfaces;



    private void Awake()
    {

        configuration = new GoogleSignInConfiguration
        {
            WebClientId = GoogleWebAPI,
            RequestIdToken = true
        };


        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        loginScreen= root.Q<VisualElement>("loginScreen");
        signUpScreen= root.Q<VisualElement>("registerScreen");
        
        email = root.Q<TextField>("emailText");
        password = root.Q<TextField>("passwordText");
        bt_goDatabase = root.Q<Button>("goDatabase");
        bt_logging = root.Q<Button>("loginButton");
        lb_GoSignup = root.Q<Label>("singUpLabel");
        lb_warning = root.Q<Label>("warnings");
        
        //inicializacion de botones del register
        emailSignUp = root.Q<TextField>("emailTextSignUP");
        passwordSignUp= root.Q<TextField>("passwordTextSignUp");
        passwordConfirm=root.Q<TextField>("passwordTextSignUpC");
        bt_signUp= root.Q<Button>("signUPButton");
        lb_goLogin = root.Q<Label>("loginLabel");
        lb_warningSignUp=root.Q<Label>("warningSignUp");

        
        bt_logging.RegisterCallback<ClickEvent>(Login);
        lb_GoSignup.RegisterCallback<ClickEvent>((evt) =>
            { loginScreen.style.display = DisplayStyle.None; signUpScreen.style.display = DisplayStyle.Flex; });
        lb_goLogin.RegisterCallback<ClickEvent>((evt) =>
            { loginScreen.style.display = DisplayStyle.Flex; signUpScreen.style.display = DisplayStyle.None; });
        
        bt_signUp.RegisterCallback<ClickEvent>(SignUp);

        bt_googleLoginButton = root.Q<Button>("googleLoginButton");
        bt_googleLoginButton.RegisterCallback<ClickEvent>(GoogleSignInClick);


    }

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }


    #region signup 

    public void SignUp(ClickEvent evt)
    {
        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = emailSignUp.value;
        string password = passwordSignUp.value;
        string confirmPassword = passwordConfirm.value;

        // Verifica que las contraseñas ingresadas sean iguales
        if (password != confirmPassword)
        {
            ShowErrorMessage("Las contraseñas no coinciden.");
            return;
        }

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {
                ShowErrorMessage("Creación de usuario cancelada.");
                return;
            }
            if (task.IsFaulted)
            {
                ShowErrorMessage("Error al crear usuario: " + task.Exception.InnerException.Message);
                return;
            }

            AuthResult result = task.Result;

            emailSignUp.value = "";
            passwordSignUp.value = "";
            passwordConfirm.value = "";
            

            SendEmailVerification();
            
        });
    }

    private void ShowErrorMessage(string message)
    {
        lb_warningSignUp.text = message;
    }
    
    public void SendEmailVerification()
    {
        StartCoroutine(SendEmailForVerificationAsync());
    }

    IEnumerator SendEmailForVerificationAsync()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        if (user != null)
        {
            var sendEmailTask = user.SendEmailVerificationAsync();
            yield return new WaitUntil(() => sendEmailTask.IsCompleted);

            if (sendEmailTask.Exception != null)
            {
                print("Email send error");
                FirebaseException firebaseException = sendEmailTask.Exception.GetBaseException() as FirebaseException;
                AuthError error = (AuthError)firebaseException.ErrorCode;

                switch (error)
                {
                    case AuthError.None:
                        break;
                    case AuthError.Unimplemented:
                        break;
                    case AuthError.Failure:
                        break;
                    case AuthError.InvalidCustomToken:
                        break;
                    case AuthError.CustomTokenMismatch:
                        break;
                    case AuthError.InvalidCredential:
                        break;
                    case AuthError.UserDisabled:
                        break;
                    case AuthError.AccountExistsWithDifferentCredentials:
                        break;
                    case AuthError.OperationNotAllowed:
                        break;
                    case AuthError.EmailAlreadyInUse:
                        break;
                    case AuthError.RequiresRecentLogin:
                        break;
                    case AuthError.CredentialAlreadyInUse:
                        break;
                    case AuthError.InvalidEmail:
                        break;
                    case AuthError.WrongPassword:
                        break;
                    case AuthError.TooManyRequests:
                        break;
                    case AuthError.UserNotFound:
                        break;
                    case AuthError.ProviderAlreadyLinked:
                        break;
                    case AuthError.NoSuchProvider:
                        break;
                    case AuthError.InvalidUserToken:
                        break;
                    case AuthError.UserTokenExpired:
                        break;
                    case AuthError.NetworkRequestFailed:
                        break;
                    case AuthError.InvalidApiKey:
                        break;
                    case AuthError.AppNotAuthorized:
                        break;
                    case AuthError.UserMismatch:
                        break;
                    case AuthError.WeakPassword:
                        break;
                    case AuthError.NoSignedInUser:
                        break;
                    case AuthError.ApiNotAvailable:
                        break;
                    case AuthError.ExpiredActionCode:
                        break;
                    case AuthError.InvalidActionCode:
                        break;
                    case AuthError.InvalidMessagePayload:
                        break;
                    case AuthError.InvalidPhoneNumber:
                        break;
                    case AuthError.MissingPhoneNumber:
                        break;
                    case AuthError.InvalidRecipientEmail:
                        break;
                    case AuthError.InvalidSender:
                        break;
                    case AuthError.InvalidVerificationCode:
                        break;
                    case AuthError.InvalidVerificationId:
                        break;
                    case AuthError.MissingVerificationCode:
                        break;
                    case AuthError.MissingVerificationId:
                        break;
                    case AuthError.MissingEmail:
                        break;
                    case AuthError.MissingPassword:
                        break;
                    case AuthError.QuotaExceeded:
                        break;
                    case AuthError.RetryPhoneAuth:
                        break;
                    case AuthError.SessionExpired:
                        break;
                    case AuthError.AppNotVerified:
                        break;
                    case AuthError.AppVerificationFailed:
                        break;
                    case AuthError.CaptchaCheckFailed:
                        break;
                    case AuthError.InvalidAppCredential:
                        break;
                    case AuthError.MissingAppCredential:
                        break;
                    case AuthError.InvalidClientId:
                        break;
                    case AuthError.InvalidContinueUri:
                        break;
                    case AuthError.MissingContinueUri:
                        break;
                    case AuthError.KeychainError:
                        break;
                    case AuthError.MissingAppToken:
                        break;
                    case AuthError.MissingIosBundleId:
                        break;
                    case AuthError.NotificationNotForwarded:
                        break;
                    case AuthError.UnauthorizedDomain:
                        break;
                    case AuthError.WebContextAlreadyPresented:
                        break;
                    case AuthError.WebContextCancelled:
                        break;
                    case AuthError.DynamicLinkNotActivated:
                        break;
                    case AuthError.Cancelled:
                        break;
                    case AuthError.InvalidProviderId:
                        break;
                    case AuthError.WebInternalError:
                        break;
                    case AuthError.WebStorateUnsupported:
                        break;
                    case AuthError.TenantIdMismatch:
                        break;
                    case AuthError.UnsupportedTenantOperation:
                        break;
                    case AuthError.InvalidLinkDomain:
                        break;
                    case AuthError.RejectedCredential:
                        break;
                    case AuthError.PhoneNumberNotFound:
                        break;
                    case AuthError.InvalidTenantId:
                        break;
                    case AuthError.MissingClientIdentifier:
                        break;
                    case AuthError.MissingMultiFactorSession:
                        break;
                    case AuthError.MissingMultiFactorInfo:
                        break;
                    case AuthError.InvalidMultiFactorSession:
                        break;
                    case AuthError.MultiFactorInfoNotFound:
                        break;
                    case AuthError.AdminRestrictedOperation:
                        break;
                    case AuthError.UnverifiedEmail:
                        break;
                    case AuthError.SecondFactorAlreadyEnrolled:
                        break;
                    case AuthError.MaximumSecondFactorCountExceeded:
                        break;
                    case AuthError.UnsupportedFirstFactor:
                        break;
                    case AuthError.EmailChangeNeedsVerification:
                        break;
                    default:
                        break;
                }
            }
            else
            {
                print("Email successfully send");
                ShowErrorMessage("Registro exitoso. Por favor, verifica tu correo electrónico utilizando el enlace enviado.");
            }
        }
    }


    #endregion

    #region Login
    public void Login(ClickEvent evt)
    {

        FirebaseAuth auth = FirebaseAuth.DefaultInstance;
        string email = this.email.value;
        string password = this.password.value;

        Credential credential =
        EmailAuthProvider.GetCredential(email, password);
        auth.SignInAndRetrieveDataWithCredentialAsync(credential).ContinueWithOnMainThread(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync was canceled.");
                lb_warning.text = "SignInAndRetrieveDataWithCredentialAsync was canceled.";
                return;
            }
            if (task.IsFaulted)
            {
              
                Debug.LogError("SignInAndRetrieveDataWithCredentialAsync encountered an error: " + task.Exception);
                lb_warning.text = "Verificar el correo o la contraseña";
                return;
            }
            AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

            StartCoroutine(CheckEmailVerificationStatus(result.User));
        });

    }

    IEnumerator CheckEmailVerificationStatus(FirebaseUser user)
    {
        yield return new WaitForSeconds(2); // Espera 2 segundos para dar tiempo al correo electrónico para que se verifique

        if (user.IsEmailVerified)
        {
            
            SetLoginStatus("Inicio de sesión exitoso.");
            EnterInApp();
        }
        else
        {
            SetLoginStatus("Por favor, verifica tu correo electrónico.");
        }
    }

    private void SetLoginStatus(string status)
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            lb_warning.text = status;
        });
    }
    #endregion

    #region Google
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
                EnterInApp();


            });
        }
    }

    void EnterInApp()
    {
        SceneManager.LoadScene("MenuLenti", LoadSceneMode.Single);
        interfaces.visualTreeAsset = foods;
        Objects comida = GetComponent<Objects>();
        comida.enabled = true;
    }

    
    #endregion

}