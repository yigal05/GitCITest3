using UnityEngine;
using System.Collections.Generic;
using Firebase.Database;
using Firebase;
using System;

public class MenuDatabase
{
    private static MenuData cachedMenuData;

    // Loads and caches the menu data from JSON
    public static MenuData LoadMenuData()
    {
        if (cachedMenuData != null)
            return cachedMenuData;

        TextAsset jsonTextFile = Resources.Load<TextAsset>("MenuDatabase");
        if (jsonTextFile != null)
        {
            cachedMenuData = JsonUtility.FromJson<MenuData>(jsonTextFile.text);
            Debug.Log("Menu data successfully loaded.");

            // Debug logs to verify data
            DebugCategoriesAndOptions(cachedMenuData);
            return cachedMenuData;
        }
        else
        {
            Debug.LogError("Failed to load menu data.");
            return null;
        }
    }

    // Debug method to print loaded categories and options
    private static void DebugCategoriesAndOptions(MenuData menuData)
    {
        foreach (var category in menuData.categories)
        {
            Debug.Log($"Category ID: {category.id}, Name: {category.name}");
        }

        foreach (var option in menuData.options)
        {
            Debug.Log($"Option Name: {option.name}, Category ID: {option.categoryId}, Quantity: {option.quantity}");
        }
    } 

}
