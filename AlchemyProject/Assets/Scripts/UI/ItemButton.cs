using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;

public class ItemButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text buttonName;
    [SerializeField] private RectTransform parent;

    private string itemId;
    private ItemRow itemRow;

    private void Awake()
    {
        if (parent == null)
    {
        parent = FindFirstObjectByType<Canvas>().GetComponent<RectTransform>();
    }
    } //if you want the item spawn at where the mouse is
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
    public void OnPointerDown(PointerEventData e)
    {
        if (e.button != PointerEventData.InputButton.Left) return;

        Debug.Log($"Button pressed for item: {itemId}");
        // trigger whatever logic you want here (spawn, select, etc.)

        Vector2 defaultPos = new Vector2(Random.Range(-200f, 200f), Random.Range(-200f, 200f));
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            parent,
            e.position,
            e.pressEventCamera,
            out localPoint))
        {
            ItemSpawner spawner = FindAnyObjectByType<ItemSpawner>();
            if (spawner != null)
            {
                spawner.InitiateItem(defaultPos, itemRow); //if you want the item spawn at where the mouse is, localPoint
            }
        }
    }
}
