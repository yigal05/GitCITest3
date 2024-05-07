using System;

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