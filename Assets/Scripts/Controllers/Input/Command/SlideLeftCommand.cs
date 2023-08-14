using System;
using UnityEngine;

public class SlideLeftCommand : Command {

	[SerializeField]
	bool DEBUG = true;

	public SlideLeftCommand() {
	}

	public override void Execute() {
        SlideLeft();
	}

	void SlideLeft() {
	}
	
}
