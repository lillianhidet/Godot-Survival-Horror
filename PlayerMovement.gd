extends CharacterBody3D


const SPEED = 5.0
const JUMP_VELOCITY = 4.5

var gravity = ProjectSettings.get_setting("physics/3d/default_gravity")
# Get the gravity from the project settings to be synced with RigidBody nodes.
var angular_acc = 7


func _physics_process(delta):
	# Get the input direction and handle the movement/deceleration.
		# Add the gravity.
	if not is_on_floor():
		velocity.y -= gravity * delta

	var h_rot = $MainCamRoot/h.global_transform.basis.get_euler().y
	
	# As good practice, you should replace UI actions with custom gameplay actions.
	var input_dir = Input.get_vector("Left", "Right", "Forward", "Back")
	var direction = Vector3(input_dir.x, 0, input_dir.y).rotated(Vector3.UP, h_rot).normalized()
	
	if direction:
		$PlayerMesh.rotation.y = lerp_angle($PlayerMesh.rotation.y, atan2(direction.x, direction.z), delta * angular_acc)
		velocity.x = direction.x * SPEED
		velocity.z = direction.z * SPEED
	else:
		velocity.x = move_toward(velocity.x, 0, SPEED)
		velocity.z = move_toward(velocity.z, 0, SPEED)

	move_and_slide()
	
	
