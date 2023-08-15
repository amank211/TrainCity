using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RailData {

    public LinkedList<Rail> rails = new LinkedList<Rail>();
    public LinkedList<Joint> joints = new LinkedList<Joint>();

    public BoundedPlane unityPlane;
    public float planeRange = 1000f;

    public static int lastID = 1;

    public RailData() {
        createNewRailNetwork(new Node(new Vector3(0,0,0)));
        unityPlane = new BoundedPlane(new Vector3(-planeRange,0,-planeRange), new Vector3(-planeRange,0,planeRange), new Vector3(planeRange,0,planeRange), new Vector3(planeRange, 0, -planeRange));
    }

    public void addNewRail(LinkedListNode<Joint> jointNode, Node node) {
        var joint = jointNode.Value;
        if (joint is RootJoint)
        {
            handleNewRailForRootJoint(jointNode, node);
        }
        else if (joint is DeadJoint)
        {
            handleNewRailForDeadJoint(jointNode, node);
        }
        else if (joint is SingleJoint) {
            handleNewRailForSingleJoint(jointNode, node);
        }
    }

    private void handleNewRailForRootJoint(LinkedListNode<Joint> jointNode, Node node) {
        var joint = jointNode.Value;
        var rail = new Rail(joint.node, node);
        var newJoint = new DeadJoint(node);
        newJoint.setPrimaryRail(rail);
        rail.jointOne = joint;
        rail.jointTwo = newJoint;
        joints.AddLast(newJoint);
        rails.AddLast(joint.secondaryRail);

    }

    private void handleNewRailForDeadJoint(LinkedListNode<Joint> jointNode, Node node) {
        var joint = jointNode.Value;
        Rail secondaryRail = new Rail(joint.node, node);
        var convertedjoint = (joint as DeadJoint).convertToSingleJoint(secondaryRail);
        var newJoint = new DeadJoint(node);
        secondaryRail.jointOne = convertedjoint;
        secondaryRail.jointTwo = newJoint;
        newJoint.setPrimaryRail(convertedjoint.secondaryRail);
        secondaryRail.autoMoveStHandle(MyRailUtilty.GetAttachedNode(convertedjoint.primaryRail, secondaryRail, true));
        joints.AddAfter(jointNode, convertedjoint);
        joints.Remove(jointNode);
        joints.AddLast(newJoint);
        rails.AddLast(joint.secondaryRail);
    }

    private void handleNewRailForSingleJoint(LinkedListNode<Joint> jointNode, Node node) {
        var joint = jointNode.Value;
        Node nodeFixed = new Node(getNodesForFixedRail(joint.node.point, node.point));
        Rail fixedRail = new FixedRail(joint.node, nodeFixed);
        Rail connectedRail = new Rail(nodeFixed, node);
        var convertedjoint = (joint as SingleJoint).convertToJunctionJoint(fixedRail);
        var newJoint = new DeadJoint(node);
        var midJoint = new SingleJoint(nodeFixed);
        midJoint.setPrimaryRail(fixedRail);
        midJoint.setSecRail(connectedRail);
        fixedRail.jointOne = convertedjoint;
        fixedRail.jointTwo = midJoint;
        connectedRail.jointOne = midJoint;
        connectedRail.jointTwo = newJoint;
        Rail secondaryRail = joint.secondaryRail;
        secondaryRail.jointOne = convertedjoint;
        var distance = 1;
        var rotation = -(convertedjoint.primaryRail.endHandle - convertedjoint.secondaryRail.startHandle).normalized;
        connectedRail.autoMoveStHandle(connectedRail.startNode.point + distance * rotation);
        newJoint.setPrimaryRail(connectedRail);
        joints.AddAfter(jointNode, convertedjoint);
        joints.AddAfter(joints.Find(convertedjoint), midJoint);
        joints.Remove(jointNode);
        joints.AddLast(newJoint);
        rails.AddLast(fixedRail);
        rails.AddLast(joint.secondaryRail);
    }

    private Vector3 getNodesForFixedRail(Vector3 st, Vector3 end) {
        var distance = Vector3.Distance(st, end);
        var fraction = distance / 25;
        var direction = (end - st).normalized;
        Vector3 fixedNodes = st + fraction * direction;
        return fixedNodes;
    }

    public void createNewRailNetwork(Node node) {
        var rootJoint = new RootJoint(node);
        joints.AddFirst(rootJoint);
    }

}
