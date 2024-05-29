using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Database;
using Firebase.Extensions;
using ScripsNewUI;
using UnityEngine;
using UnityEngine.UIElements;

public class SopaV2 : MonoBehaviour
{
    private int id = 0;
    public List<SopasV2> listaDeOpciones;


    public Button ajiaco, sancocho, sopaDePollo;
    public Button AnadirAjiaco, AnadirSancocho, AnadirSopadepollo;

    public Label ajiacoQuantityLabel;
    public Label sancochoQuantityLabel;
    public Label sopapolloQuantityLabel;

    public Label disponibilidadLabelAjiaco, disponibilidadLabelSancocho, disponibilidadLabelSopapollo;
    private DatabaseReference databaseReference;



    //es importante hacerse en el start ya que debemos esperar el awake de DishToBuyV2
    private void Start()
    {
        ajiaco = DishToBuyV2.Intance.root.Q<Button>("infAjiaco");
        sancocho = DishToBuyV2.Intance.root.Q<Button>("InfSancocho");
        sopaDePollo = DishToBuyV2.Intance.root.Q<Button>("infSopaPollo");
        AnadirAjiaco = DishToBuyV2.Intance.root.Q<Button>("anadirAjiaco");
        AnadirSancocho = DishToBuyV2.Intance.root.Q<Button>("anadirSancocho");
        AnadirSopadepollo = DishToBuyV2.Intance.root.Q<Button>("anadirSopadepollo");

        ajiacoQuantityLabel = DishToBuyV2.Intance.root.Q<Label>("AjiacoQuantityLabel");
        sancochoQuantityLabel = DishToBuyV2.Intance.root.Q<Label>("SancochoQuantityLabel");
        sopapolloQuantityLabel = DishToBuyV2.Intance.root.Q<Label>("SopapolloQuantityLabel");

        disponibilidadLabelAjiaco = DishToBuyV2.Intance.root.Q<Label>("DisponibilidadLabelAjiaco");
        disponibilidadLabelSancocho = DishToBuyV2.Intance.root.Q<Label>("DisponibilidadLabelSancocho");
        disponibilidadLabelSopapollo = DishToBuyV2.Intance.root.Q<Label>("DisponibilidadLabelSopapollo");

        listaDeOpciones = new List<SopasV2>();
        listaDeOpciones.Add(new SopasV2(Resources.Load<Sprite>("Ajiaco"), "Ajiaco", "Ajiaco, una sabrosa sopa colombiana, hecha con pollo, papas, maíz, yuca y guascas, sazonada con cilantro y servida con alcaparras y crema. Una delicia reconfortante y llena de sabor que representa la riqueza culinaria de Colombia."));
        listaDeOpciones.Add(new SopasV2(Resources.Load<Sprite>("Sancocho"), "Sancocho", "Sancocho, un guiso tradicional latinoamericano, preparado con una variedad de carnes, plátanos, yuca, ñame, maíz y otras verduras, cocidas lentamente en un caldo aromático sazonado con hierbas y especias. Un plato reconfortante y abundante que es una verdadera fiesta para el paladar"));
        listaDeOpciones.Add(new SopasV2(Resources.Load<Sprite>("Sopa de Pollo"), "Sopa de Pollo", "Sopa de pollo, un plato reconfortante hecho con trozos tiernos de pollo, verduras frescas como zanahorias, apio y cebolla, cocidas en un caldo sabroso y sazonado con hierbas aromáticas. Una opción nutritiva y satisfactoria para cualquier ocasión."));

        //DishToBuyV2.Intance.sopa.botonPlato.RegisterCallback<ClickEvent>(Showprincio);
        DishToBuyV2.Intance.sopa.botonPlatoV2.RegisterCallback<ClickEvent>(ShowListPrincipio);
        ajiaco.RegisterCallback<ClickEvent,int>(Showprincio, 0);
        sancocho.RegisterCallback<ClickEvent,int>(Showprincio, 1);
        sopaDePollo.RegisterCallback<ClickEvent,int>(Showprincio, 2);

        AnadirAjiaco.RegisterCallback<ClickEvent, int>(AnadirPlato, 0);
        AnadirSancocho.RegisterCallback<ClickEvent, int>(AnadirPlato, 1);
        AnadirSopadepollo.RegisterCallback<ClickEvent, int>(AnadirPlato, 2);

        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        databaseReference.Child("options").ValueChanged += HandleValueChanged;

        LoadDishQuantities();
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        // Manejar los cambios en los datos de la base de datos
        if (args.Snapshot.Exists)
        {
            foreach (DataSnapshot optionSnapshot in args.Snapshot.Children)
            {
                IDictionary option = (IDictionary)optionSnapshot.Value;
                int categoryId = Convert.ToInt32(option["categoryId"]);
                string name = option["name"].ToString();
                int quantity = Convert.ToInt32(option["quantity"]);

                if (categoryId == 4) // Principio
                {
                    // Actualizar los labels según el nombre de la opción
                    if (name == "Ajiaco")
                    {
                        if (quantity <= 0)
                        {
                            ajiacoQuantityLabel.text = "";
                            disponibilidadLabelAjiaco.text = "No disponible";
                        }
                        else
                        {
                            ajiacoQuantityLabel.text = quantity.ToString();
                            disponibilidadLabelAjiaco.text = "Disponible:";
                        }

                    }
                    else if (name == "Sancocho")
                    {
                        if (quantity <= 0)
                        {
                            sancochoQuantityLabel.text = "";
                            disponibilidadLabelSancocho.text = "No disponible";
                        }
                        else
                        {
                            sancochoQuantityLabel.text = quantity.ToString();
                            disponibilidadLabelSancocho.text = "Disponible:";
                        }
                    }
                    else if (name == "Sopa de Pollo")
                    {
                        if (quantity <= 0)
                        {
                            sopapolloQuantityLabel.text = "";
                            disponibilidadLabelSopapollo.text = "No disponible";
                        }
                        else
                        {
                            sopapolloQuantityLabel.text = quantity.ToString();
                            disponibilidadLabelSopapollo.text = "Disponible:";
                        }
                    }
                }
            }
        }
    }

