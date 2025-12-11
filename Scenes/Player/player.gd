extends CharacterBody2D
class_name Player

@export var move_speed: float = 100.0
@export var push_strength: float = 300.0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	update_treasure_label()
	
	position = SceneManager.player_spawn_position
	Engine.max_fps = 60


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta: float) -> void:
	move_player()
	
	push_blocks()
	
	update_treasure_label()
	
	move_and_slide()


func move_player():
	var move_vector = Input.get_vector("move_left", "move_right", "move_up","move_down")
	
	velocity = move_vector * move_speed
	
	if velocity.y > 0:
		$AnimatedSprite2D.play("move_down")
		$InteractArea2D.position = Vector2(0, 8)
	elif velocity.y < 0:
		$AnimatedSprite2D.play("move_up")
		$InteractArea2D.position = Vector2(0, -4)
	elif velocity.x > 0:
		$AnimatedSprite2D.play("move_right")
		$InteractArea2D.position = Vector2(5, 2)
	elif velocity.x < 0:
		$AnimatedSprite2D.play("move_left")
		$InteractArea2D.position = Vector2(-5, 2)
	else:
		$AnimatedSprite2D.stop()


func push_blocks():
	# Get the last collision
	# Check if it's a block and if it's a block then push it
	var collision: KinematicCollision2D = get_last_slide_collision()
	if collision:
		var collider_node = collision.get_collider()
			
		if collider_node.is_in_group("pushable"):
			var collision_normal: Vector2 = collision.get_normal()
			collider_node.apply_central_force(collision_normal * -1 * push_strength)


func _on_area_2d_body_exited(body: Node2D) -> void:
	if body.is_in_group("interactable"):
		body.can_interact = false


func _on_area_2d_body_entered(body: Node2D) -> void:
	if body.is_in_group("interactable"):
		body.can_interact = true

func update_treasure_label():
	$%TreasureLabel.text = str(SceneManager.opened_chests.size())
