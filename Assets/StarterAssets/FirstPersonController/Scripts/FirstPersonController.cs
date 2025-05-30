﻿using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	[RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM
	[RequireComponent(typeof(PlayerInput))]
#endif
	public class FirstPersonController : MonoBehaviour
	{
		[Header("Player")]
		[Tooltip("Move speed of the character in m/s")]
		public float MoveSpeed = 4.0f;
		[Tooltip("Sprint speed of the character in m/s")]
		public float SprintSpeed = 7.0f;
		[Tooltip("Crouch speed of the character in m/s")]
		public float CrouchSpeed = 2.5f;
		[Tooltip("Rotation speed of the character")]
		public float RotationSpeed = 1.0f;
		[Tooltip("Acceleration and deceleration")]
		public float SpeedChangeRate = 10.0f;

		[Space(10)]
		[Tooltip("The height the player can jump")]
		public float JumpHeight = 1.2f;
		[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
		public float Gravity = -15.0f;

		[Space(10)]
		[Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
		public float JumpTimeout = 0.1f;
		[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
		public float FallTimeout = 0.15f;

		[Header("Player Grounded")]
		[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
		public bool Grounded = true;
		[Tooltip("Useful for rough ground")]
		public float GroundedOffset = -0.14f;
		[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
		public float GroundedRadius = 0.5f;
		[Tooltip("What layers the character uses as ground")]
		public LayerMask GroundLayers;

		[Header("Cinemachine")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		public GameObject CinemachineCameraTarget;
		[Tooltip("How far in degrees can you move the camera up")]
		public float TopClamp = 90.0f;
		[Tooltip("How far in degrees can you move the camera down")]
		public float BottomClamp = -90.0f;
		[Tooltip("Original viewPos")]
		public Vector3 originViewPos = new Vector3(0f, 1.375f, 0f);
		[Tooltip("Crouch viewPos")]
		public Vector3 crouchViewPos = new Vector3(0f, 0.7f, 0f);

		// cinemachine
		private float _cinemachineTargetPitch;

		// player
		private float _speed;
		private float _rotationVelocity;
		private float _verticalVelocity;
		private float _terminalVelocity = 53.0f;

		// timeout deltatime
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;

		private int propLayer;
		private LayerMask originalGroundLayers;

		private int originalLayer;

		//Crouch
		private CapsuleCollider playerCol;

		//collider
		private float originalHeight;
		private Vector3 originalCenter;
		//characterController
		private float originalCCHeight;
		private Vector3 originalCCCenter;

		private float crouchHeight = 1.0f; // 앉을 때의 높이
		private Vector3 crouchCenterOffset = new Vector3(0, 0.5f, 0); // 앉을 때의 중심 이동



#if ENABLE_INPUT_SYSTEM
		private PlayerInput _playerInput;
#endif
		private CharacterController _controller;
		private StarterAssetsInputs _input;
		private GameObject _mainCamera;
		private RayInteract rayInt;

		private const float _threshold = 0.01f;

		private bool IsCurrentDeviceMouse
		{
			get
			{
#if ENABLE_INPUT_SYSTEM
				return _playerInput.currentControlScheme == "KeyboardMouse";
#else
				return false;
#endif
			}
		}

		private void Awake()
		{
			// get a reference to our main camera
			if (_mainCamera == null)
			{
				_mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
			}

			// 저장 원본 LayerMask
			originalGroundLayers = GroundLayers;

			// Get the layer index for "Prop"
			propLayer = LayerMask.NameToLayer("Prop");
		}

		private void Start()
		{
			_controller = GetComponent<CharacterController>();
			_input = GetComponent<StarterAssetsInputs>();

			playerCol = GetComponentInChildren<CapsuleCollider>();
			originalHeight = playerCol.height;
			originalCenter = playerCol.center;

			originalCCHeight = _controller.height;
			originalCCCenter = _controller.center;

#if ENABLE_INPUT_SYSTEM
			_playerInput = GetComponent<PlayerInput>();
#else
			Debug.LogError( "Starter Assets package is missing dependencies. Please use Tools/Starter Assets/Reinstall Dependencies to fix it");
#endif

			// reset our timeouts on start
			_jumpTimeoutDelta = JumpTimeout;
			_fallTimeoutDelta = FallTimeout;
			rayInt = GetComponent<RayInteract>();
		}

		private void Update()
		{
			JumpAndGravity();
			GroundedCheck();
			Move();
			Crouch();
		}

		private void LateUpdate()
		{
			CameraRotation();
			CrouchCameraPos();
		}
		private void GroundedCheck()
		{
			// Set sphere position, with offset
			Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);

			// Check if player is holding a prop
			if (rayInt.holdingProp != null)
			{
				// Store the original layer of the holding prop
				originalLayer = rayInt.holdingProp.layer;

				// Temporarily change the layer of the holding prop to ignore ground check
				rayInt.holdingProp.layer = LayerMask.NameToLayer("Ignore Raycast");

				// Perform the ground check
				Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);

				// Restore the original layer of the holding prop
				rayInt.holdingProp.layer = originalLayer;
			}
			else
			{
				// Perform the ground check
				Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
			}
		}
		/*
        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);

            // Check if player is holding a prop
            if (rayInt.holdingProp != null)
            {
                // Create a LayerMask that excludes the Prop layer
                LayerMask groundCheckMask = originalGroundLayers & ~(1 << propLayer);
                Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, groundCheckMask, QueryTriggerInteraction.Ignore);
            }
            else
            {
                Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, originalGroundLayers, QueryTriggerInteraction.Ignore);
            }

        }
		*/

		private void CameraRotation()
		{
			if (Time.timeScale == 0f)
			{
				return;
			}
			else
			{
				// if there is an input
				if (_input.look.sqrMagnitude >= _threshold)
				{
					//Don't multiply mouse input by Time.deltaTime
					float deltaTimeMultiplier = IsCurrentDeviceMouse ? 1.4f : Time.deltaTime;

					_cinemachineTargetPitch += _input.look.y * RotationSpeed * deltaTimeMultiplier;
					_rotationVelocity = _input.look.x * RotationSpeed * deltaTimeMultiplier;

					// clamp our pitch rotation
					_cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

					// Update Cinemachine camera target pitch
					CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

					// rotate the player left and right
					transform.Rotate(Vector3.up * _rotationVelocity);
				}

			}
		}


		private void CrouchCameraPos()
		{
			// Determine the target position based on crouch state
			Vector3 targetPosition = _input.crouch ? crouchViewPos : originViewPos;

			// Smoothly interpolate to the target position
			CinemachineCameraTarget.transform.localPosition = Vector3.Lerp(CinemachineCameraTarget.transform.localPosition, targetPosition, Time.deltaTime * 10f);
		}




		private void Move()
		{
			float targetSpeed = MoveSpeed;
			if (_input.sprint)
			{
				targetSpeed = SprintSpeed;
			}
			else if (_input.crouch)
			{
				targetSpeed = CrouchSpeed;
			}


			// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

			// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is no input, set the target speed to 0
			if (_input.move == Vector2.zero) targetSpeed = 0.0f;

			// a reference to the players current horizontal velocity
			float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

			float speedOffset = 0.1f;
			float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

			// accelerate or decelerate to target speed
			if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
			{
				// creates curved result rather than a linear one giving a more organic speed change
				// note T in Lerp is clamped, so we don't need to clamp our speed
				_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

				// round speed to 3 decimal places
				_speed = Mathf.Round(_speed * 1000f) / 1000f;
			}
			else
			{
				_speed = targetSpeed;
			}

			// normalise input direction
			Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

			// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
			// if there is a move input rotate player when the player is moving
			if (_input.move != Vector2.zero)
			{
				// move
				inputDirection = transform.right * _input.move.x + transform.forward * _input.move.y;
			}

			// move the player
			_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		}

		private void JumpAndGravity()
		{
			if (Grounded)
			{
				// reset the fall timeout timer
				_fallTimeoutDelta = FallTimeout;

				// stop our velocity dropping infinitely when grounded
				if (_verticalVelocity < 0.0f)
				{
					_verticalVelocity = -2f;
				}

				// Jump
				if (_input.jump && _jumpTimeoutDelta <= 0.0f)
				{
					// the square root of H * -2 * G = how much velocity needed to reach desired height
					_verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);
				}

				// jump timeout
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				// reset the jump timeout timer
				_jumpTimeoutDelta = JumpTimeout;

				// fall timeout
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}

				// if we are not grounded, do not jump
				_input.jump = false;
			}

			// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
			if (_verticalVelocity < _terminalVelocity)
			{
				_verticalVelocity += Gravity * Time.deltaTime;
			}
		}

		private void Crouch()
		{
			if (_input.crouch)
			{
				// 콜라이더의 높이를 줄이고 중심을 아래로 이동
				playerCol.height = crouchHeight;
				playerCol.center = originalCenter - crouchCenterOffset;

				_controller.height = crouchHeight;
				_controller.center = originalCCCenter - crouchCenterOffset;

			}
			else
			{
				// 콜라이더의 높이와 중심을 원래대로 되돌림
				playerCol.height = originalHeight;
				playerCol.center = originalCenter;

				_controller.height = originalCCHeight;
				_controller.center = originalCCCenter;
			}
		}

		private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return Mathf.Clamp(lfAngle, lfMin, lfMax);
		}

		private void OnDrawGizmosSelected()
		{
			Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
			Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

			if (Grounded) Gizmos.color = transparentGreen;
			else Gizmos.color = transparentRed;

			// when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
			Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
		}
	}
}