extends StaticBody2D

var can_interact: bool = false
var is_open: bool = false

@export var chest_name: String

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	if SceneManager.opened_chests.has(chest_name):
		is_open = true
		$AnimatedSprite2D.play("open")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("interact") && can_interact:
		if !is_open:
			open_chest()


func open_chest():
	if !is_open:
		is_open = true
		$AnimatedSprite2D.play("open")
		$ScrollEmpty.visible = true
		$Timer.start()
		SceneManager.opened_chests.append(chest_name)


func _on_timer_timeout() -> void:
	$ScrollEmpty.visible = false
