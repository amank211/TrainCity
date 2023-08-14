using System;
using UnityEngine;

public class SlideRightCommand : Command {

	[SerializeField]
	bool DEBUG = true;

	public SlideRightCommand()
	{
	}

	public override void Execute() {
		SlideRight();
	}

	void SlideRight() {
	}


}