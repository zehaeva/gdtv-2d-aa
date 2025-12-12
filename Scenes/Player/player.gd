extends CharacterBody2D
class_name Player

@export var move_speed: float = 100.0
@export var push_strength: float = 300.0
@export var acceleration: float = 15.0
@export var hp: int = 3
var can_attack: bool = true
var is_attacking: bool = false
var attack_hit: bool = false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	update_treasure_label()
	
	update_hp_bar()
	
	position = SceneManager.player_spawn_position
	Engine.max_fps = 60


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta: float) -> void:
	if SceneManager.player_hp <= 0:
		return 
	
	if !is_attacking:
		move_player()
	
	push_blocks()
	
	update_treasure_label()
	
	move_and_slide()
	
	if Input.is_action_just_pressed("interact") && can_attack:
		attack()


func move_player():
	var move_vector = Input.get_vector("move_left", "move_right", "move_up","move_down")
	
	velocity = velocity.move_toward(move_vector * move_speed, acceleration)
	
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
		can_attack = true


func _on_area_2d_body_entered(body: Node2D) -> void:
	if body.is_in_group("interactable"):
		body.can_interact = true
		can_attack = false


func update_treasure_label():
	$%TreasureLabel.text = str(SceneManager.opened_chests.size())


func _on_hitbox_area_2d_body_entered(body: Node2D) -> void:
	$DamageSFX.play()
	
	SceneManager.player_hp -= 1
	update_hp_bar()
	if SceneManager.player_hp <= 0:
		die()

	var distance_to_enemy: Vector2 = global_position - body.global_position
	var distance_normalized: Vector2 = distance_to_enemy.normalized()
	
	var knockback_strength: float = 150.0
	
	velocity += distance_normalized * knockback_strength
	
	var flash_white_color: Color = Color(50, 50, 50)
	modulate = flash_white_color
	
	await get_tree().create_timer(0.2).timeout
	
	var original_color: Color = Color(1, 1, 1)
	modulate = original_color


func die():
	if !$DeathTimer.is_stopped():
		return 
		
	$AnimatedSprite2D.play("death")
	
	$DeathTimer.start()


func update_hp_bar():
	match SceneManager.player_hp:
		3:
			$%HPBar.play("3_hp")
		2:
			$%HPBar.play("2_hp")
		1:
			$%HPBar.play("1_hp")
		_:
			$%HPBar.play("0_hp")


func attack():
	if !$AttackDurationTimer.is_stopped():
		return 
		
	is_attacking = true
	$Sword.visible = true
	$Sword/SwordArea2D.monitoring = true
	$Sword/SwordSFX.play()
	$AttackDurationTimer.start()
	
	velocity = Vector2(0, 0)
	
	match $AnimatedSprite2D.animation:
		"move_up":
			$AnimatedSprite2D.play("attack_up")
			$AnimationPlayer.play("attack_sword_up")
		"move_right":
			$AnimatedSprite2D.play("attack_right")
			$AnimationPlayer.play("attack_sword_right")
		"move_down":
			$AnimatedSprite2D.play("attack_down")
			$AnimationPlayer.play("attack_sword_down")
		"move_left":
			$AnimatedSprite2D.play("attack_left")
			$AnimationPlayer.play("attack_sword_left")


func _on_sword_area_2d_body_entered(body: Node2D) -> void:
	if attack_hit:
		return
	
	attack_hit = true
	var distance_to_enemy: Vector2 = body.global_position - global_position
	var knockback_direction: Vector2 = distance_to_enemy.normalized()
	
	var knockback_strength: float = 150
	
	body.take_hit(1, knockback_direction * knockback_strength)


func _on_attack_duration_timer_timeout() -> void:
	is_attacking = false
	attack_hit = false
	$Sword.visible = false
	$Sword/SwordArea2D.monitoring = false
	
	$AnimationPlayer.play("RESET")
	
	match $AnimatedSprite2D.animation:
		"attack_up":
			$AnimatedSprite2D.play("move_up")
		"attack_right":
			$AnimatedSprite2D.play("move_right")
		"attack_down":
			$AnimatedSprite2D.play("move_down")
		"attack_left":
			$AnimatedSprite2D.play("move_left")


func reset_scene():
	SceneManager.player_hp = hp
	get_tree().call_deferred("reload_current_scene")
