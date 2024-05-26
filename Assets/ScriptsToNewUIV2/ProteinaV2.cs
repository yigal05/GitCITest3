using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;
public class ProteinaV2 : MonoBehaviour
{
    private int id =0;
    public List<ProteinasV2> listaDeOpciones;
    public Button pollo,cerdo,pavo,pescado,huevo;
    public Button AnadirPollo, AnadirCerdo, AnadirPavo, AnadirPescado, AnadirHuevo;
    


    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuyV2
    private void Start()
    {
        pollo = DishToBuyV2.Intance.root.Q<Button>("infPollo");
        cerdo = DishToBuyV2.Intance.root.Q<Button>("infCerdo");
        pavo = DishToBuyV2.Intance.root.Q<Button>("infPavo");
        pescado = DishToBuyV2.Intance.root.Q<Button>("infPescado");
        huevo = DishToBuyV2.Intance.root.Q<Button>("infHuevo");
        AnadirPollo = DishToBuyV2.Intance.root.Q<Button>("anadirPollo");
        AnadirCerdo = DishToBuyV2.Intance.root.Q<Button>("anadirCerdo");
        AnadirPavo = DishToBuyV2.Intance.root.Q<Button>("anadirPavo");
        AnadirPescado = DishToBuyV2.Intance.root.Q<Button>("anadirPescado");
        AnadirHuevo = DishToBuyV2.Intance.root.Q<Button>("anadirHuevo");

        listaDeOpciones = new List<ProteinasV2>();
        listaDeOpciones.Add(new ProteinasV2(Resources.Load<Sprite>("Pollo"), "Pollo", "Pollo jugoso y tierno, sazonado con hierbas y especias, asado a la perfecci�n para un sabor delicioso y una experiencia de comida satisfactoria"));
        listaDeOpciones.Add( new ProteinasV2(Resources.Load<Sprite>("Cerdo"), "Cerdo", "Trozos de cerdo tierno y jugoso, cocinados lentamente en una salsa arom�tica que realza su sabor �nico y lo convierte en una opci�n deliciosa para cualquier comida."));
        listaDeOpciones.Add(new ProteinasV2(Resources.Load<Sprite>("Pavo"), "Pavo", "Pavo asado con un dorado crujiente por fuera y jugoso por dentro, sazonado con hierbas arom�ticas para un sabor delicioso y una opci�n ligera y saludable para cualquier ocasi�n."));
        listaDeOpciones.Add(new ProteinasV2(Resources.Load<Sprite>("Pescado"), "Pescado", "Pescado fresco y delicado, cocinado a la perfecci�n para resaltar su sabor natural y su textura tierna. Una opci�n ligera y nutritiva que deleitar� a los amantes del marisco"));
        listaDeOpciones.Add(new ProteinasV2(Resources.Load<Sprite>("Huevo"), "Huevo", "Huevo frito con una yema dorada y una clara crujiente en los bordes, una delicia simple y reconfortante que complementa cualquier desayuno o comida."));

        //DishToBuyV2.Intance.proteina.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
        DishToBuyV2.Intance.proteina.botonPlatoV2.RegisterCallback<ClickEvent>(ShowListPrincipio);
        pollo.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        cerdo.RegisterCallback<ClickEvent,int>(Showprincio, 1);
        pavo.RegisterCallback<ClickEvent,int>(Showprincio, 2);
        pescado.RegisterCallback<ClickEvent,int>(Showprincio, 3);
        huevo.RegisterCallback<ClickEvent,int>(Showprincio, 4);

        AnadirPollo.RegisterCallback<ClickEvent, int>(AnadirPlato, 0);
        AnadirCerdo.RegisterCallback<ClickEvent, int>(AnadirPlato, 1);
        AnadirPavo.RegisterCallback<ClickEvent, int>(AnadirPlato, 2);
        AnadirPescado.RegisterCallback<ClickEvent, int>(AnadirPlato, 3);
        AnadirHuevo.RegisterCallback<ClickEvent, int>(AnadirPlato, 4);

    }

    /**
    void Showprincio(ClickEvent evt)
    {
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }**/
    
    
    
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuyV2.Intance.plateScreen=DishToBuyV2.Intance.root.Q<ScrollView>("proteinaScreen");
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
        DishToBuyV2.Intance.plato.proteina = listaDeOpciones[id].titulo;
        DishToBuyV2.Intance.proteina.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuyV2.Intance.goBuy();
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
    void ChangeMainScreen( ProteinasV2 choosenOption)
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
        DishToBuyV2.Intance.chosee.UnregisterCallback<ClickEvent>(ElegirThis);
    }

    void AnadirPlato(ClickEvent evt, int _id)
    {
        DishToBuyV2.Intance.plato.proteina = listaDeOpciones[_id].titulo;
        DishToBuyV2.Intance.proteina.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 110));

        // Regresar a la pantalla principal después de añadir el plato
        baack(evt);

        // Aquí puedes añadir cualquier otra lógica que necesites antes de ir a la pantalla de compra
        DishToBuyV2.Intance.goBuy();
    }
}

public class ProteinasV2{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public ProteinasV2( Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }
    
}
