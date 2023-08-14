using System.Collections.Generic;
using UnityEngine;

public abstract class Joint {
    public Rail primaryRail = null;
    public Rail secondaryRail = null;
    public Rail connectedRail = null;
    public Node node;

    public Joint(Node node) {
        this.node = node;
    }
    
    abstract public void movePrimaryRailPoints(Rail.Point type, Vector3 newPos);
    abstract public void changeNode(Node node);
}
