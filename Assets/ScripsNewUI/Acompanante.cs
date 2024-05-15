using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;
public class Acompanante : MonoBehaviour
{
    private int id =0;
    public List<Acompantes> listaDeOpciones;

    
    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuy
    private void Start()
    {
        listaDeOpciones = new List<Acompantes>();
        listaDeOpciones.Add(new Acompantes(Resources.Load<Sprite>("frijoles"), "Arroz", "SABEN GOTY"));
        listaDeOpciones.Add( new Acompantes(Resources.Load<Sprite>("frijoles"), "Papa", "SABEN maso GOTY"));
        DishToBuy.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        
        DishToBuy.Intance.acompanante.RegisterCallback<ClickEvent>(Showprincio);
    }

    void Showprincio(ClickEvent evt)
    {
        DishToBuy.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuy.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuy.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuy.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }
    
    void ElegirThis(ClickEvent evt)
    {
        DishToBuy.Intance.plato.acompanate = listaDeOpciones[id].titulo;
        baack(new ClickEvent());
    }
    
    void goNextOption(ClickEvent evt)
    {
        id ++;
        if (id == listaDeOpciones.Count)
            id = 0;
        
        // if (id == -1)
        // id = listaDeOpciones.Count
        
        
        ChangeMainScreen(listaDeOpciones[id]);
    }
    void ChangeMainScreen( Acompantes choosenOption)
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
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        DishToBuy.Intance.next.UnregisterCallback<ClickEvent>(goNextOption);
        DishToBuy.Intance.back.UnregisterCallback<ClickEvent>(baack);
    }
}

public class Acompantes{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public Acompantes( Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }
    
}
