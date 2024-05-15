using UnityEngine;
using UnityEngine.UIElements;

namespace ScripsNewUI
{
    public class DishToBuy : MonoBehaviour
    {
        //singleton
        public static DishToBuy Intance;
        // Botones que seran presionados para ir a ese menu
        public VisualElement principio, acompanante, proteina, sopa, bebidas;
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
                principio = root.Q<VisualElement>("Principio-n");
                acompanante = root.Q<VisualElement>("Acompanante-n");
                proteina = root.Q<VisualElement>("Proteina-n");
                sopa = root.Q<VisualElement>("Sopas-n");
                bebidas = root.Q<VisualElement>("Bebidas-n");
                foodImage = root.Q<VisualElement>("imageFood-n");
                titleFood = root.Q<Label>("titleFood-n");
                descriptionFood = root.Q<Label>("foodDescription-n");
                
                mainScreen = root.Q<VisualElement>("MainScreen");
                foodScreen = root.Q<VisualElement>("FoodScreen");
                next = root.Q<VisualElement>("next");
                back = root.Q<VisualElement>("back");
                chosee = root.Q<VisualElement>("Choose-n");
                plato = new DishChoosen();
            }
        }

        public void imprimirTin()
        {
            print($" {plato.principio} {plato.acompanate} {plato.proteina}");
        }
    }
}

public class DishChoosen
{
    public string principio;
    public string acompanate;
    public string proteina;
    public string sopa;
    public string bebida;
}
