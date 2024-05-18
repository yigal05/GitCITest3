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
        listaDeOpciones.Add(new Proteinas(Resources.Load<Sprite>("Pollo"), "Pollo", "Pollo jugoso y tierno, sazonado con hierbas y especias, asado a la perfecci�n para un sabor delicioso y una experiencia de comida satisfactoria"));
        listaDeOpciones.Add( new Proteinas(Resources.Load<Sprite>("Cerdo"), "Cerdo", "Trozos de cerdo tierno y jugoso, cocinados lentamente en una salsa arom�tica que realza su sabor �nico y lo convierte en una opci�n deliciosa para cualquier comida."));
        listaDeOpciones.Add(new Proteinas(Resources.Load<Sprite>("Pavo"), "Pavo", "Pavo asado con un dorado crujiente por fuera y jugoso por dentro, sazonado con hierbas arom�ticas para un sabor delicioso y una opci�n ligera y saludable para cualquier ocasi�n."));
        listaDeOpciones.Add(new Proteinas(Resources.Load<Sprite>("Pescado"), "Pescado", "Pescado fresco y delicado, cocinado a la perfecci�n para resaltar su sabor natural y su textura tierna. Una opci�n ligera y nutritiva que deleitar� a los amantes del marisco"));
        listaDeOpciones.Add(new Proteinas(Resources.Load<Sprite>("Huevo"), "Huevo", "Huevo frito con una yema dorada y una clara crujiente en los bordes, una delicia simple y reconfortante que complementa cualquier desayuno o comida."));

        DishToBuy.Intance.proteina.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
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
        DishToBuy.Intance.proteina.bordenBoton.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuy.Intance.goBuy();
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
        DishToBuy.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
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
