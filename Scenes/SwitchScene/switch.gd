extends StaticBody2D

var can_interact: bool = false
var is_activated: bool = false

signal switch_activated
signal switch_deactivated


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("Interact") && can_interact:
		$AudioStreamPlayer2D.play()
		if is_activated:
			deactivate_swtich()
		else:
			activate_swtich()


func activate_swtich():
	$AnimatedSprite2D.play("activated")
	is_activated = true
	switch_activated.emit()
	

func deactivate_swtich():
	$AnimatedSprite2D.play("deactivated")
	is_activated = false
	switch_deactivated.emit()
	
