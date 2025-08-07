using System;
using UnityEngine;

namespace ThirdPersonTraversalandCombat.Combat
{
    public class Target : MonoBehaviour
    {
        public event Action<Target> OnTargetDeath;

        private void OnDestroy()
        {
            OnTargetDeath?.Invoke(this);
        }

        private void OnDisable()
        {
            OnTargetDeath?.Invoke(this);
        }
    }
}
