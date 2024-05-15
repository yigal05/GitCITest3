using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;
public class Principio : MonoBehaviour
{
    private int id =0;
    public List<Principios> listaDeOpciones;
    

    
    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuy
    private void Start()
    {
        listaDeOpciones = new List<Principios>();
        listaDeOpciones.Add(new Principios(Resources.Load<Sprite>("frijoles"), "FRIJOLES", "SABEN GOTY"));
        listaDeOpciones.Add( new Principios(Resources.Load<Sprite>("frijoles"), "LENTEJAS", "SABEN maso GOTY"));
        listaDeOpciones.Add(new Principios(Resources.Load<Sprite>("frijoles"), "PASTAS", "TAMBIEN GOTY"));
        
        DishToBuy.Intance.principio.RegisterCallback<ClickEvent>(Showprincio);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            DishToBuy.Intance.imprimirTin();
        }
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
    
    void goNextOption(ClickEvent evt)
    {
        id ++;
        if (id == listaDeOpciones.Count)
            id = 0;
        
        // if (id == -1)
            // id = listaDeOpciones.Count
        
        
            ChangeMainScreen(listaDeOpciones[id]);
    }
    void ChangeMainScreen( Principios choosenOption)
    {
        DishToBuy.Intance.foodImage.style.backgroundImage = new StyleBackground(choosenOption.imagen);
        DishToBuy.Intance.titleFood.text = choosenOption.titulo;
        DishToBuy.Intance.descriptionFood.text = choosenOption.descriptivo;
    }

    void ElegirThis(ClickEvent evt)
    {
        DishToBuy.Intance.plato.principio = listaDeOpciones[id].titulo;
        baack(new ClickEvent());
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

public class Principios{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public Principios( Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }
    
}
