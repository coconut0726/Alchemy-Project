using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System;

//This is needed to ensure this script always spawns before any others
[DefaultExecutionOrder(-10000)]
public class RecipeIndex : MonoBehaviour
{
    [SerializeField] AlchemyDB db;

    Dictionary<string, ItemRow> itemsById;
    Dictionary<(string, string), string> pairToResult;

    public static RecipeIndex instance { get; private set; }
    private void Awake()
    {
        instance = this;
        Build();
    }

    void Build()
    {
        itemsById = db.items.ToDictionary(i => i.id, StringComparer.OrdinalIgnoreCase);
        pairToResult = new Dictionary<(string, string), string>();
        foreach (var r in db.recipes)
        {
            var k = Key(r.a, r.b);
            pairToResult[k] = r.r;

        }

#if UNITY_EDITOR
        // in editor only, checks if all recipes are valid or if theres missing recipe
        foreach (var kv in pairToResult)
            if (!itemsById.ContainsKey(kv.Value))
                Debug.LogWarning($"Recipe output '{kv.Value}' missing in items!");
#endif
    }

    //Helper function allowing 2 strings in any order to produce the same key
    static (string, string) Key(string a, string b)
    => (string.CompareOrdinal(a, b) <= 0) ? (a, b) : (b, a);

    public bool TryCombine(string a, string b, out ItemRow result)
    {
        result = null;
        if(pairToResult.TryGetValue(Key(a,b), out var id))
        {
            return itemsById.TryGetValue(id, out result);
        }
        return false;
    }

    //functions to get stuff in db for xinyu
    public ItemRow GetItem(string id) => itemsById[id];
    public Sprite GetIcon(string id) => itemsById.TryGetValue(id, out var it) ? it.icon : null;
    public IEnumerable<ItemRow> AllItems => itemsById.Values;
}
