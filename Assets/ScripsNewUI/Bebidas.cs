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



    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuy
    private void Start()
    {
        listaDeOpciones = new List<Bebida>();
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("limonada"), "Limonada", "Limonada, una refrescante bebida preparada con jugo de limón recién exprimido, endulzada con aguapanela y mezclada con agua fría. Una opción deliciosa y revitalizante para calmar la sed en días calurosos"));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("mora"), "Mora", "Jugo de mora, una bebida refrescante hecha con jugo natural de mora, endulzado con azúcar y servido sobre hielo. Una delicia frutal con un toque dulce y ácido que deleita el paladar."));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("frijoles"), "Fresa", "Jugo de fresa, una bebida deliciosa y vibrante hecha con jugo fresco de fresas maduras, endulzado con un toque de azúcar y servido con hielo. Refrescante y lleno de sabor, es una opción perfecta para satisfacer los antojos de frutas."));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("cafe"), "Cafe", "Café, una bebida caliente y reconfortante preparada con granos de café tostado y molido, infusionados con agua caliente. Con su aroma tentador y su sabor robusto, el café es el compañero perfecto para empezar el día o disfrutar de un momento de tranquilidad."));
        listaDeOpciones.Add(new Bebida(Resources.Load<Sprite>("mango"), "Mango", "Jugo de mango, una bebida tropical y exótica elaborada con jugo fresco de mango, mezclado con hielo para crear una bebida refrescante y llena de sabor. Ideal para disfrutar en días soleados."));

        DishToBuy.Intance.bebidas.RegisterCallback<ClickEvent>(Showprincio);
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

    void ElegirThis(ClickEvent evt)
    {
        DishToBuy.Intance.plato.bebidas = listaDeOpciones[id].titulo;
        baack(new ClickEvent());
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
        DishToBuy.Intance.back.UnregisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.next.UnregisterCallback<ClickEvent>(goNextOption);
        //DishToBuy.Intance.ApplySelectedStyle(DishToBuy.Intance.bebidas, false);
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
