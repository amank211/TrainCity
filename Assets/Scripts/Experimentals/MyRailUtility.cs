using UnityEngine;


public class MyRailUtilty {
    public static Vector3 GetAttachedNode(Rail movedRail, Rail rail, bool isStartHandle) {
        var distance = Vector3.Distance(rail.startNode.point, isStartHandle ? rail.startHandle : rail.endHandle);
        var rotation = (movedRail.endNode.point - movedRail.endHandle).normalized;
        return (isStartHandle ? rail.startNode.point : rail.endNode.point) + distance * rotation;
    }
}
