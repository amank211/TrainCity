using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log {

    static bool debug = true;

    static bool error = true;

    static bool verbose = true;

    static public void d(string tag, string message) {
        if (debug) 
            Debug.Log("D: " + tag + " : " + message);
    }

    static public void e(string tag, string errorMessage) {
        if (error)
            Debug.Log("E: " + tag + " : " + errorMessage);
    }
}
