using UnityEngine;

[System.Serializable] public class ItemRow
{
    public string id;
    public string displayName;
    public Sprite icon;
}

[System.Serializable] public class RecipeRow
{
    public string a, b, r; //unordered, a+b = r
}

[CreateAssetMenu(menuName ="Alchemy/DB")]
public class AlchemyDB : ScriptableObject
{
    public ItemRow[] items;
    public RecipeRow[] recipes;
}