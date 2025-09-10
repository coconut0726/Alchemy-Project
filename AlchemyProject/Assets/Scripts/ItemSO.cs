using UnityEngine;

[CreateAssetMenu(fileName = "ItemSO", menuName = "Alchemy/Item")]
public class ItemSO : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public string[] tags;
}
