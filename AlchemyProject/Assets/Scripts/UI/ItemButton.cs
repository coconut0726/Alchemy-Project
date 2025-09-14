using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text buttonName;

    private string itemId;

    //use id to read directly from DB
    public void InitWithId(string id)
    {
        itemId = id;

        var row = RecipeIndex.instance.GetItem(id);
        icon.sprite = row.icon;
        buttonName.text = row.displayName;
    }
    public void OnPointerClick(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        Debug.Log($"Button pressed for item: {itemId}");
        // trigger whatever logic you want here (spawn, select, etc.)
    }
}
