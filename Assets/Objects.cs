using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Objects : MonoBehaviour
{
    private VisualElement foodsScreen, starScreen, goBack;
    private VisualElement currentScreen;
    private Label pageTitle;
    private Button confirmOrderButton;
    private OrderManager ordenar;


    public static int selectPlate;

    private void OnEnable()
    {
        ordenar = GetComponent<OrderManager>();
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        foodsScreen = root.Q<VisualElement>("Principal");
        starScreen = root.Q<VisualElement>("Start");
        pageTitle = root.Q<Label>("pageTitle");
        goBack = root.Q<VisualElement>("GoBack");
        confirmOrderButton = root.Q<Button>("Comprar");

        DishMenu principio = new DishMenu(root, "Principio", "Principio", "ButtonPrin");
        DishMenu acompanante = new DishMenu(root, "Acompanante", "Acompañante", "ButtonAcom");
        DishMenu proteina = new DishMenu(root, "Proteina", "Proteina", "ButtonPro");
        DishMenu sopa = new DishMenu(root, "Sopas", "Sopa", "ButtonSo");
        DishMenu bebida = new DishMenu(root, "Bebidas", "Bebidas", "ButtonBe");

        principio.getButton().RegisterCallback<ClickEvent, DishMenu>(ChangePage, principio);
        acompanante.getButton().RegisterCallback<ClickEvent, DishMenu>(ChangePage, acompanante);
        proteina.getButton().RegisterCallback<ClickEvent, DishMenu>(ChangePage, proteina);
        sopa.getButton().RegisterCallback<ClickEvent, DishMenu>(ChangePage, sopa);
        bebida.getButton().RegisterCallback<ClickEvent, DishMenu>(ChangePage, bebida);
        goBack.RegisterCallback<ClickEvent>(GoBack);
        confirmOrderButton.RegisterCallback<ClickEvent>(ConfirmOrder);

        //platos 

        Dish lentejas = new Dish(root, "Lentejas", "Lentejas", principio.getButton(), ordenar);
        Dish frijoles = new Dish(root, "Frijoles", "Frijoles", principio.getButton(), ordenar);
        Dish pasta = new Dish(root, "Pasta", "Pasta", principio.getButton(), ordenar);

        Dish Arroz = new Dish(root, "Arroz", "Arroz", acompanante.getButton(), ordenar);
        Dish Pure = new Dish(root, "Pure", "Pure", acompanante.getButton(), ordenar);

        Dish pollo = new Dish(root, "Pollo", "Pollo", proteina.getButton(), ordenar);
        Dish cerdo = new Dish(root, "Cerdo", "Cerdo", proteina.getButton(), ordenar);
        Dish pavo = new Dish(root, "Pavo", "Pavo", proteina.getButton(), ordenar);
        Dish pescado = new Dish(root, "Pescado", "Pescado", proteina.getButton(), ordenar);
        Dish huevo = new Dish(root, "Huevo", "Huevo", proteina.getButton(), ordenar);

        Dish sancocho = new Dish(root, "Sancocho", "Sancocho", sopa.getButton(), ordenar);
        Dish ajiaco = new Dish(root, "Ajiaco", "Ajiaco", sopa.getButton(), ordenar);
        Dish sopaPollo = new Dish(root, "SopaPollo", "Sopa De Pollo", sopa.getButton(), ordenar);

        Dish limonada = new Dish(root, "Limonada", "Limonada", bebida.getButton(), ordenar);
        Dish mango = new Dish(root, "Mango", "Mango", bebida.getButton(), ordenar);
        Dish mora = new Dish(root, "Mora", "Mora", bebida.getButton(), ordenar);
        Dish fresa = new Dish(root, "Fresa", "Fresa", bebida.getButton(), ordenar);
        Dish cafe = new Dish(root, "Cafe", "Café", bebida.getButton(), ordenar);


        // eventos de seleccion de comida 

        //Principio
        lentejas.buttonToSelect().RegisterCallback<ClickEvent>(lentejas.UpdateTitle);
        frijoles.buttonToSelect().RegisterCallback<ClickEvent>(frijoles.UpdateTitle);
        pasta.buttonToSelect().RegisterCallback<ClickEvent>(pasta.UpdateTitle);
        lentejas.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        frijoles.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        pasta.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);

        //Acompañante
        Arroz.buttonToSelect().RegisterCallback<ClickEvent>(Arroz.UpdateTitle);
        Pure.buttonToSelect().RegisterCallback<ClickEvent>(Pure.UpdateTitle);
        Arroz.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        Pure.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);

        //Proteina 
        pollo.buttonToSelect().RegisterCallback<ClickEvent>(pollo.UpdateTitle);
        cerdo.buttonToSelect().RegisterCallback<ClickEvent>(cerdo.UpdateTitle);
        pavo.buttonToSelect().RegisterCallback<ClickEvent>(pavo.UpdateTitle);
        pescado.buttonToSelect().RegisterCallback<ClickEvent>(pescado.UpdateTitle);
        huevo.buttonToSelect().RegisterCallback<ClickEvent>(huevo.UpdateTitle);
        pollo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        cerdo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        pavo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        pescado.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        huevo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);

        //Sopas
        sancocho.buttonToSelect().RegisterCallback<ClickEvent>(sancocho.UpdateTitle);
        ajiaco.buttonToSelect().RegisterCallback<ClickEvent>(ajiaco.UpdateTitle);
        sopaPollo.buttonToSelect().RegisterCallback<ClickEvent>(sopaPollo.UpdateTitle);
        sancocho.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        ajiaco.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        sopaPollo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);

        //Bebidas 
        limonada.buttonToSelect().RegisterCallback<ClickEvent>(limonada.UpdateTitle);
        mango.buttonToSelect().RegisterCallback<ClickEvent>(mango.UpdateTitle);
        mora.buttonToSelect().RegisterCallback<ClickEvent>(mora.UpdateTitle);
        fresa.buttonToSelect().RegisterCallback<ClickEvent>(fresa.UpdateTitle);
        cafe.buttonToSelect().RegisterCallback<ClickEvent>(cafe.UpdateTitle);
        limonada.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        mango.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        mora.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        fresa.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        cafe.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);

    }

    private void ConfirmOrder(ClickEvent evt)
    {
        ordenar.ConfirmOrder();
    }

    void ChangePage(ClickEvent evt, DishMenu seleccionado)
    {
        if (currentScreen != null)
        {
            currentScreen.style.display = DisplayStyle.None;
        }
        currentScreen = seleccionado.getScreen();
        starScreen.style.display = DisplayStyle.None;
        foodsScreen.style.display = DisplayStyle.Flex;
        seleccionado.getScreen().style.display = DisplayStyle.Flex;
        pageTitle.text = seleccionado.getTitle();
    }

    public void GoBack(ClickEvent evt)
    {
        currentScreen.style.display = DisplayStyle.None;
        starScreen.style.display = DisplayStyle.Flex;
        foodsScreen.style.display = DisplayStyle.None;
        if (selectPlate == 5) { confirmOrderButton.style.display = DisplayStyle.Flex; };
    }

}


