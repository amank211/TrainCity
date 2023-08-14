using PathCreation;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Follower : MonoBehaviour
{

    public PathCreator creator;
    public float speed = 1f;
    float distanceTravelled;
    GameObject engineObject;
    GameObject wagonObject;
    List<GameObject> wagons;
    float wagonOffset;

    const string TAG = "Follower";

    // Start is called before the first frame update  
    void Start()
    {
        engineObject = Utility.getChildFromOnlyTag("ENGINE", transform);
        wagonObject = Utility.getChildFromOnlyTag("WAGON", transform);
        wagons = Utility.getAllChildren(wagonObject.transform);
        wagonOffset = Mathf.Abs(wagonObject.transform.localPosition.z);
        Log.d(TAG, wagonOffset.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        distanceTravelled += speed * Time.deltaTime;
        Log.d(TAG, distanceTravelled.ToString());
        engineObject.transform.position = creator.path.GetPointAtDistance(distanceTravelled);
        engineObject.transform.rotation = creator.path.GetRotationAtDistance(distanceTravelled);
        float wagonDisatance = distanceTravelled - 0.2f;
        for (int i = 0; i<wagons.Count; i++) {
            wagons[i].transform.position = creator.path.GetPointAtDistance(wagonDisatance - 0.08f * i);
            wagons[i].transform.rotation = creator.path.GetRotationAtDistance(wagonDisatance - 0.08f * i);
        }
        
    }
}
