using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;
public class AcompananteV2 : MonoBehaviour
{
    private int id =0;
    public List<AcompantesV2> listaDeOpciones;
    public Button arroz, pure;
    public Button AnadirArroz, AnadirPure;

    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuyV2
    private void Start()
    {
        arroz = DishToBuyV2.Intance.root.Q<Button>("infArroz");
        pure = DishToBuyV2.Intance.root.Q<Button>("infPure");
        AnadirArroz = DishToBuyV2.Intance.root.Q<Button>("anadirArroz");
        AnadirPure = DishToBuyV2.Intance.root.Q<Button>("anadirPure");

        listaDeOpciones = new List<AcompantesV2>();
        listaDeOpciones.Add(new AcompantesV2(Resources.Load<Sprite>("Arroz"), "Arroz", "Arroz blanco cocido perfectamente, ligero y esponjoso, listo para acompa�ar cualquier comida con su sencillez y versatilidad"));
        listaDeOpciones.Add( new AcompantesV2(Resources.Load<Sprite>("Pure"), "Pure", "Puré de papas cremoso y suave, preparado con mantequilla y leche, una deliciosa guarnici�n reconfortante que complementa cualquier plato principal"));
        
        //DishToBuyV2.Intance.acompanante.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
        DishToBuyV2.Intance.acompanante.botonPlatoV2.RegisterCallback<ClickEvent>(ShowListPrincipio);
        arroz.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        pure.RegisterCallback<ClickEvent,int>(Showprincio, 1);

        AnadirArroz.RegisterCallback<ClickEvent, int>(AnadirPlato, 0);
        AnadirPure.RegisterCallback<ClickEvent, int>(AnadirPlato, 1);
    }

    /**
    void Showprincio(ClickEvent evt)
    {
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }**/
    
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuyV2.Intance.plateScreen = DishToBuyV2.Intance.root.Q<ScrollView>("acompananteScreen");
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.plateScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
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
        DishToBuyV2.Intance.plato.acompanante = listaDeOpciones[id].titulo;
        DishToBuyV2.Intance.acompanante.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuyV2.Intance.goBuy();
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
    
    void ChangeMainScreen( AcompantesV2 choosenOption)
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
        DishToBuyV2.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        DishToBuyV2.Intance.next.UnregisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.back.UnregisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        
    }

    void AnadirPlato(ClickEvent evt, int _id)
    {
        DishToBuyV2.Intance.plato.acompanante = listaDeOpciones[_id].titulo;
        DishToBuyV2.Intance.acompanante.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 110));

        // Regresar a la pantalla principal después de añadir el plato
        baack(evt);

        // Aquí puedes añadir cualquier otra lógica que necesites antes de ir a la pantalla de compra
        DishToBuyV2.Intance.goBuy();
    }
}

public class AcompantesV2{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public AcompantesV2( Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }
    
}

