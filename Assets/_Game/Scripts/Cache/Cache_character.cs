using System.Collections.Generic;
using UnityEngine;

public static class Cache_character
{
    private static Dictionary<Collider, Character> characters = new Dictionary<Collider, Character>();

    public static Character GetChar(Collider collider)
    {
        if (!characters.ContainsKey(collider))
        {
            characters.Add(collider, collider.GetComponent<Character>());
        }
        
        return characters[collider];
    }

    public static void ClearCache()
    {
        characters.Clear();
    }
}
