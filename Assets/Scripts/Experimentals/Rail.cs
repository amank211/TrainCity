using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Rail {

    public enum Point {
        ST, EN, ST_HAND, EN_HAND
    }

    [SerializeField]
    public int id;

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

    public Rail(Node start, Node end) {
        startNode = start;
        endNode = end;
        id = RailData.lastID;
        RailData.lastID++;
        var direction = (start.point - end.point).normalized;
        startHandle = start.point - direction;
        endHandle = end.point + direction;
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
}
