extends Node3D

var camrot_h = 0
var camrot_v = 0
var campan_h = 0
var campan_v = 0

var sens = 0.1

var t = 0.0
var aim_cam_pos
var aim_cam
var main_cam
var main_cam_pos

var aiming : bool = false
var moving : bool = false

#TODO: 
# Maintain aim position as always behind the player (DONE)
# Aiming mode camera movement (DONE)
# Restrict movement when in aiming mode (DONE)
# Reset Camera positions when leaving their mode



# Called when the node enters the scene tree for the first time.
func _ready():
	aim_cam_pos = $"%aimCamPos"
	aim_cam = $"%aimCamPos/aimcam"
	main_cam = $"h/v/mainCamPos/cam"
	main_cam_pos = $"h/v/mainCamPos"
	
	main_cam.current = true
	
	Input.mouse_mode = Input.MOUSE_MODE_CAPTURED


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta):
	if (Input.is_action_just_pressed("Right Click")):
		moving = true
	
func _input(event):
	if(event is InputEventMouseMotion):
		if(!aiming):
			camrot_h += -event.relative.x * sens
			camrot_v += -event.relative.y * sens
		else:
			campan_h += -event.relative.x * sens
			campan_v += -event.relative.y * sens
			
			campan_h = clampf(campan_h,-3,3)
			campan_v = clampf(campan_v, 0, 1)
	
func _physics_process(delta):
	
	moveCam()
		
	if(moving):
		transitionHandler(delta)
		
func moveCam():
	if(!aiming):
		$h.rotation_degrees.y = camrot_h
		$h/v.rotation_degrees.x = camrot_v
	else:
		
		$%AimCamRoot/h.transform.origin.x = campan_h
		$%AimCamRoot/h/v.transform.origin.y = campan_v
		print(campan_v)

func transitionHandler(delta):
	if(aiming):
		transitionCam(aim_cam, main_cam, aim_cam_pos, main_cam_pos, delta)
	else:
		transitionCam(main_cam, aim_cam, main_cam_pos, aim_cam_pos, delta)
	
func transitionCam(movingCam, otherCam, origin, destination, d):
	if(t <= 1):
		t	+= d * 0.4
		#Move camera to other cameras position
		movingCam.global_transform = origin.global_transform.interpolate_with(destination.global_transform, t)
	else:
		t = 0
		aiming = !aiming
		moving = false
		movingCam.current = false
		otherCam.current = true
		movingCam.global_transform = origin.global_transform
	
	
	
