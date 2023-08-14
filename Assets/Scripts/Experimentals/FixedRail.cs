using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class FixedRail : Rail {

    public float startHandLength = 0.01f;
    public float endHandLength = 0.01f;

    public FixedRail(Vector3 start, Vector3 end) : base(start, end) {
        startHandle = start - new Vector3(startHandLength, 0, startHandLength);
        endHandle = end + new Vector3(startHandLength  , 0, endHandLength);
    }
    override public void setStartHandle(Vector3 point) {
    }
    override public void setEndHandle(Vector3 point) {
        
    }


}