extends StaticBody2D

var can_interact: bool = false
@export var npc_name: String = ""
@export var dialogue_lines: Array[String] = []
var dialogue_index = 0
@export var hp: int = 10

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	$CanvasLayer/NameLabel.text = npc_name


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if Input.is_action_just_pressed("Interact") && can_interact:
		$AudioStreamPlayer2D.play()
		if dialogue_index < dialogue_lines.size():
			$CanvasLayer.visible = true
			$CanvasLayer/DialogueLabel.text = dialogue_lines[dialogue_index]
			dialogue_index += 1
			# get_tree().paused = $CanvasLayer.visible
		else: 
			dialogue_index = 0
			$CanvasLayer.visible = false
	
	if !can_interact:
		$CanvasLayer.visible = false
		dialogue_index = 0
