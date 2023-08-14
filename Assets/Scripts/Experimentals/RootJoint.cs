using UnityEngine;

public class RootJoint : Joint{
    public RootJoint(Node node) : base(node){}

    public override void movePrimaryRailPoints(Rail.Point type, Vector3 newPos) { }

    public override void changeNode(Node node) { }
}
