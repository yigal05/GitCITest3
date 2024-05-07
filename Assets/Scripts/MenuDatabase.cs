using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;

public class MenuDatabase : MonoBehaviour
{
    // Referencia a la base de datos de Firebase
    private DatabaseReference databaseReference;

    void Awake()
    {
        // Establece la referencia a la base de datos de Firebase
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void LoadMenuData(Action<List<MealCategory>, List<MealOption>> callback)
    {
        // Obtiene los datos de Firebase de forma asíncrona
        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<MealCategory> categories = new List<MealCategory>();
                List<MealOption> options = new List<MealOption>();

                // Parsea las categorías (como en tu código original)

                // Parsea las opciones
                foreach (DataSnapshot optionSnapshot in snapshot.Child("options").Children)
                {
                    string optionName = optionSnapshot.Child("name").Value.ToString();
                    int categoryId = Convert.ToInt32(optionSnapshot.Child("categoryId").Value);
                    int quantity = Convert.ToInt32(optionSnapshot.Child("quantity").Value);
                    MealOption option = new MealOption(optionName, categoryId, quantity);
                    options.Add(option);
                }

                // Invoca el callback con los datos del menú cargados
                callback(categories, options);
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to load menu data: " + task.Exception);
                callback(null, null);
            }
        });
    }

    public void GetMenuOptions(Action<List<MealOption>> callback)
    {
        // Obtiene los datos de Firebase de forma asíncrona
        databaseReference.Child("options").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<MealOption> options = new List<MealOption>();

                // Parsea las opciones
                foreach (DataSnapshot optionSnapshot in snapshot.Children)
                {
                    string optionName = optionSnapshot.Child("name").Value.ToString();
                    int categoryId = Convert.ToInt32(optionSnapshot.Child("categoryId").Value);
                    int quantity = Convert.ToInt32(optionSnapshot.Child("quantity").Value);
                    MealOption option = new MealOption(optionName, categoryId, quantity);
                    options.Add(option);
                }

                // Invoca el callback con los datos del menú cargados
                callback(options);
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to load menu options: " + task.Exception);
                callback(null);
            }
        });
    }

    public void UpdateMenuOptionQuantity(string optionName, int newQuantity)
    {
        DatabaseReference optionsRef = databaseReference.Child("options");

        // Busca el nodo correspondiente al plato en Firebase
        optionsRef.OrderByChild("name").EqualTo(optionName).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;

                // Itera sobre los nodos encontrados (debería ser solo uno)
                foreach (DataSnapshot optionSnapshot in snapshot.Children)
                {
                    // Actualiza la cantidad en el nodo correspondiente
                    optionSnapshot.Child("quantity").Reference.SetValueAsync(newQuantity);
                }
            }
            else if (task.IsFaulted)
            {
                Debug.LogError("Failed to update menu option quantity: " + task.Exception);
            }
        });
    }
}
