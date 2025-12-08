extends CharacterBody2D

@export var move_speed: float = 100.0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var score: int = 200
	var money: float = 15.5
	var username: String = "Bob"
	var is_player_alive: bool = true
	var first_name: String = "Howard"
	var damage: float = 7.5
	
	print("first_name: ", first_name, " damage: ", damage)


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
