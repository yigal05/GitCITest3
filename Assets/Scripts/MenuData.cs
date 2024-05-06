using System;
using System.Collections.Generic;

[Serializable]
public class MenuData
{
    public List<MealCategory> categories;
    public List<MealOption> options;
}

[Serializable]
public class MealCategory
{
    public int id;
    public string name;
}

[Serializable]
public class MealOption
{
    public string name;
    public int categoryId;
    public int quantity;
}