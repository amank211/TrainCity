using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngineController : MonoBehaviour {


    public enum State { 
        ACCELERATING,
        BREAKING,
        IDLE
    }
     
    public interface StateChangeListener {
        public void onStateChanged(State state);
    }

    private StateChangeListener listener;

    public EngineController(StateChangeListener listener) {
        this.listener = listener;
    }

    public void setStateChangeListener(StateChangeListener listener) {
        this.listener = listener;
    }

    public void setState(State state) {
        listener.onStateChanged(state);
    }
}
