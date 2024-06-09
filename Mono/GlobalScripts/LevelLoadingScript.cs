using Godot;
using System;

public partial class LevelLoadingScript : Node
{
	// Called when the node enters the scene tree for the first time.
	public void ChangeLevel(string levelPath)
	{
		// Delete current level
		GetTree().Root.GetChild(-1).CallDeferred("free");

		Node2D levelToLoad = ResourceLoader.Load<PackedScene>(levelPath).Instantiate<Node2D>();
		GetTree().Root.AddChild(levelToLoad);
	}
}
