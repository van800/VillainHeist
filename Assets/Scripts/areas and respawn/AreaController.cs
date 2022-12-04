using System;
using System.ComponentModel;
using JetBrains.Annotations;
using movement_and_Camera_Scripts;
using UnityEngine;
using UnityEngine.UIElements;

namespace areas_and_respawn
{
    public class AreaController : MonoBehaviour
    {
        private RoomController[] _subRooms;

        public CheckPointController tdSpawnPoint;
        
        public CheckPointController fpSpawnPoint;

        private void Start()
        {
            _subRooms = GetComponentsInChildren<RoomController>();
            print(_subRooms.Length);
            PlayerController player = GameObject.FindWithTag("PLayer").GetComponent<PlayerController>();
            foreach (RoomController subRoom in _subRooms) // call on sub areas
            {
                subRoom.SetUp();
                subRoom.Save();
                if (player.isFirstPov)
                {
                    if (subRoom.CompareTag($"Start Room"))
                    {
                        PlayerController playerController = FindObjectOfType<PlayerController>();
                        playerController.SetRoom(subRoom);
                        playerController.checkpoint = tdSpawnPoint;
                        FindObjectOfType<PlayerController>().transform.position = tdSpawnPoint.transform.position;
                    }
                }
                else
                {
                    if (subRoom.CompareTag($"End Room"))
                    {
                        PlayerController playerController = FindObjectOfType<PlayerController>();
                        playerController.SetRoom(subRoom);
                        playerController.checkpoint = fpSpawnPoint;
                        FindObjectOfType<PlayerController>().transform.position = fpSpawnPoint.transform.position;
                    }
                }
            }
        }

        public void Reset()
        {
           foreach (RoomController subArea in _subRooms) // call on sub areas
           {
               subArea.Reset();
           }
        }

        public void Save()
        {
            foreach (RoomController subRoom in _subRooms) // call on sub areas
            {
                subRoom.Save();
            }
        }
    }
}
