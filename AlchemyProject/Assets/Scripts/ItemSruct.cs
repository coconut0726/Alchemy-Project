using UnityEngine;
using UnityEngine.UI;

public class ItemSruct : MonoBehaviour
{
    [SerializeField] private GameObject DataBase;
    [SerializeField] private Image iconUI;

    [SerializeField] private string defaultItemID;
    [SerializeField] private Sprite defaultIcon;
    
    public string itemID;
    public string itemName;
    public Sprite icon;

    private void Awake()
    {
        iconUI = GetComponent<Image>();
    }
    private void Start()
    {

        //Debug.Log("Item Initializing");

        if (string.IsNullOrEmpty(defaultItemID))
        {
            Debug.Log("Default item empty");
        }
        else
        { 
            Debug.Log(defaultItemID);
        }
        if (!string.IsNullOrEmpty(defaultItemID)) {
            if (RecipeIndex.instance == null) {
                Debug.Log("RecipeIndex instance null");
            }
            var item = RecipeIndex.instance.GetItem(defaultItemID);
 
            SetItem(item);
        }
    }
    public void SetItem(ItemRow item)
    {
        //Debug.Log(item.icon);

        itemID = item.id;
        itemName = item.displayName;
        icon = item.icon;

        //Debug.Log(icon);

        UpdateUI();

    }

    private void UpdateUI()
    {
        iconUI.sprite = icon;
    }

}
