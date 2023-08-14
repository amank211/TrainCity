using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBuilder : MonoBehaviour {

    [HideInInspector]
    public RailData railData;

    public void createRailData() {
        railData = new RailData();
    }
}
