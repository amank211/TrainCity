
using UnityEngine;

[System.Serializable]
public class Node
{
    [SerializeField]
    public Vector3 point;

    public Node(Vector3 point) {
        this.point = point;
    }
}
