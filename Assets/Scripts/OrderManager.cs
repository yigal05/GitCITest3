using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using TMPro;

public class OrderManager : MonoBehaviour
{
    // Referencia al MenuManager para acceder a los datos del menú
    public MenuManager menuManager;

    // Texto para mostrar el estado de la orden
    public TMP_Text orderStatusText;

    // Método para manejar el pedido de un elemento
    public void OrderItem(string itemName)
    {
        // Llama a GetMenuOptions con una función de devolución de llamada
        menuManager.GetMenuOptions((options) =>
        {
            if (options != null)
            {
                // Busca el elemento en la lista de opciones
                MealOption item = options.Find(option => option.name.Equals(itemName));

                if (item != null)
                {
                    // Verifica si hay suficiente cantidad disponible
                    if (item.quantity > 0)
                    {
                        // Decrementa la cantidad en 1
                        int newQuantity = item.quantity - 1;

                        // Actualiza la cantidad en la base de datos y en la lista local de opciones
                        menuManager.UpdateMenuOptionQuantity(itemName, newQuantity);

                        // Actualizar el texto del estado del pedido
                        UpdateOrderStatus($"Pedido realizado: {itemName}");
                    }
                    else
                    {
                        orderStatusText.text = $"¡Lo siento, {itemName} no está disponible!";
                    }
                }
                else
                {
                    orderStatusText.text = $"¡{itemName} no encontrado en el menú!";
                }
            }
            else
            {
                Debug.LogError("Failed to load menu options.");
            }
        });
    }

    private void UpdateOrderStatus(string status)
    {
        if (orderStatusText != null)
        {
            orderStatusText.text = status;
        }
        else
        {
            Debug.LogError("El objeto de texto del estado del pedido no está asignado en el Inspector de Unity.");
        }
    }
}
