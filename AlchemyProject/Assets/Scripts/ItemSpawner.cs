using Unity.VisualScripting;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private RectTransform parent;

    public GameObject InitiateItem(Vector2 anchoredPos, ItemRow item) {
        if (itemPrefab == null) {
            Debug.LogError("itemPrefab == null.");
            return null;
        }

        GameObject obj = Instantiate(itemPrefab, parent);
        RectTransform rect = obj.GetComponent<RectTransform>();
        rect.anchoredPosition = anchoredPos;

        ItemSruct itemSruct = obj.GetComponent<ItemSruct>();
        if (itemSruct != null)
        {
            itemSruct.SetItem(item);
        }
        else {
            Debug.LogWarning("No ItemStruct Component Found");
        }

        return obj;
    }
}
