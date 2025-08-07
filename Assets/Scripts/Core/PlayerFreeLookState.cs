using UnityEngine;

namespace ThirdPersonTraversalandCombat.Core.StateMachine
{
    public class PlayerFreeLookState : PlayerBaseState
    {
        private readonly int FreeLookSpeedHash = Animator.StringToHash("FreeLookSpeed");
        private readonly int FreeLookBlendTreeHash = Animator.StringToHash("FreeLookBlendTree");

        public PlayerFreeLookState(PlayerStateMachine stateMachine) : base(stateMachine) { }

        public override void Enter()
        {
            stateMachine.InputReader.OnTargetEvent += InputReader_OnTargetEvent;

            stateMachine.Animator.Play(FreeLookBlendTreeHash);
        }

        public override void Tick(float deltaTime)
        {
            Vector3 movement = CalculateMovement();

            stateMachine.CharacterController.Move(movement * stateMachine.FreeLookMovementSpeed * Time.deltaTime);

            if (movement != Vector3.zero)
            {
                stateMachine.Animator.SetFloat(FreeLookSpeedHash, 1f, .1f, deltaTime);

                FaceMovementDirection(movement, deltaTime);
            }
            else
            {
                stateMachine.Animator.SetFloat(FreeLookSpeedHash, 0f, .1f, deltaTime);
            }
        }

        public override void Exit()
        {
            stateMachine.InputReader.OnTargetEvent -= InputReader_OnTargetEvent;
        }

        private Vector3 CalculateMovement()
        {
            Vector3 cameraForward = stateMachine.MainCameraTransform.forward;
            Vector3 cameraRight = stateMachine.MainCameraTransform.right;
            cameraForward.y = 0;
            cameraRight.y = 0;
            cameraForward.Normalize();
            cameraRight.Normalize();

            Vector3 movement = cameraForward * stateMachine.InputReader.MovementValue.y
                + cameraRight * stateMachine.InputReader.MovementValue.x;

            return movement;
        }

        private void FaceMovementDirection(Vector3 movement, float deltaTime)
        {
            stateMachine.transform.rotation = Quaternion.Lerp(
                stateMachine.transform.rotation,
                Quaternion.LookRotation(movement),
                deltaTime * stateMachine.RotationDamping);
        }

        private void InputReader_OnTargetEvent()
        {
            if (stateMachine.Targeter.SelectTarget())

            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
    }
}
