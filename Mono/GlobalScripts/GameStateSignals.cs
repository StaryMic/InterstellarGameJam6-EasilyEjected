using Godot;

namespace IsolationInterstellarGameJam.Mono.GlobalScripts;

public partial class GameStateSignals : Node
{
	// Alarm
	[Signal]
	public delegate void AlarmSetOffEventHandler();
	// Timer End (and failure)
	[Signal]
	public delegate void TimerElapsedEventHandler();
	// Successfully ejected
	[Signal]
	public delegate void VictoryEventHandler();
	// Player Fails
	[Signal]
	public delegate void FailureEventHandler();
}