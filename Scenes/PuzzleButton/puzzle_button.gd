extends Area2D

var bodies_on_top: int = 0
#signal pressed
#signal unpressed


func _on_body_entered(body: Node2D) -> void:
	if body.is_in_group("pushable") or body is CharacterBody2D:
		bodies_on_top += 1
		$AnimatedSprite2D.play("pressed")
		$AudioStreamPlayer2D.pitch_scale = 1.0
		$AudioStreamPlayer2D.play()
		#pressed.emit()


func _on_body_exited(body: Node2D) -> void:
	if body.is_in_group("pushable") or body is CharacterBody2D:
		bodies_on_top -= 1
		if bodies_on_top == 0:
			$AudioStreamPlayer2D.pitch_scale = 0.8
			$AudioStreamPlayer2D.play()
			$AnimatedSprite2D.play("unpressed")
			#unpressed.emit()
