using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

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
        private VisualElement root;
        public VisualElement back;
        public DishChoosen plato;
        
        // Los campos que se modificaran en cada menu
        public VisualElement foodImage;
        public Label titleFood, descriptionFood;
        
        //screen que esta actualmente por defecto deberia ser main
        private VisualElement currentScreen;

        public VisualElement mainScreen, foodScreen;
        public VisualElement next;

        public VisualElement chosee;
        

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
                titleFood = root.Q<Label>("titleFood-n");
                descriptionFood = root.Q<Label>("foodDescription-n");
                
                mainScreen = root.Q<VisualElement>("MainScreen");
                foodScreen = root.Q<VisualElement>("FoodScreen");
                next = root.Q<VisualElement>("next");
                back = root.Q<VisualElement>("back");
                chosee = root.Q<VisualElement>("Choose-n");
                plato = new DishChoosen();
                
                principio.botonPlato = root.Q<VisualElement>("Principio-n");
                principio.bordenBoton =root.Q<VisualElement>("iconElement-P");
                
                acompanante.botonPlato = root.Q<VisualElement>("Acompanante-n");
                acompanante.bordenBoton =root.Q<VisualElement>("iconElement-P");
                
                proteina.botonPlato = root.Q<VisualElement>("Proteina-n");
                proteina.bordenBoton =root.Q<VisualElement>("iconElement-P");
                
                sopa.botonPlato = root.Q<VisualElement>("Sopas-n");
                sopa.bordenBoton =root.Q<VisualElement>("iconElement-P");
                
                bebidas.botonPlato = root.Q<VisualElement>("Bebidas-n");
                bebidas.bordenBoton =root.Q<VisualElement>("iconElement-P");

            }
        }

        public void imprimirTin()
        {
            print($" {plato.principio} {plato.acompanate} {plato.proteina} {plato.sopa} {plato.bebidas}");
        }

    }
}

public class DishChoosen
{
    public string principio;
    public string acompanate;
    public string proteina;
    public string sopa;
    public string bebidas;
}
