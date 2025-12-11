extends StaticBody2D

var can_interact: bool = false
var is_activated: bool = false

signal switch_activated
signal switch_deactivated

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("interact") && can_interact:
		print("switched!")
		if is_activated:
			deactivate_swtich()
		else:
			activate_swtich()


func _on_switch_activated() -> void:
	print("the switch is activated by signal")


func _on_switch_deactivated() -> void:
	print("the switch is DEactivated by signal")


func activate_swtich():
	$AnimatedSprite2D.play("activated")
	is_activated = true
	switch_activated.emit()
	

func deactivate_swtich():
	$AnimatedSprite2D.play("deactivated")
	is_activated = false
	switch_deactivated.emit()
	
