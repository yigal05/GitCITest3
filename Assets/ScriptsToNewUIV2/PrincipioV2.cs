using System;
using System.Collections;
using System.Collections.Generic;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;
public class PrincipioV2 : MonoBehaviour
{
    private int id =0;
    public List<PrincipiosV2> listaDeOpciones;

    public Button frijoles,lentejas,pastas;

    public Button AnadirFrijoles;
    public Button AnadirLentejas;
    public Button AnadirPasta;



    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuyV2
    private void Start()
    {
        frijoles = DishToBuyV2.Intance.root.Q<Button>("infFrijoles");
        lentejas = DishToBuyV2.Intance.root.Q<Button>("InfLentejas");
        pastas = DishToBuyV2.Intance.root.Q<Button>("infPasta");
        AnadirFrijoles = DishToBuyV2.Intance.root.Q<Button>("anadirFrijoles");
        AnadirLentejas = DishToBuyV2.Intance.root.Q<Button>("anadirLentejas");
        AnadirPasta = DishToBuyV2.Intance.root.Q<Button>("anadirPasta");


        listaDeOpciones = new List<PrincipiosV2>();
        listaDeOpciones.Add(new PrincipiosV2(Resources.Load<Sprite>("Frijoles"), "Frijoles", "Deliciosos frijoles cocidos lentamente en una sabrosa mezcla de especias."));
        listaDeOpciones.Add(new PrincipiosV2(Resources.Load<Sprite>("Lentejas"), "Lentejas", "Lentejas cocinadas a fuego lento en un caldo arom�tico con cebolla, ajo y zanahorias, sazonadas con hierbas frescas como el tomillo y el laurel"));
        listaDeOpciones.Add(new PrincipiosV2(Resources.Load<Sprite>("Pasta"), "Pasta", "Pasta al dente con una salsa de tomate casera y hierbas frescas. Simple, sabroso y reconfortante."));
        
        //DishToBuyV2.Intance.principio.botonPlato.RegisterCallback<ClickEvent>(Showprincio); esta linea manda directamente a la informacion (antigua)
        DishToBuyV2.Intance.principio.botonPlatoV2.RegisterCallback<ClickEvent>(ShowListPrincipio);
        frijoles.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        lentejas.RegisterCallback<ClickEvent,int>(Showprincio, 1);
        pastas.RegisterCallback<ClickEvent,int>(Showprincio, 2);

        AnadirFrijoles.RegisterCallback<ClickEvent, int>(AnadirPlato, 0);
        AnadirLentejas.RegisterCallback<ClickEvent, int>(AnadirPlato, 1);
        AnadirPasta.RegisterCallback<ClickEvent, int>(AnadirPlato, 2);
    }
    
    /**
    void Showprincio(ClickEvent evt)
    {
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.plateScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
        ChangeMainScreen(listaDeOpciones[id]);
    }**/
    
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuyV2.Intance.plateScreen=DishToBuyV2.Intance.root.Q<ScrollView>("PlateScreen");
        DishToBuyV2.Intance.mainScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.plateScreen.style.display = DisplayStyle.Flex;
        DishToBuyV2.Intance.foodScreen.style.display = DisplayStyle.None;
        DishToBuyV2.Intance.back.RegisterCallback<ClickEvent>(baack);
        DishToBuyV2.Intance.next.RegisterCallback<ClickEvent>(goNextOption);
        DishToBuyV2.Intance.chosee.RegisterCallback<ClickEvent>(ElegirThis);
        id = 0;
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
    
    void goNextOption(ClickEvent evt)
    {
        id ++;
        if (id == listaDeOpciones.Count)
            id = 0;
        
        // if (id == -1)
            // id = listaDeOpciones.Count
        
        
            ChangeMainScreen(listaDeOpciones[id]);
    }
    void ChangeMainScreen( PrincipiosV2 choosenOption)
    {
        DishToBuyV2.Intance.foodImage.style.backgroundImage = new StyleBackground(choosenOption.imagen);
        DishToBuyV2.Intance.titleFood.text = choosenOption.titulo;
        DishToBuyV2.Intance.descriptionFood.text = choosenOption.descriptivo;
    }

    void ElegirThis(ClickEvent evt)
    {
        DishToBuyV2.Intance.plato.principio = listaDeOpciones[id].titulo;
        DishToBuyV2.Intance.principio.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
        baack(new ClickEvent());
        DishToBuyV2.Intance.goBuy();
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
    }

    void AnadirPlato(ClickEvent evt, int _id)
    {
        DishToBuyV2.Intance.plato.principio = listaDeOpciones[_id].titulo;
        DishToBuyV2.Intance.principio.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 110));

        // Regresar a la pantalla principal después de añadir el plato
        baack(evt);

        // Aquí puedes añadir cualquier otra lógica que necesites antes de ir a la pantalla de compra
        DishToBuyV2.Intance.goBuy();
    }

}

public class PrincipiosV2{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public PrincipiosV2( Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }
    
}
