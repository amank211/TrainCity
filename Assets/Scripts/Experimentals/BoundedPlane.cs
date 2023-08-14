using UnityEngine;

public class BoundedPlane {

    public Vector3 a;
    public Vector3 b;
    public Vector3 c;
    public Vector3 d;

    public Plane plane;

    public BoundedPlane(Vector3 a, Vector3 b, Vector3 c, Vector3 d) {
        this.a = a;
        this.b = b;
        this.c = c;
        this.d = d;
        plane = new Plane(a, b, c);
    }
    
}
