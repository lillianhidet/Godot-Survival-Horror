extends Camera3D


var rotationSpeed : float = 0.01

func _ready():
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED


# Called every frame. 'delta' is the elapsed time since the previous frame.
#func _process(delta):
	#look_at(get_parent().position)

func _input(event):
	if(event) is InputEventMouseMotion:
		get_parent().rotate_y(event.relative.x * rotationSpeed)
