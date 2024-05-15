using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;
public class Proteina : MonoBehaviour
{
    private int id =0;
    public List<Proteinas> listaDeOpciones;
    

    
    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuy
    private void Start()
    {
        listaDeOpciones = new List<Proteinas>();
        listaDeOpciones.Add(new Proteinas(Resources.Load<Sprite>("frijoles"), "POLLO", "SABEN GOTY"));
        listaDeOpciones.Add( new Proteinas(Resources.Load<Sprite>("frijoles"), "PESCADO", "SABEN maso GOTY"));
        listaDeOpciones.Add(new Proteinas(Resources.Load<Sprite>("frijoles"), "PAVO", "TAMBIEN GOTY"));
        
        DishToBuy.Intance.proteina.RegisterCallback<ClickEvent>(Showprincio);
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
    
    void ElegirThis(ClickEvent evt)
    {
        DishToBuy.Intance.plato.proteina = listaDeOpciones[id].titulo;
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
    void ChangeMainScreen( Proteinas choosenOption)
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
    }
}

public class Proteinas{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public Proteinas( Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }
    
}
