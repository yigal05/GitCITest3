using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using System.Collections.Generic;
using TMPro;
public class OrderManager : MonoBehaviour
{
    // Referencia al MenuManager para acceder a los datos del men�
    public MenuManager menuManager;

    // Texto para mostrar el estado de la orden
    public TMP_Text orderStatusText;

    private List<string> selectedItems = new List<string>();

    // M�todo para manejar el pedido de un elemento
    public void OrderItem(string itemName)
    {
        if (!selectedItems.Contains(itemName))
        {
            selectedItems.Add(itemName);
        }
        else
        {
            selectedItems.Remove(itemName);
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
    }


}
