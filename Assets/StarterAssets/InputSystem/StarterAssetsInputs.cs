using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool interact;
		public bool throwInput;
		public bool use;
		public bool crouch;
		public bool restart;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM
		public void OnMove(InputAction.CallbackContext context)
		{
			MoveInput(context.ReadValue<Vector2>());

		}

		public void OnLook(InputAction.CallbackContext context)
		{
			if (cursorInputForLook)
			{
				LookInput(context.ReadValue<Vector2>());
			}
		}

		public void OnJump(InputAction.CallbackContext context)
		{
			JumpInput(context.performed);
		}

		public void OnSprint(InputAction.CallbackContext context)
		{
			SprintInput(context.performed);
		}

		public void OnInteract(InputAction.CallbackContext context)
		{
			InteractInput(context.performed);
		}

		public void OnThrow(InputAction.CallbackContext context)
		{
			ThrowInput(context.performed);
		}

		public void OnUse(InputAction.CallbackContext context)
		{
			UseInput(context.performed);
		}

		public void OnCrouch(InputAction.CallbackContext context)
		{
			CrouchInput(context.performed);
		}

		public void OnRestart(InputAction.CallbackContext context)
		{
			RestartInput(context.performed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		}

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void InteractInput(bool newInteractState)
		{
			interact = newInteractState;
		}


		public void ThrowInput(bool newThrowState)
		{
			throwInput = newThrowState;
		}

		public void UseInput(bool newUseState)
		{
			use = newUseState;
		}

		public void CrouchInput(bool newCrouchState)
		{
			crouch = newCrouchState;
		}

		public void RestartInput(bool newRestartState)
		{
			restart = newRestartState;
		}


		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}

}