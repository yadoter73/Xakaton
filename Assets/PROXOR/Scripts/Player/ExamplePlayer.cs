using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KinematicCharacterController;
using KinematicCharacterController.Examples;

namespace KinematicCharacterController.Examples
{
    public class ExamplePlayer : MonoBehaviour
    {
        public ExampleCharacterController Character;
        private const string HorizontalInput = "Horizontal";
        private const string VerticalInput = "Vertical";

        private Transform _cam;
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            _cam = Camera.main.transform;
        }
        private void Update()
        {
            HandleCharacterInput();
        }
        private void HandleCharacterInput()
        {
            PlayerCharacterInputs characterInputs = new PlayerCharacterInputs();

            characterInputs.MoveAxisForward = Input.GetAxisRaw(VerticalInput);
            characterInputs.MoveAxisRight = Input.GetAxisRaw(HorizontalInput);
            characterInputs.CameraRotation = _cam.rotation;
            characterInputs.JumpDown = Input.GetKeyDown(KeyCode.Space);
            characterInputs.CrouchDown = Input.GetKeyDown(KeyCode.C);
            characterInputs.CrouchUp = Input.GetKeyUp(KeyCode.C);
            characterInputs.SprintHeld = Input.GetKey(KeyCode.LeftShift);
            Character.SetInputs(ref characterInputs);
        }
    }
}