using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RailBuilder))]
public class RailEditor : Editor {

    RailBuilder builder;
    Joint currentJointSelected = null;

    void OnSceneGUI() {
        Draw();
        Input();
    }

    void Input() {
        Event guiEvent = Event.current;
        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift) {
            Undo.RecordObject(builder, "Add joint");
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

            //Initialise the enter variable
            float enter = 0.0f;
            if (builder.railData.unityPlane.plane.Raycast(ray, out enter))
            {
                //Get the point that is clicked
                Vector3 hitPoint = ray.GetPoint(enter);
                LinkedListNode<Joint> currentNode = builder.railData.joints.Find(currentJointSelected);

                if (currentNode != null)
                {
                    builder.railData.addNewRail(currentNode, new Node(hitPoint));
                }
                
            }
        }

        if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1) {
            currentJointSelected = null;
        }
    }

    void Draw() {
        Vector3[] verts = new Vector3[]
        {
            builder.railData.unityPlane.a, builder.railData.unityPlane.b, builder.railData.unityPlane.c, builder.railData.unityPlane.d
        };

        Handles.DrawSolidRectangleWithOutline(verts, new Color(100f, 0.5f, 0.5f, 0.1f), new Color(100, 0, 0, 1));
        LinkedList<JointNode> editorGraph = getUiGraph(builder.railData.joints);

        Handles.color = Color.red;
        LinkedList<JointNode>.Enumerator jointsEnum = editorGraph.GetEnumerator();

        // Root Joint
        jointsEnum.MoveNext();
        JointNode jointNode = jointsEnum.Current;
        Event guiEvent = Event.current;
        var point = Handles.FreeMoveHandle(jointNode.joint.node.point, Quaternion.identity, 1f, Vector2.zero, Handles.CylinderHandleCap);
        if (point != jointNode.joint.node.point)
        {
            Undo.RecordObject(builder, "Move Point");
            if (guiEvent.control)
            {
                currentJointSelected = jointNode.joint;
                Debug.Log(currentJointSelected.node.point);
            }
            else
            {
                jointNode.joint.changeNode(new Node(point));
            }
            jointNode.joint.changeNode(new Node(point));
        }

        while (jointsEnum.MoveNext())
        {
            Joint joint = jointsEnum.Current.joint;
            bool isDead = joint is DeadJoint;
            bool isSingle = joint is SingleJoint;
            bool isJunction = joint is JunctionJoint;

            if(isDead)
                Handles.color = Color.black;
            else if(isSingle)
                Handles.color = Color.green;
            bool isSelected = currentJointSelected == joint;
            
            var rail = joint.primaryRail;
            bool isFixedRail = (rail is FixedRail);

            if (isDead) {
                Handles.color = isSelected ? Color.magenta : Color.black;
                point = Handles.FreeMoveHandle(joint.node.point, Quaternion.identity, 1f, Vector2.zero, Handles.CylinderHandleCap);
                if (point != joint.node.point)
                {
                    Undo.RecordObject(builder, "Move Point");
                    if (guiEvent.control)
                    {
                        currentJointSelected = joint;
                        Debug.Log(currentJointSelected.node.point);
                    }
                    else
                    {
                        joint.changeNode(new Node(point));
                    }
                }
                
                Handles.DrawBezier(rail.startNode.point, rail.endNode.point, rail.startHandle, rail.endHandle, isSelected ? Color.magenta : Color.cyan, null, 2f);
                Handles.color = Color.white;
                var stHandle = Handles.FreeMoveHandle(rail.startHandle, Quaternion.identity, !isFixedRail ? 0.5f : 0.2f, Vector2.zero, Handles.CylinderHandleCap);
                if (stHandle != rail.startHandle)
                {
                    Undo.RecordObject(builder, "Move Point");
                    joint.movePrimaryRailPoints(Rail.Point.ST_HAND, stHandle);
                }

                var endHandle = Handles.FreeMoveHandle(rail.endHandle, Quaternion.identity, !isFixedRail ? 0.5f : 0.2f, Vector2.zero, Handles.CylinderHandleCap);
                if (endHandle != rail.endHandle)
                {
                    Undo.RecordObject(builder, "Move Point");
                    joint.movePrimaryRailPoints(Rail.Point.EN_HAND, endHandle);
                }
                Handles.DrawLine(stHandle, rail.startNode.point);
                Handles.DrawLine(endHandle, rail.endNode.point);
            }

            if (isSingle) {
                Handles.color = isSelected ? Color.magenta : Color.green;
                point = Handles.FreeMoveHandle(joint.node.point, Quaternion.identity, !isFixedRail ? 1f : 0.5f, Vector2.zero, Handles.CylinderHandleCap);
                if (point != joint.node.point)
                {
                    Undo.RecordObject(builder, "Move Point");
                    if (guiEvent.control)
                    {
                        currentJointSelected = joint;
                        Debug.Log(currentJointSelected.node.point);
                    }
                    else
                    {
                        joint.changeNode(new Node(point));
                    }
                }
                var secRail = joint.secondaryRail;
                Handles.DrawBezier(rail.startNode.point, rail.endNode.point, rail.startHandle, rail.endHandle, isSelected ? Color.magenta : Color.cyan, null, 2f);
                Handles.color = Color.white;
                if (rail != null)
                {
                    Handles.color = Color.gray;
                    var stHandle = Handles.FreeMoveHandle(rail.startHandle, Quaternion.identity, !isFixedRail ? 0.5f : 0.2f, Vector2.zero, Handles.CylinderHandleCap);
                    if (stHandle != rail.startHandle)
                    {
                        Undo.RecordObject(builder, "Move Point");
                        joint.movePrimaryRailPoints(Rail.Point.ST_HAND, stHandle);
                    }

                    Handles.color = Color.black;
                    var endHandle = Handles.FreeMoveHandle(rail.endHandle, Quaternion.identity, !isFixedRail ? 0.5f : 0.2f, Vector2.zero, Handles.CylinderHandleCap);

                    if (endHandle != rail.endHandle)
                    {
                        Undo.RecordObject(builder, "Move Point");
                        joint.movePrimaryRailPoints(Rail.Point.EN_HAND, endHandle);
                    }
                    Handles.DrawLine(stHandle, rail.startNode.point);
                    Handles.DrawLine(endHandle, rail.endNode.point);
                }
            }

            if (isJunction)
            {
                Handles.color = isSelected ? Color.magenta : Color.green;
                point = Handles.FreeMoveHandle(joint.node.point, Quaternion.identity, 1f, Vector2.zero, Handles.CylinderHandleCap);
                if (point != joint.node.point)
                {
                    Undo.RecordObject(builder, "Move Point");
                    if (guiEvent.control)
                    {
                        currentJointSelected = joint;
                        Debug.Log(currentJointSelected.node.point);
                    }
                    else
                    {
                        joint.changeNode(new Node(point));
                    }
                }
                var secRail = joint.secondaryRail;
                Handles.DrawBezier(rail.startNode.point, rail.endNode.point, rail.startHandle, rail.endHandle, isSelected ? Color.magenta : Color.cyan, null, 2f);
                Handles.color = Color.white;
                if (rail != null)
                {
                    Handles.color = Color.gray;
                    var stHandle = Handles.FreeMoveHandle(rail.startHandle, Quaternion.identity, !isFixedRail ? 0.5f : 0.2f, Vector2.zero, Handles.CylinderHandleCap);
                    if (stHandle != rail.startHandle)
                    {
                        Undo.RecordObject(builder, "Move Point");
                        joint.movePrimaryRailPoints(Rail.Point.ST_HAND, stHandle);
                    }
                    Handles.color = Color.black;
                    var endHandle = Handles.FreeMoveHandle(rail.endHandle, Quaternion.identity, !isFixedRail ? 0.5f : 0.2f, Vector2.zero, Handles.CylinderHandleCap);

                    if (endHandle != rail.endHandle)
                    {
                        Undo.RecordObject(builder, "Move Point");
                        joint.movePrimaryRailPoints(Rail.Point.EN_HAND, endHandle);
                    }
                    Handles.DrawLine(stHandle, rail.startNode.point);
                    Handles.DrawLine(endHandle, rail.endNode.point);
                }
            }
        }

    }

    void OnEnable() {
        builder = (RailBuilder)target;
        if (builder.railData == null)
        {
            builder.createRailData();
        }
    }

    public class JointNode {
        bool isVisited = false;
        public Joint joint;

        public JointNode(Joint joint) {
            this.joint = joint;
        }
    }

    public LinkedList<JointNode> getUiGraph(LinkedList<Joint> linkedList) {
        LinkedList<Joint>.Enumerator jointsEnum = linkedList.GetEnumerator();
        LinkedList<JointNode> result = new LinkedList<JointNode>();

        while (jointsEnum.MoveNext())
        {
            Joint joint = jointsEnum.Current;
            JointNode node = new JointNode(joint);
            result.AddLast(node);
        }
        return result;
    }
}
