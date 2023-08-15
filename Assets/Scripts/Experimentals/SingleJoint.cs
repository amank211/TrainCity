using UnityEngine;

public class SingleJoint : Joint{
    public SingleJoint(Node node) : base(node) { }

    public JunctionJoint convertToJunctionJoint(Rail rail) {
        JunctionJoint joint = new JunctionJoint(node);
        joint.setPrimaryRail(primaryRail);
        joint.setSecRail(secondaryRail);
        joint.setConnectedRail(rail);
        return joint;
    }

    public override void movePrimaryRailPoints(Rail.Point type, Vector3 newPos) {
        switch (type)
        {
            case Rail.Point.ST:
            case Rail.Point.EN:
            case Rail.Point.ST_HAND:
                primaryRail.setStartHandle(newPos);
                Rail secJointPrimRail = primaryRail.jointOne.primaryRail;
                var distance = Vector3.Distance(secJointPrimRail.endNode.point, secJointPrimRail.endHandle);
                var rotation = (primaryRail.startNode.point - primaryRail.startHandle).normalized;
                secJointPrimRail.autoMoveEndHandle(secJointPrimRail.endNode.point + distance * rotation);
                break;
            case Rail.Point.EN_HAND:
                primaryRail.setEndHandle(newPos);
                distance = Vector3.Distance(secondaryRail.startNode.point, secondaryRail.startHandle);
                rotation = (primaryRail.endNode.point - primaryRail.endHandle).normalized;
                secondaryRail.autoMoveStHandle(secondaryRail.startNode.point + distance * rotation);
                break;
        }
    }

    public override void changeNode(Node node) {
        this.node = node;
        primaryRail.setEnd(node);
        secondaryRail.setStart(node);
    }

    public void setPrimaryRail(Rail rail) {
        primaryRail = rail;
    }

    public void setSecRail(Rail rail) {
        secondaryRail = rail;
    }
}
