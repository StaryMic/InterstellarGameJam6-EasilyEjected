extends Area2D


# Called when the node enters the scene tree for the first time.
func _ready():
	body_entered.connect(change_level)

func change_level(_goawaybodyrefewwwww):
	LevelLoadingScript.call_deferred("ChangeLevel","res://Maps/TestScene2.tscn")
	#LevelLoadingScript.ChangeLevel("res://Maps/TestScene2.tscn")
