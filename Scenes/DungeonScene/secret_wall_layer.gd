extends TileMapLayer


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func disable_secret_wall():
	visible = false
	collision_enabled = false


func enable_secret_wall():
	visible = true
	collision_enabled = true
