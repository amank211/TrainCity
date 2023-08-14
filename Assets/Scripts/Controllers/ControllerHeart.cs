using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerHeart : MonoBehaviour
{

    public static ControllerHeart mInstance;

    public const int INPUT_CONTROLLER = 1;
    public const int TRAIN_CONTROLLER = 2;

    [SerializeField]
    public InputController inputController;

    [SerializeField]
    public TrainController trainController;

    private void Start() {
        mInstance = this;
    }

    public static ControllerHeart getInstance() {
        return mInstance;
    }

    public Controller getController(int id) {
        switch(id) {
            case INPUT_CONTROLLER:
                return inputController;
            case TRAIN_CONTROLLER:
                return trainController;
            default:
                return null;
        }
    }
}
