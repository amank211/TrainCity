using UnityEngine;

public class FixedRail : Rail {

    public float startHandLength = 0.01f;
    public float endHandLength = 0.01f;

    public FixedRail(Node start, Node end) : base(start, end) {
        startHandle = start.point - new Vector3(startHandLength, 0, startHandLength);
        endHandle = end.point + new Vector3(startHandLength  , 0, endHandLength);
    }
    override public void setStartHandle(Vector3 point) {
        // Since Fixed rails handles are fixed, so do nothing.
    }
    override public void setEndHandle(Vector3 point) {
        // Since Fixed rails handles are fixed, so do nothing.
    }


}