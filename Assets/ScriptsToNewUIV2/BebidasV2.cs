using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;

public class BebidasV2 : MonoBehaviour
{
    private int id = 0;
    public List<BebidaV2> listaDeOpciones;


    public Button limonada,mora,fresa,cafe,mango;
    public Button AnadirLimonada, AnadirMora, AnadirFresa, AnadirCafe, AnadirMango;
    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuyV2
    private void Start()
    {
        limonada = DishToBuyV2.Intance.root.Q<Button>("infLimonada");
        mora = DishToBuyV2.Intance.root.Q<Button>("InfMora");
        fresa = DishToBuyV2.Intance.root.Q<Button>("infFresa");
        cafe = DishToBuyV2.Intance.root.Q<Button>("infCafe");
        mango = DishToBuyV2.Intance.root.Q<Button>("infMango");
        AnadirLimonada = DishToBuyV2.Intance.root.Q<Button>("anadirLimonada");
        AnadirMora = DishToBuyV2.Intance.root.Q<Button>("anadirMora");
        AnadirFresa = DishToBuyV2.Intance.root.Q<Button>("anadirFresa");
        AnadirCafe = DishToBuyV2.Intance.root.Q<Button>("anadirCafe");
        AnadirMango = DishToBuyV2.Intance.root.Q<Button>("anadirMango");


        listaDeOpciones = new List<BebidaV2>();
        listaDeOpciones.Add(new BebidaV2(Resources.Load<Sprite>("Limonada"), "Limonada", "Limonada, una refrescante BebidaV2 preparada con jugo de lim�n reci�n exprimido, endulzada con aguapanela y mezclada con agua fr�a. Una opci�n deliciosa y revitalizante para calmar la sed en d�as calurosos"));
        listaDeOpciones.Add(new BebidaV2(Resources.Load<Sprite>("Mora"), "Mora", "Jugo de mora, una BebidaV2 refrescante hecha con jugo natural de mora, endulzado con az�car y servido sobre hielo. Una delicia frutal con un toque dulce y �cido que deleita el paladar."));
        listaDeOpciones.Add(new BebidaV2(Resources.Load<Sprite>("Frijoles"), "Fresa", "Jugo de fresa, una BebidaV2 deliciosa y vibrante hecha con jugo fresco de fresas maduras, endulzado con un toque de az�car y servido con hielo. Refrescante y lleno de sabor, es una opci�n perfecta para satisfacer los antojos de frutas."));
        listaDeOpciones.Add(new BebidaV2(Resources.Load<Sprite>("Cafe"), "Cafe", "Caf�, una BebidaV2 caliente y reconfortante preparada con granos de caf� tostado y molido, infusionados con agua caliente. Con su aroma tentador y su sabor robusto, el caf� es el compa�ero perfecto para empezar el d�a o disfrutar de un momento de tranquilidad."));
        listaDeOpciones.Add(new BebidaV2(Resources.Load<Sprite>("Mango"), "Mango", "Jugo de mango, una BebidaV2 tropical y ex�tica elaborada con jugo fresco de mango, mezclado con hielo para crear una BebidaV2 refrescante y llena de sabor. Ideal para disfrutar en d�as soleados."));

        //DishToBuyV2.Intance.BebidaV2s.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
        DishToBuyV2.Intance.bebidas.botonPlatoV2.RegisterCallback<ClickEvent>(ShowListPrincipio);
        limonada.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        mora.RegisterCallback<ClickEvent,int>(Showprincio, 1);
        fresa.RegisterCallback<ClickEvent,int>(Showprincio, 2);
        cafe.RegisterCallback<ClickEvent,int>(Showprincio, 3);
        mango.RegisterCallback<ClickEvent,int>(Showprincio, 4);

        AnadirLimonada.RegisterCallback<ClickEvent, int>(AnadirPlato, 0);
        AnadirMora.RegisterCallback<ClickEvent, int>(AnadirPlato, 1);
        AnadirFresa.RegisterCallback<ClickEvent, int>(AnadirPlato, 2);
        AnadirCafe.RegisterCallback<ClickEvent, int>(AnadirPlato, 3);
        AnadirMango.RegisterCallback<ClickEvent, int>(AnadirPlato, 4);
    }

    void Showprincio(ClickEvent evt)
    {
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        //DishToBuyV2.Intance.ApplySelectedStyle(DishToBuyV2.Intance.BebidaV2s, true);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
        
    }
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuyV2.Intance.plateScreen = DishToBuyV2.Intance.root.Q<ScrollView>("bebidasScreen");
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.plateScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }
    void ElegirThis(ClickEvent evt)
    {
        DishToBuyV2.Intance.plato.bebidas = listaDeOpciones[id].titulo;
        DishToBuyV2.Intance.bebidas.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuyV2.Intance.goBuy();
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
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.plateScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = _id;
        ChangeMainScreen(listaDeOpciones[id]);
    }
    void ChangeMainScreen(BebidaV2 choosenOption)
    {
        DishToBuyV2.Intance.foodImage.style.backgroundImage = new StyleBackground(choosenOption.imagen);
        DishToBuyV2.Intance.titleFood.text = choosenOption.titulo;
        DishToBuyV2.Intance.descriptionFood.text = choosenOption.descriptivo;
    }

    void baack(ClickEvent evt)
    {
        id = 0;
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.plateScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.back.UnregisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.UnregisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);;
    }

    void AnadirPlato(ClickEvent evt, int _id)
    {
        DishToBuyV2.Intance.plato.bebidas = listaDeOpciones[_id].titulo;
        DishToBuyV2.Intance.bebidas.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 110));

        // Regresar a la pantalla principal después de añadir el plato
        baack(evt);

        // Aquí puedes añadir cualquier otra lógica que necesites antes de ir a la pantalla de compra
        DishToBuyV2.Intance.goBuy();
    }
}

public class BebidaV2
{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public BebidaV2(Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }

}
