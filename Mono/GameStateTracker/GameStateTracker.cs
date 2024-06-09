using Godot;
using System;
using IsolationInterstellarGameJam.Mono.AirlockHatch;
using IsolationInterstellarGameJam.Mono.GlobalScripts;
using IsolationInterstellarGameJam.Mono.HUD;
using IsolationInterstellarGameJam.Mono.Player;

[GlobalClass]
public partial class GameStateTracker : Node
{
	[Export] private float LevelTimer = 30f;
	
	// Node references
	private GameStateSignals _gameStateSignals;

	private PlayerCharacter _player;
	private HeadsUpDisplay _hud;
	private AirlockHatch _airlock;
	
	// State objects
	public Timer _timer = new Timer();
	
	// State variables
	private bool hasWon;
	
	public override void _Ready()
	{
		// Get node references
		_gameStateSignals = GetNode<GameStateSignals>("/root/GameStateSignals");
		_player = GetTree().Root.GetChild(-1).GetNode<PlayerCharacter>("Player");
		_airlock = GetTree().Root.GetChild(-1).GetNode<AirlockHatch>("Airlock");
		_hud = GetTree().Root.GetChild(-1).GetNode<HeadsUpDisplay>("HUD/HeadsUpDisplay");
		
		
		// Connect game events
		_gameStateSignals.AlarmSetOff += GameStateSignalsOnAlarmSetOff;
		_gameStateSignals.Victory += GameStateSignalsOnVictory;
		_gameStateSignals.TimerElapsed += GameStateSignalsOnTimerElapsed;
		
		// Suicidal Signals
		this.TreeExiting += () =>
		{
			_gameStateSignals.AlarmSetOff -= GameStateSignalsOnAlarmSetOff;
			_gameStateSignals.Victory -= GameStateSignalsOnVictory;
			_gameStateSignals.TimerElapsed -= GameStateSignalsOnTimerElapsed;
		};
		
		// Make the timer
		_timer = GetNode<Timer>("Timer");
		
		GD.Print("READY BLAH BLAH");
	}

	private void GameStateSignalsOnTimerElapsed()
	{
		if (!hasWon)
		{
			_airlock.StopSucking();
			GD.Print("Timer has elapsed. Fail or Win for dry cat?");
			_gameStateSignals.EmitSignal(GameStateSignals.SignalName.Failure);
			_hud.ShowFailScreen();
			GetTree().Paused = true;
		}
	}

	

	private void GameStateSignalsOnVictory()
	{
		GD.Print("yroure winer :)");
		hasWon = true;
		_hud.ShowWinScreen();
		GetTree().Paused = true;
	}

	private void GameStateSignalsOnAlarmSetOff()
	{
		if(_timer.TimeLeft != 0)
			return;
		_timer.Start(LevelTimer);
		_timer.Timeout += TimerOnTimeout;
		GD.Print("Alarm has been set off");
	}

	private void TimerOnTimeout()
	{
		_gameStateSignals.EmitSignal(GameStateSignals.SignalName.TimerElapsed);
		GD.Print("Timer has elapsed.");
	}
}
