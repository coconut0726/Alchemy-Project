using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DiscoveryTracker : MonoBehaviour
{
    [Tooltip("IDs of items the player has at the beginning of the game")]
    [SerializeField] private string[] startingIds = { "apple", "banana", "watermelon" };

    private HashSet<string> discovered = new(StringComparer.OrdinalIgnoreCase);

    public static DiscoveryTracker instance { get; private set; }
    public event Action<string> OnDiscovered; //Event triggers when something new is discovered

    private void Awake()
    {
        instance = this;
        Seed();
    }

    public bool isDiscovered (string id) => discovered.Contains(id);
    public IEnumerable<string> AllDiscoveredIds() => discovered;

    public bool MarkDiscovered(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            return false;
        }
        //try add to hashset, if new and added, return true
        if (discovered.Add(id))
        {
            OnDiscovered?.Invoke(id);
            return true;
        }
        return false;
    }

    void Seed()
    {
        foreach(var id in startingIds)
        {
            discovered.Add(id);
        }
    }
}
