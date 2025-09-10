using UnityEngine;

[CreateAssetMenu(fileName = "RecipeSO", menuName = "Alchemy/Recipe")]
public class RecipeSO : ScriptableObject
{
    public ItemSO item1;
    public ItemSO item2;
    public ItemSO result;
}
