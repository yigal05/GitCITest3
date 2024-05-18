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
        listaDeOpciones.Add(new Acompantes(Resources.Load<Sprite>("arroz"), "Arroz", "Arroz blanco cocido perfectamente, ligero y esponjoso, listo para acompa�ar cualquier comida con su sencillez y versatilidad"));
        listaDeOpciones.Add( new Acompantes(Resources.Load<Sprite>("pure"), "Pure", "Pur� de papas cremoso y suave, preparado con mantequilla y leche, una deliciosa guarnici�n reconfortante que complementa cualquier plato principal"));
        
        DishToBuy.Intance.acompanante.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
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
    
    void ElegirThis(ClickEvent evt)
    {
        DishToBuy.Intance.plato.acompanante = listaDeOpciones[id].titulo;
        DishToBuy.Intance.acompanante.bordenBoton.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuy.Intance.goBuy();
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
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        
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
