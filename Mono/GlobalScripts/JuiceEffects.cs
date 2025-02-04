using Godot;
using IsolationInterstellarGameJam.Mono.HUD;

namespace IsolationInterstellarGameJam.Mono.GlobalScripts;

[GlobalClass]
public partial class JuiceEffects : Node
{
	// Node References
	private Player.PlayerCharacter _player;
	private HeadsUpDisplay _hud;
	private GameStateSignals _gameStateSignals;
	
	public override void _Ready()
	{
		_player = GetTree().Root.GetChild(-1).GetNode<Player.PlayerCharacter>("Player");
		_hud = GetTree().Root.GetChild(-1).GetNode<HeadsUpDisplay>("HUD/HeadsUpDisplay");
		_gameStateSignals = GetNode<GameStateSignals>("/root/GameStateSignals");
	}
	
	
	private float _bulletTimeLeft;
	public void BulletTime(float time, float timeScale = 0.5f)
	{
		_bulletTimeLeft = time;
		Engine.TimeScale = timeScale;
	}

	public void VignetteImpact()
	{
		AnimationPlayer _vignetteAnimationPlayer = _hud.GetNode<AnimationPlayer>("AnimationPlayer");
		_vignetteAnimationPlayer.Play("VignetteImpact");
		_vignetteAnimationPlayer.AnimationFinished +=
			name =>
			{
				_gameStateSignals.EmitSignal(GameStateSignals.SignalName.AlarmSetOff);
				GD.Print("Animation is over and alarm should be set off");
				_vignetteAnimationPlayer.Play("VignetteAlarmTransition");
			};
	}

	public void FadeToBlack()
	{
		AnimationPlayer _vignetteAnimationPlayer = _hud.GetNode<AnimationPlayer>("AnimationPlayer");
		_vignetteAnimationPlayer.Play("EndScreenFadeIn");
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