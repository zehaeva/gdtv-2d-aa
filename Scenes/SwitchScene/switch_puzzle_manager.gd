extends Marker2D

signal puzzle_solved
signal puzzle_failed

@export var target_score: int = 0
var score: int = 0


func increase_score():
	score += 1
	
	if score >= target_score:
		puzzle_solved.emit()
	else:
		puzzle_failed.emit()


func decrease_score():
	score -= 1
	
	if score < target_score:
		puzzle_failed.emit()
