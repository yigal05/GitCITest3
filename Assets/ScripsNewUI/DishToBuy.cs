using System;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using Firebase.Database;

public struct boton
{
    public VisualElement botonPlato;
    public VisualElement bordenBoton;
}
namespace ScripsNewUI
{
    public class DishToBuy : MonoBehaviour
    {

        //singleton
        public static DishToBuy Intance;
        // Botones que seran presionados para ir a ese menu
        public boton principio;
        public boton acompanante;
        public boton proteina;
        public boton sopa;
        public boton bebidas;
        public VisualElement root;
        public VisualElement back;
        public DishChoosen plato;
        
        // Los campos que se modificaran en cada menu
        public VisualElement foodImage, foodImageBuy;
        public Label titleFood, descriptionFood;
        
        //screen que esta actualmente por defecto deberia ser main
        private VisualElement currentScreen;

        public VisualElement mainScreen, foodScreen,appScreen , welcomeScreen, topBanner;
        public ScrollView  buyScreen,plateScreen;
        public VisualElement next,chosee, chooseLunch;

        public OrderManager ordenar;
        public Label comprar;
        private DatabaseReference databaseReference;

        public Button AnadirButton;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                print($"se ha elegido {plato.principio} , {plato.acompanante} , {plato.proteina} , {plato.sopa}, {plato.bebidas}");
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
                
                next = root.Q<VisualElement>("next");
                back = root.Q<VisualElement>("back");
                chosee = root.Q<VisualElement>("Choose-n");
                AnadirButton = root.Q<Button>("anadirButton");
                plato = new DishChoosen();
                
                
                principio.botonPlato = root.Q<VisualElement>("Principio-n");
                principio.bordenBoton =root.Q<VisualElement>("bordePrincipio");
                
                acompanante.botonPlato = root.Q<VisualElement>("Acompanante-n");
                acompanante.bordenBoton =root.Q<VisualElement>("bordeAcompanate");
                
                proteina.botonPlato = root.Q<VisualElement>("Proteina-n");
                proteina.bordenBoton =root.Q<VisualElement>("bordeProteina");
                
                sopa.botonPlato = root.Q<VisualElement>("Sopas-n");
                sopa.bordenBoton =root.Q<VisualElement>("bordeSopa");
                
                bebidas.botonPlato = root.Q<VisualElement>("Bebidas-n");
                bebidas.bordenBoton =root.Q<VisualElement>("bordeBebidas");

                chooseLunch = root.Q<VisualElement>("goApp_n");
                chooseLunch.RegisterCallback<ClickEvent>(GoToMainMenu);
                
                comprar = root.Q<Label>("comprar");
                comprar.RegisterCallback<ClickEvent>(BuyDish);

                databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

            }
        }
        

        public void GoToMainMenu(ClickEvent evt)
        {
            welcomeScreen.style.display = DisplayStyle.None;
            appScreen.style.display = DisplayStyle.Flex;
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

        void BuyDish(ClickEvent evr)
        {
            ordenar.AddToOrder(plato.principio);
            ordenar.AddToOrder(plato.acompanante);
            ordenar.AddToOrder(plato.proteina);
            ordenar.AddToOrder(plato.sopa);
            ordenar.AddToOrder(plato.bebidas);
            ordenar.ConfirmOrder();
        }
        
        
        
        

    }
}

public class DishChoosen
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
