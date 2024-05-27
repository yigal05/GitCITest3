using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Firebase.Database;
using Google;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public struct botonV2
{
    public VisualElement botonPlatoV2;
    public VisualElement bordenbotonV2;
}
namespace ScripsNewUI
{
    public class DishToBuyV2 : MonoBehaviour
    {

        //singleton
        public static DishToBuyV2 Intance;
        // botonV2es que seran presionados para ir a ese menu
        public botonV2 principio;
        public botonV2 acompanante;
        public botonV2 proteina;
        public botonV2 sopa;
        public botonV2 bebidas;
        public VisualElement root;
        public VisualElement back;
        public DishChoosenV2 plato;
        public Button logoutButton, cerrarButtonFinal;
        public VisualElement userButton;

        // Los campos que se modificaran en cada menu
        public VisualElement foodImage, foodImageBuy;
        public Label titleFood, descriptionFood;

        //screen que esta actualmente por defecto deberia ser main
        private VisualElement currentScreen;

        public VisualElement mainScreen, foodScreen, appScreen, welcomeScreen, topBanner;
        public ScrollView buyScreen, plateScreen;
        public VisualElement next, chosee, chooseLunch, logoutPanel, compraFinalPanel;

        public OrderManager ordenar;
        public Label comprar;
        private DatabaseReference databaseReference;

