using System;
using Godot;

namespace IsolationInterstellarGameJam.Mono.SimpleSignalIO;

[GlobalClass]
public partial class Transmitter : Node
{
	[ExportCategory("Source")]
	[Export] private Node _sourceNode;
	[Export] private String _sourceSignal;
	
	[ExportCategory("Target")]
	[Export] private Node _targetNode;
	[Export] private String _targetSignal;


	public override void _Ready()
	{
		_sourceNode.Connect(_sourceSignal, Callable.From(Trigger));
	}

	public void Trigger()
	{
		_targetNode.EmitSignal(_targetSignal);
	}
}