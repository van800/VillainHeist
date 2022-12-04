using System;
using System.Linq;
using movement_and_Camera_Scripts;
using UnityEngine;

namespace areas_and_respawn
{
    public class CheckPointController : MonoBehaviour
    {
        private Transform _spawnPoint;
        
        private RoomController _room;

        private AreaController _area;

        private GameUI _gameUI;
        
        private bool _isSpawn;
        
        private bool _hasRender;

        private Renderer _rend;
        
        [SerializeField] private Material currentCheckpoint;

        [SerializeField] private Material openCheckpoint;
        
        [SerializeField] private Material closedCheckpoint;
        
        private AudioSource checkPointAS;

        // Start is called before the first frame update
        void Start()
        {
            PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            _gameUI = FindObjectOfType<GameUI>();
            _spawnPoint = transform;
            _room = GetComponentInParent<RoomController>();
            _area = GetComponentInParent<AreaController>();
            _isSpawn = _room is null;
            if (!_isSpawn)
            {
                _rend = GetComponentInChildren<Renderer>();
                DeselectCheckpoint();
                if (player.isFirstPov)
                {
                    SetLightMaterial(closedCheckpoint); 
                }
            }
            else
            {
                if (player.isFirstPov)
                {
                    _room = GameObject.FindWithTag("End Room").GetComponent<RoomController>();
                }
                else
                {
                    _room = GameObject.FindWithTag("Start Room").GetComponent<RoomController>();
                }
            }
            checkPointAS = GetComponent<AudioSource>();
        }

        public void Respawn(PlayerController player)
        {
            player.transform.position = _spawnPoint.position;
            player.SetRoom(_room);
            if (_isSpawn)
            {
                _area.Reset();
            }
            else
            {
                _room.Reset();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                PlayerController player = other.gameObject.GetComponent<PlayerController>();
                if (player.checkpoint != this && !player.isFirstPov)
                {
                    _gameUI.ShowCheckpoint();
                    checkPointAS.Play();
                    player.SetCheckpoint(this);
                    _area.Save();
                    if (!_isSpawn)
                    {
                        SetLightMaterial(currentCheckpoint); 
                    }
                }
            }
        }

        public void DeselectCheckpoint()
        {
            if (!_isSpawn)
            {
               SetLightMaterial(openCheckpoint); 
            }
        }

        private void SetLightMaterial(Material mat)
        {
            Material[] mats = _rend.materials;
            mats[2] = mat;
            _rend.materials = mats;
        }
    }
}
