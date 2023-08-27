using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform FindRecursively(this Transform parent, string name)
    {
        Transform result = parent.Find(name);
        if (result != null)
        {
            return result;
        }

        foreach (Transform child in parent)
        {
            result = child.FindRecursively(name);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }
}