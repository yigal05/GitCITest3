using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;

public class Sopa : MonoBehaviour
{
    private int id = 0;
    public List<Sopas> listaDeOpciones;


    public Button ajiaco, sancocho, sopaDePollo;
    public Button AnadirAjiaco, AnadirSancocho, AnadirSopadepollo;

    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuy
    private void Start()
    {
        ajiaco = DishToBuy.Intance.root.Q<Button>("infAjiaco");
        sancocho = DishToBuy.Intance.root.Q<Button>("InfSancocho");
        sopaDePollo = DishToBuy.Intance.root.Q<Button>("infSopaPollo");
        AnadirAjiaco = DishToBuy.Intance.root.Q<Button>("anadirAjiaco");
        AnadirSancocho = DishToBuy.Intance.root.Q<Button>("anadirSancocho");
        AnadirSopadepollo = DishToBuy.Intance.root.Q<Button>("anadirSopadepollo");

        listaDeOpciones = new List<Sopas>();
        listaDeOpciones.Add(new Sopas(Resources.Load<Sprite>("Ajiaco"), "Ajiaco", "Ajiaco, una sabrosa sopa colombiana, hecha con pollo, papas, maíz, yuca y guascas, sazonada con cilantro y servida con alcaparras y crema. Una delicia reconfortante y llena de sabor que representa la riqueza culinaria de Colombia."));
        listaDeOpciones.Add(new Sopas(Resources.Load<Sprite>("Sancocho"), "Sancocho", "Sancocho, un guiso tradicional latinoamericano, preparado con una variedad de carnes, plátanos, yuca, ñame, maíz y otras verduras, cocidas lentamente en un caldo aromático sazonado con hierbas y especias. Un plato reconfortante y abundante que es una verdadera fiesta para el paladar"));
        listaDeOpciones.Add(new Sopas(Resources.Load<Sprite>("Sopa de Pollo"), "Sopa de Pollo", "Sopa de pollo, un plato reconfortante hecho con trozos tiernos de pollo, verduras frescas como zanahorias, apio y cebolla, cocidas en un caldo sabroso y sazonado con hierbas aromáticas. Una opción nutritiva y satisfactoria para cualquier ocasión."));

        //DishToBuy.Intance.sopa.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
        DishToBuy.Intance.sopa.botonPlato.RegisterCallback<ClickEvent>(ShowListPrincipio);
        ajiaco.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        sancocho.RegisterCallback<ClickEvent,int>(Showprincio, 1);
        sopaDePollo.RegisterCallback<ClickEvent,int>(Showprincio, 2);

        AnadirAjiaco.RegisterCallback<ClickEvent, int>(AnadirPlato, 0);
        AnadirSancocho.RegisterCallback<ClickEvent, int>(AnadirPlato, 1);
        AnadirSopadepollo.RegisterCallback<ClickEvent, int>(AnadirPlato, 2);
    }

    void Showprincio(ClickEvent evt)
    {
        DishToBuy.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuy.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        DishToBuy.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }
    
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuy.Intance.plateScreen = DishToBuy.Intance.root.Q<ScrollView>("sopasScreen");
        DishToBuy.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.plateScreen.style.display = DisplayStyle.Flex;
        DishToBuy.Intance.foodScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }
    void ElegirThis(ClickEvent evt)
    {
        DishToBuy.Intance.plato.sopa = listaDeOpciones[id].titulo;
        DishToBuy.Intance.sopa.bordenBoton.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuy.Intance.goBuy();
    }

    void goNextOption(ClickEvent evt)
    {
        id++;
        if (id == listaDeOpciones.Count)
            id = 0;

        // if (id == -1)
        // id = listaDeOpciones.Count

        print($"la opcion elegida actualmente es {listaDeOpciones[id].titulo}");
        ChangeMainScreen(listaDeOpciones[id]);
    }
    
    void Showprincio(ClickEvent evt, int _id)
    {
        DishToBuy.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.plateScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuy.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = _id;
        ChangeMainScreen(listaDeOpciones[id]);
    }
    void ChangeMainScreen(Sopas choosenOption)
    {
        DishToBuy.Intance.foodImage.style.backgroundImage = new StyleBackground(choosenOption.imagen);
        DishToBuy.Intance.titleFood.text = choosenOption.titulo;
        DishToBuy.Intance.descriptionFood.text = choosenOption.descriptivo;
    }

    void baack(ClickEvent evt)
    {
        id = 0;
        DishToBuy.Intance.mainScreen.style.display = DisplayStyle.Flex;
        DishToBuy.Intance.foodScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.plateScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.back.UnregisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.next.UnregisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
    }

    void AnadirPlato(ClickEvent evt, int _id)
    {
        DishToBuy.Intance.plato.sopa = listaDeOpciones[_id].titulo;
        DishToBuy.Intance.sopa.bordenBoton.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 110));

        // Regresar a la pantalla principal después de añadir el plato
        baack(evt);

        // Aquí puedes añadir cualquier otra lógica que necesites antes de ir a la pantalla de compra
        DishToBuy.Intance.goBuy();
    }
}

public class Sopas
{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public Sopas(Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }

}
