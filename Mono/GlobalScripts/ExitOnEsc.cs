using Godot;
using System;

public partial class ExitOnEsc : Node
{
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("ui_cancel"))
		{
			GetTree().Quit();
		}
	}
}
