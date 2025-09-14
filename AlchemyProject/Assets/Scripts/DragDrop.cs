using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private ItemSruct itemSruct;

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>(); //Possibly causing problem if there's mutiple canvas called canvas
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        itemSruct = GetComponent<ItemSruct>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        //Debug.Log("OnPointerDown");
    }

    public void OnDrag(PointerEventData eventData) {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log($"Item {eventData.pointerDrag.name} dropped on {gameObject.name} at {transform.position}");

        GameObject droppedObj = eventData.pointerDrag;

        //Debug.Log(droppedObj.GetType());

        if (droppedObj == null) {
            Debug.Log("droppedObj == null");
            return; 
        } 

        ItemSruct droppedItem = droppedObj.GetComponent<ItemSruct>();

        if (droppedItem == null)
        {
            Debug.Log("dropped Item null");
            return;
        }
        else if (itemSruct == null) {
            Debug.Log("this itemStruct null");
            return;
        }

        if (RecipeIndex.instance.TryCombine(itemSruct.itemID, droppedItem.itemID, out ItemRow result))
        {
            Debug.Log("Create" + result.displayName);
            DiscoveryTracker.instance.MarkDiscovered(result.id);

            //Spawn
            ItemSpawner spawner = FindAnyObjectByType<ItemSpawner>();
            Vector2 spawnPos = rectTransform.anchoredPosition;
            spawner.InitiateItem(spawnPos, result);

            //Destroy
            Destroy(droppedItem);
            Destroy(droppedObj);
            Destroy(gameObject);


        }
    }
    public void OnBeginDrag(PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData) {
        //Debug.Log("OnEndGrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
}

