using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Consumed a click command and exexutes for the same.
/// </summary>
public class MouseCommand: Command{

    [SerializeField]
    bool DEBUG = true;

    Button mButton;
    List<ClickListener> clickListeners = new List<ClickListener>();
    
    public enum Button{ 
        LEFT,
        RIGHT
    }

	public MouseCommand(Button button){
        this.mButton = button;
	}

    public void setOnClickListener(ClickListener listener) { 
        clickListeners.Add(listener);
    }

    public override void Execute() {

        if (mButton == Button.LEFT) {
            OnButtonLeftClicked();
        } else if(mButton == Button.RIGHT) {
            OnButtonRightClicked();
        }

    }

    private void OnButtonLeftClicked() {
        foreach (ClickListener listener in clickListeners) {
            listener.OnButtonClicked(Button.LEFT);
        }
    }

    private void OnButtonRightClicked() {
        foreach (ClickListener listener in clickListeners)
        {
            listener.OnButtonClicked(Button.RIGHT);
        }
    }

    public interface ClickListener { 
        void OnButtonClicked(Button button);
    }
    
}
