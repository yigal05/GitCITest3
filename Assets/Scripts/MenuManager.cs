using UnityEngine;
using System;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    // Propiedad est�tica que proporciona una referencia �nica al MenuManager
    public static MenuManager Instance { get; private set; }

    // Referencia a MenuDatabase
    private MenuDatabase menuDatabase;

    void Awake()
    {
        // Patr�n Singleton: asegura que solo haya una instancia de MenuManager en el juego
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asigna esta instancia como la instancia activa de MenuManager
        Instance = this;

        // Evita que el objeto se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);

        // Obt�n la referencia a MenuDatabase
        menuDatabase = GetComponent<MenuDatabase>();

        // Carga y muestra los datos del men�
        LoadAndPrintMenuData();
    }

    // M�todo para cargar y mostrar los datos del men�
    public void LoadAndPrintMenuData()
    {
        // Llama a LoadMenuData para cargar los datos del men� desde Firebase
        menuDatabase.LoadMenuData((categories, options) =>
        {
            // Callback: se llama cuando se completan las operaciones de carga
            if (categories != null && options != null)
            {
                // Imprime las categor�as
                foreach (var category in categories)
                {
                    Debug.Log($"Category: ID: {category.id}, Name: {category.name}");
                }

                // Imprime las opciones
                foreach (var option in options)
                {
                    Debug.Log($"Option: Name: {option.name}, Category ID: {option.categoryId}, Quantity: {option.quantity}");
                }
            }
            else
            {
                Debug.LogError("Failed to load menu data.");
            }
        });
    }

    public void GetMenuOptions(Action<List<MealOption>> callback)
    {
        // Obtiene las opciones del men�
        menuDatabase.GetMenuOptions(callback);
    }

    // M�todo para actualizar la cantidad de una opci�n de men� en la base de datos
    public void UpdateMenuOptionQuantity(string optionName, int newQuantity)
    {
        // Actualiza la cantidad en la base de datos
        menuDatabase.UpdateMenuOptionQuantity(optionName, newQuantity);
    }
}
