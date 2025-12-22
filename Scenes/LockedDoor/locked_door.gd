extends StaticBody2D

var buttons_pressed: int = 0
@export var buttons_needed: int = 1


func _on_puzzle_button_pressed() -> void:
	buttons_pressed += 1
	
	if buttons_needed == buttons_pressed:
		visible = false
		$CollisionShape2D.set_deferred("disabled", true)


func _on_puzzle_button_unpressed() -> void:
	buttons_pressed -= 1
	
	if buttons_needed != buttons_pressed:
		visible = true
		$CollisionShape2D.set_deferred("disabled", false)
