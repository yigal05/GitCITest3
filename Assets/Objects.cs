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
    private Button comprar;
    
    public static int selectPlate;
    
    private void OnEnable()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        foodsScreen = root.Q<VisualElement>("Principal");
        starScreen = root.Q<VisualElement>("Start");
        pageTitle = root.Q<Label>("pageTitle");
        goBack = root.Q<VisualElement>("GoBack");
        comprar = root.Q<Button>("Comprar");
        
        DishMenu principio = new DishMenu(root , "Principio","Principio","ButtonPrin");
        DishMenu acompanante = new DishMenu(root , "Acompanante","Acompañante","ButtonAcom");
        DishMenu proteina = new DishMenu(root , "Proteina","Proteina","ButtonPro");
        DishMenu sopa = new DishMenu(root , "Sopas","Sopa","ButtonSo");
        DishMenu bebida = new DishMenu(root , "Bebidas","Bebidas","ButtonBe");
        
        principio.getButton().RegisterCallback<ClickEvent,DishMenu>(ChangePage , principio);
        acompanante.getButton().RegisterCallback<ClickEvent,DishMenu>(ChangePage , acompanante);
        proteina.getButton().RegisterCallback<ClickEvent,DishMenu>(ChangePage , proteina);
        sopa.getButton().RegisterCallback<ClickEvent,DishMenu>(ChangePage , sopa);
        bebida.getButton().RegisterCallback<ClickEvent,DishMenu>(ChangePage , bebida);
        goBack.RegisterCallback<ClickEvent>(GoBack);
        
        //platos 

        Dish lentejas = new Dish(root, "Lentejas","lentejas", principio.getButton());
        Dish frijoles = new Dish(root, "Frijoles","Frijoles", principio.getButton());
        Dish pasta = new Dish(root, "Pasta","Pasta", principio.getButton());
        
        Dish Arroz = new Dish(root, "Arroz","Arroz", acompanante.getButton());
        Dish Pure = new Dish(root, "Pure","Pure", acompanante.getButton());
        
        Dish pollo = new Dish(root, "Pollo","Pollo", proteina.getButton());
        Dish cerdo = new Dish(root, "Cerdo","Cerdo", proteina.getButton());
        Dish pavo = new Dish(root, "Pavo","Pavo", proteina.getButton());
        Dish pescado = new Dish(root, "Pescado","Pescado", proteina.getButton());
        Dish huevo = new Dish(root, "Huevo","Huevo", proteina.getButton());
        
        Dish sancocho = new Dish(root, "Sancocho","Sancocho", sopa.getButton());
        Dish ajiaco = new Dish(root, "Ajiaco","Ajiaco", sopa.getButton());
        Dish sopaPollo = new Dish(root, "SopaPollo","Sopa De Pollo", sopa.getButton());
        
        Dish limonada = new Dish(root, "Limonada","Limonada", bebida.getButton());
        Dish mango = new Dish(root, "Mango","Mango", bebida.getButton());
        Dish mora = new Dish(root, "Mora","Mora", bebida.getButton());
        Dish fresa = new Dish(root, "Fresa","Fresa", bebida.getButton());
        Dish cafe = new Dish(root, "Cafe","Café", bebida.getButton());
        
        
        // eventos de seleccion de comida 
        
        //Principio
        lentejas.buttonToSelect().RegisterCallback<ClickEvent>(lentejas.updateTitle);
        frijoles.buttonToSelect().RegisterCallback<ClickEvent>(frijoles.updateTitle);
        pasta.buttonToSelect().RegisterCallback<ClickEvent>(pasta.updateTitle);
        lentejas.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        frijoles.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        pasta.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        
        //Acompañante
        Arroz.buttonToSelect().RegisterCallback<ClickEvent>(Arroz.updateTitle);
        Pure.buttonToSelect().RegisterCallback<ClickEvent>(Pure.updateTitle);
        Arroz.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        Pure.buttonToSelect().RegisterCallback<ClickEvent>(GoBack); 
        
        //Proteina 
        pollo.buttonToSelect().RegisterCallback<ClickEvent>(pollo.updateTitle);
        cerdo.buttonToSelect().RegisterCallback<ClickEvent>(cerdo.updateTitle);
        pavo.buttonToSelect().RegisterCallback<ClickEvent>(pavo.updateTitle);
        pescado.buttonToSelect().RegisterCallback<ClickEvent>(pescado.updateTitle);
        huevo.buttonToSelect().RegisterCallback<ClickEvent>(huevo.updateTitle);
        pollo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        cerdo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack); 
        pavo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        pescado.buttonToSelect().RegisterCallback<ClickEvent>(GoBack); 
        huevo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        
        //Sopas
        sancocho.buttonToSelect().RegisterCallback<ClickEvent>(sancocho.updateTitle);
        ajiaco.buttonToSelect().RegisterCallback<ClickEvent>(ajiaco.updateTitle);
        sopaPollo.buttonToSelect().RegisterCallback<ClickEvent>(sopaPollo.updateTitle);
        sancocho.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        ajiaco.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        sopaPollo.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        
        //Bebidas 
        limonada.buttonToSelect().RegisterCallback<ClickEvent>(limonada.updateTitle);
        mango.buttonToSelect().RegisterCallback<ClickEvent>(mango.updateTitle);
        mora.buttonToSelect().RegisterCallback<ClickEvent>(mora.updateTitle);
        fresa.buttonToSelect().RegisterCallback<ClickEvent>(fresa.updateTitle);
        cafe.buttonToSelect().RegisterCallback<ClickEvent>(cafe.updateTitle);
        limonada.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        mango.buttonToSelect().RegisterCallback<ClickEvent>(GoBack); 
        mora.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        fresa.buttonToSelect().RegisterCallback<ClickEvent>(GoBack); 
        cafe.buttonToSelect().RegisterCallback<ClickEvent>(GoBack);
        
    }

    void ChangePage(ClickEvent evt , DishMenu seleccionado)
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
        if (selectPlate == 5) { comprar.style.display = DisplayStyle.Flex; };
    }
     
}


class  DishMenu
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

class Dish
{
    private VisualElement comidaSeleccionada;
    string title;
    private Button campoACambiar;

    public Dish(VisualElement root, string selectedName,string _title  , Button buttonCambiar)
    {
        comidaSeleccionada = root.Q<VisualElement>(selectedName);
        title = _title;
        campoACambiar = buttonCambiar;
    }

    public void updateTitle(ClickEvent evt)
    {
        campoACambiar.text = title;
        if ( campoACambiar.style.backgroundColor != Color.green){Objects.selectPlate += 1;}
        campoACambiar.style.backgroundColor = new StyleColor(Color.green);

    }

    public VisualElement buttonToSelect()
    {
        return comidaSeleccionada;
    }
}




