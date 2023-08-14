using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class JunctionJoint : Joint {
    public JunctionJoint(Node node) : base(node) {

    }

    public override void changeNode(Node node) {
        this.node = node;
        primaryRail.setEnd(node);
        secondaryRail.setStart(node);
        connectedRail.setStart(node);
    }

    public override void movePrimaryRailPoints(Rail.Point type, Vector3 newPos) {
        switch (type)
        {
            case Rail.Point.ST:
            case Rail.Point.EN:
            case Rail.Point.ST_HAND:
                primaryRail.setStartHandle(newPos);
                Rail secJointPrimRail = primaryRail.jointOne.primaryRail;
                if (secJointPrimRail != null)
                {
                    var distance = Vector3.Distance(secJointPrimRail.endNode.point, secJointPrimRail.endHandle);
                    Debug.Log(distance);
                    var rotation = (primaryRail.startNode.point - primaryRail.startHandle).normalized;
                    secJointPrimRail.autoMoveEndHandle(secJointPrimRail.endNode.point + distance * rotation);
                }
                break;
            case Rail.Point.EN_HAND:
                primaryRail.setEndHandle(newPos);
                if (secondaryRail != null)
                {
                    var distance = Vector3.Distance(secondaryRail.startNode.point, secondaryRail.startHandle);
                    var rotation = (primaryRail.endNode.point - primaryRail.endHandle).normalized;
                    secondaryRail.autoMoveStHandle(secondaryRail.startNode.point + distance * rotation);
                }
                if (connectedRail != null)
                {
                    var distance = Vector3.Distance(connectedRail.startNode.point, connectedRail.startHandle);
                    var rotation = -(secondaryRail.startNode.point - secondaryRail.startHandle).normalized;
                    connectedRail.autoMoveStHandle(connectedRail.startNode.point + distance * rotation);
                }
                break;
        }
    }

    public void setPrimaryRail(Rail rail) {
        this.primaryRail = rail;
    }

    public void setSecRail(Rail rail) {
        this.secondaryRail = rail;
    }

    public void setConnectedRail(Rail rail) {
        this.connectedRail = rail;
    }
}

