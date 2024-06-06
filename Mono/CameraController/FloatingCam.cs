using Godot;
using System;
[GlobalClass]
public partial class FloatingCam : Camera2D
{
    [Export]public Node2D TargetObject;

    public override void _Ready()
    {
        if (TargetObject == null)
        {
            TargetObject = (Node2D)GetTree().Root.GetChild(0);
        }
    }

    public override void _Process(double delta)
    {
        this.GlobalPosition = TargetObject.GlobalPosition;
    }
}
