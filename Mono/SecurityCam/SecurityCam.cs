using Godot;
using System;
using IsolationInterstellarGameJam.Mono.Astronaut;
using IsolationInterstellarGameJam.Mono.GlobalScripts;
using IsolationInterstellarGameJam.Mono.Player;

public partial class SecurityCam : Sprite2D
{
    private Area2D _detectionArea;
    private GameStateSignals _gameStateSignals;

    public override void _Ready()
    {
        // Set node references
        _detectionArea = GetNode<Area2D>("Area2D");
        _gameStateSignals = GetNode<GameStateSignals>("/root/GameStateSignals");
        
        // Connect event
        _detectionArea.BodyEntered += DetectionAreaOnBodyEntered;
    }

    private void DetectionAreaOnBodyEntered(Node2D body)
    {
        if (body is AstronautBasic)
            _gameStateSignals.EmitSignal(GameStateSignals.SignalName.AlarmSetOff);

        if (body is PlayerCharacter)
            _gameStateSignals.EmitSignal(GameStateSignals.SignalName.AlarmSetOff);
    }
}
