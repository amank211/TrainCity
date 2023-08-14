using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Utility class to hold common APIs which is required very frequently.
/// </summary>
public class Utility
{

    /// <summary>
    /// API to get allChildren of an object from its tag.
    /// </summary>
    /// <param name="tag"> tag of gameObject which children to be find</param>
    /// <param name="transform">transform of gameobject</param>
    /// <returns></returns>
    public static List<GameObject> getAllChildrenFromTag(string tag, Transform transform) {
        List<GameObject> objs = new List<GameObject>();

        foreach (Transform child in transform) {
            if (child.tag == tag) {
                objs.Add(child.gameObject);
            }
        }
        return objs;
    }

    public static List<GameObject> getAllChildren(Transform transform) {
        List<GameObject> objs = new List<GameObject>();

        foreach (Transform child in transform) {
            objs.Add(child.gameObject);
        }
        return objs;
    }

    public static GameObject getChildFromOnlyTag(string tag, Transform transform) {
        return Utility.getAllChildrenFromTag(tag, transform)[0];
    }
}
