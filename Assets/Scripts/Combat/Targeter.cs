using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

namespace ThirdPersonTraversalandCombat.Combat
{
    public class Targeter : MonoBehaviour
    {
       [SerializeField] private CinemachineTargetGroup targetGroup;

        private List<Target> targets = new List<Target>();

        public Target CurrentTarget { get; private set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Target target))
            {
                if (!targets.Contains(target))
                {
                    targets.Add(target);

                    target.OnTargetDeath += RemoveTarget;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Target target))
            {
                if (targets.Contains(target))
                {
                    RemoveTarget(target);
                }
            }
        }

        private void RemoveTarget(Target target)
        {
            if (CurrentTarget == target)
            {
                CurrentTarget = null;
                // TODO: Select Next Target or Cancel TargetingState
            }

            if (targets.Contains(target))
            {
                targets.Remove(target);
            }

            target.OnTargetDeath -= RemoveTarget;
        }

        public bool SelectTarget()
        {
            if (targets.Count == 0) return false;

            CurrentTarget = targets[0];

            targetGroup.AddMember(CurrentTarget.transform, 1f, 1f);
            return true;
        }

        public void Cancel()
        {
            if (CurrentTarget == null) return;

            targetGroup.RemoveMember(CurrentTarget.transform);

            CurrentTarget = null;
        }
    }
}
