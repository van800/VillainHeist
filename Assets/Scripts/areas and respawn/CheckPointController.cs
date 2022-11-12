using System;
using movement_and_Camera_Scripts;
using UnityEngine;

namespace areas_and_respawn
{
    public class CheckPointController : MonoBehaviour
    {
        public Transform _spawnPoint;
        
        private SubAreaController _subArea;

        private AreaController _area;

        private bool _isSpawn;

        // Start is called before the first frame update
        void Start()
        {
            _spawnPoint = GetComponentInChildren<Transform>();
            _subArea = GetComponentInParent<SubAreaController>();
            _area = GetComponentInParent<AreaController>();
            _isSpawn = _subArea is null;
        }

        public void Respawn(PlayerController player)
        {
            player.transform.position = _spawnPoint.position;
            player.SetRoom(_area);
            if (_isSpawn)
            {
                _area.Reset();
            }
            else
            {
                _subArea.Reset();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player.checkpoint != this)
                {
                    player.SetCheckpoint(this);
                    _subArea.Save();
                }
            }
        }
    }
}
