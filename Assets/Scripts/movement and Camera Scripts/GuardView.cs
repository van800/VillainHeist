using System;
using System.Transactions;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class GuardView : MonoBehaviour
    {
        private GuardController _guardController;
        
        private void Start()
        {
            _guardController = GetComponentInParent<GuardController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _guardController.AttackPlayer();
            }
        }
    }
}
