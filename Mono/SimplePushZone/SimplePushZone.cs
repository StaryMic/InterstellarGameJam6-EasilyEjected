using Godot;
using System;
using Godot.Collections;

public partial class SimplePushZone : Area2D
{
	[Export] private Vector2 _pushDirection;
	private Array<RigidBody2D> _pushList = new Array<RigidBody2D>();
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}

	public override void _PhysicsProcess(double delta)
	{
		foreach (RigidBody2D body in _pushList)
		{
			body.ApplyForce(_pushDirection);
		}
	}

	private void OnBodyExited(Node2D body)
	{
		if (body is RigidBody2D rigidBody2D)
		{
			_pushList.Remove(rigidBody2D);
		}
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is RigidBody2D rigidBody2D)
		{
			_pushList.Add(rigidBody2D);
		}
	}
}
