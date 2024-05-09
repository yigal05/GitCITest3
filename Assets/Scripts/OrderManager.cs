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

    private List<string> selectedItems = new List<string>();

    // Método para manejar el pedido de un elemento
    public void OrderItem(string itemName)
    {
        if (!selectedItems.Contains(itemName))
        {
            selectedItems.Add(itemName);
            UpdateOrderStatus($"Plato añadido: {itemName}");
        }
        else
        {
            selectedItems.Remove(itemName);
            UpdateOrderStatus($"Plato eliminado: {itemName}");
        }
    }

    public void AddToOrder(string itemName)
    {
        if (!selectedItems.Contains(itemName))
        {
            selectedItems.Add(itemName);
        }
    }

    public void RemoveFromOrder(string itemName)
    {
        if (selectedItems.Contains(itemName))
        {
            selectedItems.Remove(itemName);
        }
    }

    public void ConfirmOrder()
    {
        foreach (string itemName in selectedItems)
        {
            menuManager.GetMenuOptions((options) =>
            {
                MealOption item = options.Find(option => option.name.Equals(itemName));
                if (item != null && item.quantity > 0)
                {
                    int newQuantity = item.quantity - 1;
                    menuManager.UpdateMenuOptionQuantity(itemName, newQuantity);
                }
            });
        }

        selectedItems.Clear();
        UpdateOrderStatus("Pedido confirmado.");
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