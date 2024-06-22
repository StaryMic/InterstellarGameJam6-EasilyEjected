using Godot;

namespace IsolationInterstellarGameJam.Mono.GlobalScripts;

public partial class ExitOnEsc : Node
{
	public override void _Ready()
	{
		ProcessMode = ProcessModeEnum.Always;
	}

	public override void _Input(InputEvent @event)
	{
		if (Input.IsActionJustPressed("ui_cancel"))
		{
			GetTree().Quit();
		}
		
		if (Input.IsActionJustPressed("Restart"))
		{
			GetTree().Paused = false;
			GetTree().ReloadCurrentScene();
		}
	}
}