extends CharacterBody2D

var target: Node2D
@export var speed: int = 30 
@export var acceleration: float = 15
@export var hp: int = 2

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _physics_process(delta: float) -> void:
	if hp <= 0:
		return
		
	chase_target()
	
	animate_enemy()
	
	move_and_slide()


func chase_target():
	if target:
		var distance_to_target = target.global_position - global_position
		var direction_normal: Vector2 = distance_to_target.normalized()
		
		velocity = velocity.move_toward(direction_normal * speed, acceleration)


func animate_enemy():
	var direction_normal: Vector2 = velocity.normalized()
	
	if direction_normal.y > 0.707:
		$AnimatedSprite2D.play("move_down")
	elif direction_normal.y < -0.707:
		$AnimatedSprite2D.play("move_up")
	elif direction_normal.x > 0.707:
		$AnimatedSprite2D.play("move_right")
	elif direction_normal.x < -0.707:
		$AnimatedSprite2D.play("move_left")
	else:
		$AnimatedSprite2D.stop()


func _on_player_detect_area_2d_body_entered(body: Node2D) -> void:
	if body is Player:
		target = body


func take_hit(damage: int, knockback: Vector2):
	$DamageSFX.play()
	
	var flash_white_color: Color = Color(10, .5, .5)
	modulate = flash_white_color
	
	await get_tree().create_timer(0.2).timeout
	
	var original_color: Color = Color(1, 1, 1)
	modulate = original_color
	
	velocity += knockback
	
	hp -= damage
	if hp <= 0:
		die()


func die():
	$GPUParticles2D.emitting = true
	$AnimatedSprite2D.visible = false
	$CollisionShape2D.set_deferred("disabled", true)
	
	await get_tree().create_timer(1).timeout
	
	queue_free()
