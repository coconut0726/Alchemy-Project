using UnityEngine;
using TMPro;
using System.Linq;

public class RecipeCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;

    void OnEnable()
    {

        UpdateCounter();
        if (DiscoveryTracker.instance != null)
            DiscoveryTracker.instance.OnDiscovered += HandleDiscovered;
    }

    void OnDisable()
    {
        if (DiscoveryTracker.instance != null)
            DiscoveryTracker.instance.OnDiscovered -= HandleDiscovered;
    }

    void HandleDiscovered(string id) => UpdateCounter();

    void UpdateCounter()
    {
        int discovered = DiscoveryTracker.instance.AllDiscoveredIds().Count();
        int total = RecipeIndex.instance.AllItems.Count();
        counterText.text = $"{discovered} / {total}";
    }
}