class DishMenu
{
    private VisualElement dishScreen;
    private string title;
    private Button Trigger;


    public DishMenu(VisualElement root, string _screenName, string _title, string _buttonName)
    {
        this.dishScreen = root.Q<VisualElement>(_screenName);
        this.Trigger = root.Q<Button>(_buttonName);
        this.title = _title;
    }

    public VisualElement getScreen()
    {
        return this.dishScreen;
    }

    public string getTitle()
    {
        return this.title;
    }

    public Button getButton()
    {
        return Trigger;
    }
}

class Dish : MonoBehaviour
{
    private VisualElement comidaSeleccionada;
    string title;
    private Button campoACambiar;
    private OrderManager ordenar;
    private bool selected = false;

    public Dish(VisualElement root, string selectedName, string _title, Button buttonCambiar, OrderManager manager)
    {
        comidaSeleccionada = root.Q<VisualElement>(selectedName);
        title = _title;
        campoACambiar = buttonCambiar;
        ordenar = manager;

    }

    public void UpdateTitle(ClickEvent evt)
    {
        if (!selected)
        {
            campoACambiar.text = title;
            Objects.selectPlate += 1;
            campoACambiar.style.backgroundColor = new StyleColor(Color.green);
            ordenar.AddToOrder(title);
        }
        else
        {
            campoACambiar.text = "";
            Objects.selectPlate -= 1;
            campoACambiar.style.backgroundColor = new StyleColor(Color.white);
            ordenar.RemoveFromOrder(title);
        }

        selected = !selected;
    }

    public VisualElement buttonToSelect()
    {
        return comidaSeleccionada;
    }
}
