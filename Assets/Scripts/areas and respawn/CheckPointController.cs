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
        
        private Renderer _rend;

        [SerializeField]
        private bool isSpawn;
        
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
            
            if (!isSpawn)
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
            _area.Reset();
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
                    SetLightMaterial(currentCheckpoint); 
                }
            }
        }

        public void DeselectCheckpoint()
        {
            SetLightMaterial(openCheckpoint); 
        }

        private void SetLightMaterial(Material mat)
        {
            if (!isSpawn)
            {
                Material[] mats = _rend.materials;
                mats[2] = mat;
                _rend.materials = mats;
            }
        }
    }
}
