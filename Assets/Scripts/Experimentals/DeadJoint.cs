using UnityEngine;

public class DeadJoint : Joint{
    public DeadJoint(Node node) : base(node){
        Debug.Log("dead joint created " + primaryRail);
    }

    public SingleJoint convertToSingleJoint(Rail rail) {
        SingleJoint joint  = new SingleJoint(node);
        joint.setPrimaryRail(primaryRail);
        joint.setSecRail(rail);
        return joint;
    }

    override public void movePrimaryRailPoints(Rail.Point type, Vector3 newPos) {
        
        switch (type) {
            case Rail.Point.ST:
            case Rail.Point.EN:
            case Rail.Point.ST_HAND:
                Joint jointOne = primaryRail.jointOne;
                Rail secJointPrimRail = jointOne.primaryRail;
                Rail jointConnectedRail = jointOne.connectedRail;
                if (secJointPrimRail != null)
                {
                    var distance = Vector3.Distance(secJointPrimRail.endNode.point, secJointPrimRail.endHandle);
                    var rotation = (primaryRail.startNode.point - primaryRail.startHandle).normalized;
                    secJointPrimRail.autoMoveEndHandle(secJointPrimRail.endNode.point + distance * rotation);
                }
                
                if (jointConnectedRail != null) {
                    if (jointConnectedRail == primaryRail)
                        return;
                    var rotation = -(jointOne.primaryRail.endHandle - jointOne.secondaryRail.startHandle).normalized;
                    var distance = Vector3.Distance(jointConnectedRail.startNode.point, jointConnectedRail.startHandle);
                    jointConnectedRail.autoMoveStHandle(jointConnectedRail.startNode.point + distance * rotation);
                }

                primaryRail.setStartHandle(newPos);
                break;
            case Rail.Point.EN_HAND:
                primaryRail.setEndHandle(newPos);
                break;     
        }
    }

    public override void changeNode(Node node) {
        this.node = node;
        primaryRail.setEnd(node);
    }

    public void setPrimaryRail(Rail rail) {
        this.primaryRail = rail;
    }
}
