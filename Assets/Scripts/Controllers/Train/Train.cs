using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Train : MonoBehaviour {

    [SerializeField]
    private Engine engine;

    public EngineController getController() { 
        return engine.engineController;
    }

}
