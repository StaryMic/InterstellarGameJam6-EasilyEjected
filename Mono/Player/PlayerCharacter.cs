using Godot;

namespace IsolationInterstellarGameJam.Mono.Player;

public partial class PlayerCharacter : RigidBody2D
{
    private float _maxSpeed = 2000f;
    private float _thrustSpeed = 10f;

    private Sprite2D _playerSprite;
    private ShapeCast2D _grabRaycast;
    private AudioStreamPlayer2D _thrustAudio;

    private bool _holdingBody = false;

    private PackedScene _astronautBody = ResourceLoader.Load<PackedScene>("res://Mono/Astronaut/AstronautNPC.tscn");

    public float LastFrameSpeed = 0f;

    public override void _Ready()
    {
        _playerSprite = GetNode<Sprite2D>("PlayerCharacter");
        _grabRaycast = GetNode<ShapeCast2D>("PlayerCharacter/ShapeCast2D");
        _thrustAudio = GetNode<AudioStreamPlayer2D>("ThrustLoop");
    }

    public override void _Process(double delta)
    {
        // Update angle
        _playerSprite.LookAt(GetGlobalMousePosition());
        _playerSprite.Rotation += Mathf.DegToRad(90f);
        
        // Handle physics independent inputs
        // Throw is before Grab to fix a bug where you instantly dropped the body after grabbing
        if (Input.IsActionJustPressed("Throw") && _holdingBody && !_grabRaycast.IsColliding())
        {
            Astronaut.AstronautBasic instance = _astronautBody.Instantiate<Astronaut.AstronautBasic>();
            GetTree().Root.GetChild(-1).AddChild(instance);
            
            instance.GlobalPosition = _playerSprite.GlobalPosition + (-_playerSprite.Transform.Y * 100);
            instance.KnockOut();
            instance.ApplyImpulse((-_playerSprite.Transform.Y * 500)+ LinearVelocity);

            _holdingBody = false;
        }
        
        if (Input.IsActionJustPressed("Grab") && !_holdingBody && _grabRaycast.IsColliding())
        {
            if(_grabRaycast.GetCollider(0) is Astronaut.AstronautBasic collidedNPC && !collidedNPC.IsConscious)
            {
                collidedNPC.QueueFree();

                _holdingBody = true;
            }
        }
        
        // Start and end audio with player thrust
        if (Input.IsActionJustPressed("Thrust"))
        {
            _thrustAudio.Play();
        }

        if (Input.IsActionJustReleased("Thrust"))
        {
            _thrustAudio.Stop();
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        // Inputs
        if (Input.IsActionPressed("Thrust"))
        {
            LinearVelocity += (GetGlobalMousePosition() - this.GlobalPosition) * (_thrustSpeed * (float)delta);
        }
        
        // Clamp speed
        LinearVelocity = new Vector2(
            Mathf.Clamp(LinearVelocity.X, -_maxSpeed, _maxSpeed),
            Mathf.Clamp(LinearVelocity.Y, -_maxSpeed, _maxSpeed));
        
        // Dampen speed since Damp Mode is fucked
        LinearVelocity -= LinearVelocity * 0.01f;
        
        // Debuggy shit
        // GD.Print(LinearVelocity.Length());
        
        // Set previous frame speed variable
        LastFrameSpeed = LinearVelocity.Abs().Length();
    }
}