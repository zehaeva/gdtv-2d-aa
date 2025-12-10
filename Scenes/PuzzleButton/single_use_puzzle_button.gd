extends "res://Scenes/PuzzleButton/puzzle_button.gd"


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

# we never unpress a single use button
func _on_body_exited(body: Node2D) -> void:
	pass
