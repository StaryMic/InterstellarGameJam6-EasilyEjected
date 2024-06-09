using Godot;
using System;
using IsolationInterstellarGameJam.Mono.Astronaut;
using IsolationInterstellarGameJam.Mono.GlobalScripts;
using IsolationInterstellarGameJam.Mono.Player;

public partial class SecurityCam : Sprite2D
{
    private Area2D _detectionArea;
    private GameStateSignals _gameStateSignals;
    private JuiceEffects _juice;

    private bool hasSeen = false;

    public override void _Ready()
    {
        // Set node references
        _detectionArea = GetNode<Area2D>("Area2D");
        _gameStateSignals = GetNode<GameStateSignals>("/root/GameStateSignals");
        _juice = GetTree().Root.GetChild(-1).GetNode<JuiceEffects>("JuiceEffects");
        
        // Connect event
        _detectionArea.BodyEntered += DetectionAreaOnBodyEntered;
    }

    private void DetectionAreaOnBodyEntered(Node2D body)
    {
        if (hasSeen)
            return;
        
        if (body is AstronautBasic)
        {
            _gameStateSignals.EmitSignal(GameStateSignals.SignalName.AlarmSetOff);
            GD.Print("I see an astronaut");
            _juice.VignetteImpact();
            _juice.BulletTime(1);
        }
        if (body is PlayerCharacter)
        {
            _gameStateSignals.EmitSignal(GameStateSignals.SignalName.AlarmSetOff);
            GD.Print("I see the player");
            
            _juice.VignetteImpact();
            _juice.BulletTime(1);
        }
    }
}
