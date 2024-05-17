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
        listaDeOpciones.Add(new Principios(Resources.Load<Sprite>("frijoles2"), "Frijoles", "Deliciosos frijoles cocidos lentamente en una sabrosa mezcla de especias."));
        listaDeOpciones.Add(new Principios(Resources.Load<Sprite>("lentejas"), "Lentejas", "Lentejas cocinadas a fuego lento en un caldo arom�tico con cebolla, ajo y zanahorias, sazonadas con hierbas frescas como el tomillo y el laurel"));
        listaDeOpciones.Add(new Principios(Resources.Load<Sprite>("pasta"), "Pasta", "Pasta al dente con una salsa de tomate casera y hierbas frescas. Simple, sabroso y reconfortante."));

        DishToBuy.Intance.principio.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
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
        DishToBuy.Intance.principio.bordenBoton.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuy.Intance.goBuy();
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
