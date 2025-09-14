using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ItemButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text buttonName;

    private string itemId;
    private ItemRow itemRow;

    private void Start()
    {
        itemRow = RecipeIndex.instance.GetItem(itemId);
    }

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

        //spawn item
        Vector2 defaultPos = Vector2.zero;
        ItemSpawner spawner = FindAnyObjectByType<ItemSpawner>();
        if (spawner != null) {
            spawner.InitiateItem(defaultPos, itemRow);
        }



    }
}
