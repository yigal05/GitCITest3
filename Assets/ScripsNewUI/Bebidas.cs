using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;

public class Bebidas : MonoBehaviour
{
    private int id = 0;
    public List<Bebida> listaDeOpciones;


    public Button limonada,mora,fresa,cafe,mango;
    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuy
    private void Start()
    {
        limonada = DishToBuy.Intance.root.Q<Button>("infLimonada");
        mora = DishToBuy.Intance.root.Q<Button>("InfMora");
        fresa = DishToBuy.Intance.root.Q<Button>("infFresa");
        cafe = DishToBuy.Intance.root.Q<Button>("infCafe");
        mango = DishToBuy.Intance.root.Q<Button>("infMango");
        listaDeOpciones = new List<Bebida>();
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("Limonada"), "Limonada", "Limonada, una refrescante bebida preparada con jugo de lim�n reci�n exprimido, endulzada con aguapanela y mezclada con agua fr�a. Una opci�n deliciosa y revitalizante para calmar la sed en d�as calurosos"));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("Mora"), "Mora", "Jugo de mora, una bebida refrescante hecha con jugo natural de mora, endulzado con az�car y servido sobre hielo. Una delicia frutal con un toque dulce y �cido que deleita el paladar."));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("Frijoles"), "Fresa", "Jugo de fresa, una bebida deliciosa y vibrante hecha con jugo fresco de fresas maduras, endulzado con un toque de az�car y servido con hielo. Refrescante y lleno de sabor, es una opci�n perfecta para satisfacer los antojos de frutas."));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("Cafe"), "Cafe", "Caf�, una bebida caliente y reconfortante preparada con granos de caf� tostado y molido, infusionados con agua caliente. Con su aroma tentador y su sabor robusto, el caf� es el compa�ero perfecto para empezar el d�a o disfrutar de un momento de tranquilidad."));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("Mango"), "Mango", "Jugo de mango, una bebida tropical y ex�tica elaborada con jugo fresco de mango, mezclado con hielo para crear una bebida refrescante y llena de sabor. Ideal para disfrutar en d�as soleados."));

        //DishToBuy.Intance.bebidas.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
        DishToBuy.Intance.bebidas.botonPlato.RegisterCallback<ClickEvent>(ShowListPrincipio);
        limonada.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        mora.RegisterCallback<ClickEvent,int>(Showprincio, 1);
        fresa.RegisterCallback<ClickEvent,int>(Showprincio, 2);
        cafe.RegisterCallback<ClickEvent,int>(Showprincio, 3);
        mango.RegisterCallback<ClickEvent,int>(Showprincio, 4);
    }

    void Showprincio(ClickEvent evt)
    {
        DishToBuy.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuy.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        DishToBuy.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        //DishToBuy.Intance.ApplySelectedStyle(DishToBuy.Intance.bebidas, true);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
        
    }
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuy.Intance.plateScreen = DishToBuy.Intance.root.Q<ScrollView>("bebidasScreen");
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
        DishToBuy.Intance.plato.bebidas = listaDeOpciones[id].titulo;
        DishToBuy.Intance.bebidas.bordenBoton.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
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
    void ChangeMainScreen(Bebida choosenOption)
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
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);;
    }
}

public class Bebida
{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public Bebida(Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }

}
