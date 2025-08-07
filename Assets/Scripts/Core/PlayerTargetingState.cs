using UnityEngine;

namespace ThirdPersonTraversalandCombat.Core.StateMachine
{
    public class PlayerTargetingState : PlayerBaseState
    {
        private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

        public override void Enter()
        {
            stateMachine.InputReader.OnTargetEvent += CancelTargetingState;
            stateMachine.InputReader.OnCancelEvent += CancelTargetingState;

            stateMachine.Animator.Play(TargetingBlendTreeHash);

            stateMachine.transform.forward = (stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position).normalized;
        }

        public override void Tick(float deltaTime)
        {
            if (stateMachine.Targeter.CurrentTarget == null)
            {
                CancelTargetingState();
            }
        }

        public override void Exit()
        {
            stateMachine.InputReader.OnTargetEvent -= CancelTargetingState;
            stateMachine.InputReader.OnCancelEvent -= CancelTargetingState;
        }

        public PlayerTargetingState(PlayerStateMachine stateMachine) : base(stateMachine)
        {

        }

        private void CancelTargetingState()
        {
            stateMachine.Targeter.Cancel();

            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
