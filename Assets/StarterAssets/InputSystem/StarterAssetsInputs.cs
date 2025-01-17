using UnityEngine;
using CnControls;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")] public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool Throw;
		public bool SitBike;

		[Header("Movement Settings")] public bool analogMovement;

		[Header("Mouse Cursor Settings")] public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		public float cameraHorizontalInput;
		public float cameraVerticalInput;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if (cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void Throwinput(InputValue value)
		{
			ThrowInput(value.isPressed);
		}
		public void BikeSitinput(InputValue value)
		{
			SitBikeInput(value.isPressed);
		}
		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
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

		public void ThrowInput(bool throwBool)
		{
			Throw = throwBool;
		}
		public void SitBikeInput(bool Sit)
		{
			SitBike = Sit;
		}
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

		private void Update()
		{
			// Capture touchpad or joystick input
			cameraHorizontalInput = CnInputManager.GetAxis("CameraHorizontal");
			cameraVerticalInput = CnInputManager.GetAxis("CameraVertical");
		}
	}
}