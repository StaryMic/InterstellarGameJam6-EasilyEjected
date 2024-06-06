using Godot;
using System;

public partial class JuiceEffects : Node
{
	private float _bulletTimeLeft;
	public void BulletTime(float time, float timeScale = 0.5f)
	{
		_bulletTimeLeft = time;
		Engine.TimeScale = timeScale;
	}

	public void VignetteImpact(float time, float intensity)
	{
		
	}

	public override void _Ready()
	{
		PrintTree();
	}

	public override void _Process(double delta)
	{
		_bulletTimeLeft -= (float)delta;
		if (_bulletTimeLeft <= 0)
		{
			Engine.TimeScale = 1.0f;
		}
	}
}
