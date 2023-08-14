using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Controller
{

    [SerializeField]
    bool DEBUG = true;

    Command commandKeyPressed_A;
    Command commandKeyPressed_D;
    Command commandMouse_Left;
    Command commandMouse_Right;

    bool isPressedKey_A = false;
    bool isPressedKey_D = false;

    public Command mSlideLeftCommand { get; } = new SlideLeftCommand();
    public Command mSlideRightCommand { get; } = new SlideRightCommand();
    public Command mMouseLeftCommand { get;  } = new MouseCommand(MouseCommand.Button.LEFT);
    public Command mMouseRightCommand { get; } = new MouseCommand(MouseCommand.Button.RIGHT);
    public Command accelerationCommand { get; } = new TrainEngineSateCommand(EngineController.State.ACCELERATING);
    public Command breakCommand { get; } = new TrainEngineSateCommand(EngineController.State.BREAKING);
    public Command idleCommand { get; } = new TrainEngineSateCommand(EngineController.State.IDLE);


    ISet<Command> mBlockCommands = new HashSet<Command>();

    // Start is called before the first frame update
    void Start()
    {
        DefaultConfigKey();
    }

    void DefaultConfigKey() {
        commandKeyPressed_A = breakCommand;
        commandKeyPressed_D = accelerationCommand;
        commandMouse_Left = mMouseLeftCommand;
        commandMouse_Right = mMouseRightCommand;
    }

    // Update is called once per frame
    void Update()
    {
        Command commaand = ObserveInputs();
        if (commaand != null && !mBlockCommands.Contains(commaand)) {
            commaand.Execute();
        }
    }

    public void BlockCommand(Command command) {
        mBlockCommands.Add(command);
    }

    public void UnBlockCommand(Command command) {
        mBlockCommands.Remove(command);
    }

    Command ObserveInputs() {
        if (Input.GetKeyDown(KeyCode.A)) {
            isPressedKey_A = true;
        }
        else if (Input.GetKeyUp(KeyCode.A)) {
            isPressedKey_A = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            isPressedKey_D = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            isPressedKey_D = false;
        }

        if (isPressedKey_A)
        {
            return commandKeyPressed_A;
        }
        else if (isPressedKey_D)
        {
            return commandKeyPressed_D;
        }
        else {
            return idleCommand;
        }
        return null;
    }
}