    private void LoadDishQuantities()
    {
        databaseReference.Child("options").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result; // Obtener el snapshot de la base de datos

                foreach (DataSnapshot optionSnapshot in snapshot.Children)
                {
                    IDictionary option = (IDictionary)optionSnapshot.Value;
                    int categoryId = Convert.ToInt32(option["categoryId"]);
                    string name = option["name"].ToString();
                    int quantity = Convert.ToInt32(option["quantity"]);

                    if (categoryId == 1) // Principio
                    {
                        if (name == "Ajiaco")
                        {
                            if (quantity <= 0)
                            {
                                ajiacoQuantityLabel.text = "";
                                disponibilidadLabelAjiaco.text = "No disponible";
                            }
                            else
                            {
                                ajiacoQuantityLabel.text = quantity.ToString();
                                disponibilidadLabelAjiaco.text = "Disponible:";
                            }

                        }
                        else if (name == "Sancocho")
                        {
                            if (quantity <= 0)
                            {
                                sancochoQuantityLabel.text = "";
                                disponibilidadLabelSancocho.text = "No disponible";
                            }
                            else
                            {
                                sancochoQuantityLabel.text = quantity.ToString();
                                disponibilidadLabelSancocho.text = "Disponible:";
                            }
                        }
                        else if (name == "Sopa de Pollo")
                        {
                            if (quantity <= 0)
                            {
                                sopapolloQuantityLabel.text = "";
                                disponibilidadLabelSopapollo.text = "No disponible";
                            }
                            else
                            {
                                sopapolloQuantityLabel.text = quantity.ToString();
                                disponibilidadLabelSopapollo.text = "Disponible:";
                            }
                        }
                    }
                }
            }
        });
    }


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
    }
    
    void ShowListPrincipio(ClickEvent evt)
    {
        DishToBuyV2.Intance.plateScreen = DishToBuyV2.Intance.root.Q<ScrollView>("sopasScreen");
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
        DishToBuyV2.Intance.plato.sopa = listaDeOpciones[id].titulo;
        DishToBuyV2.Intance.sopa.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0,0,0,110));
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

        print($"la opcion elegida actualmente es {listaDeOpciones[id].titulo}");
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
    void ChangeMainScreen(SopasV2 choosenOption)
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
        DishToBuyV2.Intance.plato.sopa = listaDeOpciones[_id].titulo;
        DishToBuyV2.Intance.sopa.bordenbotonV2.style.backgroundColor = new StyleColor(new Color32(0, 0, 0, 110));

        // Regresar a la pantalla principal después de añadir el plato
        baack(evt);

        // Aquí puedes añadir cualquier otra lógica que necesites antes de ir a la pantalla de compra
        DishToBuyV2.Intance.goBuy();
    }
}

public class SopasV2
{
    public Sprite imagen;
    public string titulo;
    public string descriptivo;

    public SopasV2(Sprite _imagen, string _titulo, string _descriptivo)
    {
        imagen = _imagen;
        titulo = _titulo;
        descriptivo = _descriptivo;

    }

}
