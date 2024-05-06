using UnityEngine;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour
{
    // Propiedad estática que proporciona una referencia única al MenuManager
    public static MenuManager Instance { get; private set; }
    private List<MealOption> options = new List<MealOption>();

    // Referencia a la base de datos de Firebase
    private DatabaseReference databaseReference;

    void Awake()
    {
        // Patrón Singleton: asegura que solo haya una instancia de MenuManager en el juego
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asigna esta instancia como la instancia activa de MenuManager
        Instance = this;

        // Evita que el objeto se destruya al cargar una nueva escena
        DontDestroyOnLoad(gameObject);

        // Establece la referencia a la base de datos de Firebase
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

        // Carga y muestra los datos del menú
        LoadAndPrintMenuData();
    }

    // Método para cargar y mostrar los datos del menú
    public void LoadAndPrintMenuData()
    {
        // Llama a LoadMenuData para cargar los datos del menú desde Firebase
        LoadMenuData((categories, options) =>
        {
            // Callback: se llama cuando se completan las operaciones de carga
            if (categories != null && options != null)
            {
                // Imprime las categorías
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

    // Método para cargar los datos del menú desde Firebase
    private void LoadMenuData(Action<List<MealCategory>, List<MealOption>> callback)
    {
        // Obtiene los datos de Firebase de forma asíncrona
        databaseReference.GetValueAsync().ContinueWith(task =>
        {
            // Procesa los datos cargados
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<MealCategory> categories = new List<MealCategory>();

                // Parsea las categorías (como en tu código original)

                // Parsea las opciones
                foreach (DataSnapshot optionSnapshot in snapshot.Child("options").Children)
                {
                    string optionName = optionSnapshot.Child("name").Value.ToString();
                    int categoryId = Convert.ToInt32(optionSnapshot.Child("categoryId").Value);
                    int quantity = Convert.ToInt32(optionSnapshot.Child("quantity").Value);
                    MealOption option = new MealOption(optionName, categoryId, quantity);
                    options.Add(option); // Agrega la opción a la lista de opciones
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
        // Establece la referencia a la base de datos de Firebase
        if (databaseReference == null)
        {
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        }

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

    // Método para actualizar la cantidad de una opción de menú en la base de datos
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


    // Clase para representar una categoría de menú
    [Serializable]
    public class MealCategory
    {
        public int id;
        public string name;

        // Constructor de MealCategory
        public MealCategory(int id, string name)
        {
            this.id = id;
            this.name = name;
        }
    }

    // Clase para representar una opción de menú
    [Serializable]
    public class MealOption
    {
        public string name;
        public int categoryId;
        public int quantity;

        // Constructor de MealOption
        public MealOption(string name, int categoryId, int quantity)
        {
            this.name = name;
            this.categoryId = categoryId;
            this.quantity = quantity;
        }
    }
}