        public Button AnadirButton;
        public Label EditarPlato;
        private FirebaseAuth auth;
        private GoogleSignInConfiguration configuration;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                print($"se ha elegido {plato.principio}, {plato.acompanante}, {plato.proteina}, {plato.sopa}, {plato.bebidas}");
            }
        }

        private void Awake()
        {
            if (Intance != null && Intance != this)
            {
                Destroy(this);
            }
            else
            {
                Intance = this;
                SceneManager.sceneLoaded += OnSceneLoaded;


            }
        }

        private void InitializeUIElements()
        {
            Debug.Log("DishToBuy: InitializeUIElements llamado");

            root = GetComponent<UIDocument>().rootVisualElement;

            foodImage = root.Q<VisualElement>("imageFood-n");
            foodImageBuy = root.Q<VisualElement>("FoodImageBuy");
            titleFood = root.Q<Label>("titleFood-n");
            descriptionFood = root.Q<Label>("foodDescription-n");

            mainScreen = root.Q<VisualElement>("MainScreen");
            foodScreen = root.Q<VisualElement>("FoodScreen");
            appScreen = root.Q<VisualElement>("App");
            welcomeScreen = root.Q<VisualElement>("Welcome");
            topBanner = root.Q<VisualElement>("Header");
            buyScreen = root.Q<ScrollView>("BuyScreen");

            userButton = root.Q<VisualElement>("usuario2");
            logoutButton = root.Q<Button>("cerrarSesionButton2");
            logoutPanel = root.Q<VisualElement>("CerrarSesion2");
            compraFinalPanel = root.Q<VisualElement>("GraciasPorComprar");
            cerrarButtonFinal = root.Q<Button>("CerrarButtonFinal");

            next = root.Q<VisualElement>("next");
            back = root.Q<VisualElement>("back");
            chosee = root.Q<VisualElement>("Choose-n");
            AnadirButton = root.Q<Button>("anadirButton");
            plato = new DishChoosenV2();


            principio.botonPlatoV2 = root.Q<VisualElement>("Principio-n");
            principio.bordenbotonV2 = root.Q<VisualElement>("bordePrincipio");

            acompanante.botonPlatoV2 = root.Q<VisualElement>("Acompanante-n");
            acompanante.bordenbotonV2 = root.Q<VisualElement>("bordeAcompanate");

            proteina.botonPlatoV2 = root.Q<VisualElement>("Proteina-n");
            proteina.bordenbotonV2 = root.Q<VisualElement>("bordeProteina");

            sopa.botonPlatoV2 = root.Q<VisualElement>("Sopas-n");
            sopa.bordenbotonV2 = root.Q<VisualElement>("bordeSopa");

            bebidas.botonPlatoV2 = root.Q<VisualElement>("Bebidas-n");
            bebidas.bordenbotonV2 = root.Q<VisualElement>("bordeBebidas");

            //activar
            //chooseLunch = root.Q<VisualElement>("goApp_n");
            //chooseLunch.RegisterCallback<ClickEvent>(GoToMainMenu);

            userButton.RegisterCallback<ClickEvent>(ShowLogoutPanel);
            logoutButton.RegisterCallback<ClickEvent>(Logout);
            cerrarButtonFinal.RegisterCallback<ClickEvent>(CerrarBuyDish);

            comprar = root.Q<Label>("comprar");
            comprar.RegisterCallback<ClickEvent>(BuyDish);

            EditarPlato = root.Q<Label>("editarButton");
            EditarPlato.RegisterCallback<ClickEvent>(EditDish);

            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

            auth = FirebaseAuth.DefaultInstance;

            configuration = new GoogleSignInConfiguration
            {
                WebClientId = "737799834196-lg6mapd3763f7abnsfm2v242qbpk77el.apps.googleusercontent.com",
                RequestIdToken = true
            };

            Debug.Log("DishToBuy: InitializeUIElements completado");
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "UI2")
            {
                Debug.Log("DishToBuy: OnSceneLoaded llamado para dishToBuy");
                InitializeUIElements();
                appScreen.style.display = DisplayStyle.Flex;
                mainScreen.style.display = DisplayStyle.Flex;
                // Registra los eventos nuevamente
            }
        }

        public void GoToMainMenu(ClickEvent evt)
        {
            welcomeScreen.style.display = DisplayStyle.None;
            appScreen.style.display = DisplayStyle.Flex;
        }

        private void ShowLogoutPanel(ClickEvent evt)
        {
            mainScreen.style.display = DisplayStyle.None;
            logoutPanel.style.display = DisplayStyle.Flex;
            Debug.Log("boton presionado");
        }

        private void Logout(ClickEvent evt)
        {
            try
            {
                ResetUIElements();
                auth.SignOut();
                GoogleSignIn.DefaultInstance.SignOut();


                ResetDishes();

                SceneManager.LoadScene("LogRegScene");
            }
            catch (Exception e)
            {
                Debug.LogError("Error during logout: " + e.Message);
            }
        }

        private void ResetUIElements()
        {
            mainScreen.style.display = DisplayStyle.None;
            logoutPanel.style.display = DisplayStyle.None;
            appScreen.style.display = DisplayStyle.None;
            // Restablece cualquier otro elemento de UI si es necesario
        }

        public void goBuy()
        {
            if (plato.AreAllFieldsNotNull())
            {
                buyScreen.style.display = DisplayStyle.Flex;
                mainScreen.style.display = DisplayStyle.None;
                topBanner.style.display = DisplayStyle.None;
                Label description = root.Q<Label>("DishDescription");
                description.text =
                    $"{plato.principio} , {plato.acompanante} , {plato.proteina} , {plato.sopa}, {plato.bebidas}";
                ChangeVisualElementImage($"{plato.principio}");


            }
        }

        void ChangeVisualElementImage(string spritePath)
        {
            // Carga el Sprite desde la carpeta Resources
            Sprite sprite = Resources.Load<Sprite>(spritePath);

            // Verifica si el Sprite se ha cargado correctamente
            if (sprite != null)
            {
                // Crea una nueva imagen de fondo con el Sprite
                var backgroundImage = new StyleBackground(sprite);

                // Asigna la nueva imagen de fondo al VisualElement
                foodImageBuy.style.backgroundImage = backgroundImage;
            }
            else
            {
                Debug.LogError("Sprite not found at path: " + spritePath);
            }
        }

        void EditDish(ClickEvent evt)
        {
            buyScreen.style.display = DisplayStyle.None;
            mainScreen.style.display = DisplayStyle.Flex;
            topBanner.style.display = DisplayStyle.Flex;
            ResetDishes();
        }

        void BuyDish(ClickEvent evr)
        {
            ordenar.AddToOrder(plato.principio);
            ordenar.AddToOrder(plato.acompanante);
            ordenar.AddToOrder(plato.proteina);
            ordenar.AddToOrder(plato.sopa);
            ordenar.AddToOrder(plato.bebidas);
            ordenar.ConfirmOrder();
            buyScreen.style.display = DisplayStyle.None;
            compraFinalPanel.style.display = DisplayStyle.Flex;
        }

        void CerrarBuyDish(ClickEvent evt)
        {
            compraFinalPanel.style.display = DisplayStyle.None;
            mainScreen.style.display = DisplayStyle.Flex;
            topBanner.style.display = DisplayStyle.Flex;
            ResetDishes();
        }

        void ResetDishes()
        {
            plato.principio = null; principio.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 36));
            plato.acompanante = null; acompanante.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 36));
            plato.proteina = null; proteina.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 36));
            plato.bebidas = null; bebidas.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 36));
            plato.sopa = null; sopa.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 36));
        }




    }
}

public class DishChoosenV2
{
    public string principio;
    public string acompanante;
    public string proteina;
    public string sopa;
    public string bebidas;

    public bool AreAllFieldsNotNull()
    {
        return principio != null && acompanante != null && proteina != null && sopa != null && bebidas != null;
    }


}