using UnityEngine;
using System.Linq;
using System.Collections.Concurrent;

public class SideBarUI : MonoBehaviour
{
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject itemButtonPrefab;

    void OnEnable()
    {
        RebuildAll();

        if (DiscoveryTracker.instance != null)
            DiscoveryTracker.instance.OnDiscovered += HandleDiscovered;
    }

    void OnDisable()
    {
        if (DiscoveryTracker.instance != null)
            DiscoveryTracker.instance.OnDiscovered -= HandleDiscovered;
    }

    void HandleDiscovered(string id) => AddButton(id);

    void RebuildAll()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            Destroy (content.GetChild(i).gameObject);
        }
        foreach (var id in DiscoveryTracker.instance.AllDiscoveredIds().OrderBy(x => x))
            AddButton(id);
    }
    void AddButton(string id)
    {
        var b = Instantiate(itemButtonPrefab, content);
        b.GetComponent<ItemButton>().InitWithId(id);
    }
}
