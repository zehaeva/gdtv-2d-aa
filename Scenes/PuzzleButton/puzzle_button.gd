extends Area2D

var bodies_on_top: int = 0
signal pressed
signal unpressed

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass


func _on_body_entered(body: Node2D) -> void:
	if body.is_in_group("pushable") or body is Player:
		bodies_on_top += 1
		$AnimatedSprite2D.play("pressed")
		$AudioStreamPlayer2D.pitch_scale = 1.0
		$AudioStreamPlayer2D.play()
		pressed.emit()


func _on_body_exited(body: Node2D) -> void:
	if body.is_in_group("pushable") or body is Player:
		bodies_on_top -= 1
		if bodies_on_top == 0:
			$AudioStreamPlayer2D.pitch_scale = 0.8
			$AudioStreamPlayer2D.play()
			$AnimatedSprite2D.play("unpressed")
			unpressed.emit()
