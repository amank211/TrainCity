using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rail
{
    [SerializeField]
    public int id;
    public bool isDrawn = false;

    [SerializeField]
    public Node startNode;
    [SerializeField]
    public Node endNode;
    [SerializeField]
    public Vector3 startHandle;
    [SerializeField]
    public Vector3 endHandle;

    public Joint jointOne = null;
    public Joint jointTwo = null;

    GameObject railObject;

    public enum Point {
        ST, EN, ST_HAND, EN_HAND
    }

    public Rail(Vector3 start, Vector3 end) {
        startNode = new Node(start);
        endNode = new Node(end);
        id = RailData.lastID;
        RailData.lastID++;
        var direction = (start - end).normalized;
        startHandle = start - direction;
        endHandle = end + direction;
    }

    virtual public void setStart(Node node) {
        var delta = startNode.point - node.point;
        startNode.point = node.point;
        startHandle -= delta;
    }

    virtual public void setEnd(Node node) {
        var delta = endNode.point - node.point;
        endNode.point = node.point;
        endHandle -= delta;
    }
    public void autoMoveStHandle(Vector3 point) {
        startHandle = point;
    }

    public void autoMoveEndHandle(Vector3 point) {
        endHandle = point;
    }

    virtual public void setStartHandle(Vector3 point) {
        startHandle = point;
    }
    virtual public void setEndHandle(Vector3 point) {
        endHandle = point;
    }

    public float getDistance() {
        return Vector3.Distance(startNode.point, endNode.point);
    }

    public Vector3 getDirection() {
        return (startNode.point - endNode.point).normalized;
    }
}
