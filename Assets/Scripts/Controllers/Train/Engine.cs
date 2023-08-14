using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour, EngineController.StateChangeListener {
    public EngineController engineController;

    [SerializeField]
    float accelerationPowerMax = 10f;

    [SerializeField]
    float breakingPower = 2f;

    float acceleration = 0f;

    EngineController.State state = EngineController.State.IDLE;

    public void onStateChanged(EngineController.State state) {
        this.state = state;
    }

    private void Start() {
        engineController = new EngineController(this);
    }

    private void Update() {
        switch (state) {
            case EngineController.State.IDLE:
                break;
            case EngineController.State.ACCELERATING:
                if (acceleration < accelerationPowerMax)
                    acceleration += Time.deltaTime;
                else
                    acceleration = accelerationPowerMax;
                break;
            case EngineController.State.BREAKING:
                if (acceleration > 0)
                    acceleration -= breakingPower * Time.deltaTime;
                break;
        }

        Vector3 newPosition = transform.position;
        newPosition.z += acceleration * Time.deltaTime;
        transform.position = newPosition;
    }
}
