using Godot;

namespace IsolationInterstellarGameJam.Mono.GlobalScripts;

public partial class GameStateSignals : Node
{
	// Alarm
	[Signal]
	public delegate void AlarmSetOffEventHandler();
	// Timer Start
	[Signal]
	public delegate void StartTimerEventHandler();
	// Timer End (and failure)
	[Signal]
	public delegate void TimerElapsedEventHandler();
	// Successfully ejected
	[Signal]
	public delegate void VictoryEventHandler();
	// Player dies
	[Signal]
	public delegate void PlayerKilledEventHandler();
}