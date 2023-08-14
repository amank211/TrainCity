using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainEngineSateCommand : Command
{

    [SerializeField]
    bool DEBUG = true;
    EngineController.State state;


    public TrainEngineSateCommand(EngineController.State state) {
        this.state = state;
    }

    public override void Execute() {
        TrainController trainController = ControllerHeart.getInstance().getController(ControllerHeart.TRAIN_CONTROLLER)
            as TrainController;
        trainController.currentTrain.getController().setState(state);
    }
}
