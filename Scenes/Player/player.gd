extends CharacterBody2D
class_name Player

@export var move_speed: float = 100.0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	position = SceneManager.player_spawn_position


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	var move_vector = Input.get_vector("move_left", "move_right", "move_up","move_down")
	
	velocity = move_vector * move_speed
	
	if velocity.y > 0:
		$AnimatedSprite2D.play("move_down")
	elif velocity.y < 0:
		$AnimatedSprite2D.play("move_up")
	elif velocity.x > 0:
		$AnimatedSprite2D.play("move_right")
	elif velocity.x < 0:
		$AnimatedSprite2D.play("move_left")
	else:
		$AnimatedSprite2D.stop()
		
	move_and_slide()
