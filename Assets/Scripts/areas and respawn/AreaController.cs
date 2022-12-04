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
            PlayerController player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
            foreach (RoomController subRoom in _subRooms) // call on sub areas
            {
                subRoom.SetUp();
                subRoom.Save();
                if (player.isFirstPov)
                {
                    if (subRoom.CompareTag($"End Room"))
                    {
                        player.SetRoom(subRoom);
                        player.checkpoint = fpSpawnPoint;
                        player.transform.position = fpSpawnPoint.transform.position;
                    }
                }
                else
                {
                    if (subRoom.CompareTag($"Start Room"))
                    {
                        player.SetRoom(subRoom);
                        player.checkpoint = tdSpawnPoint;
                        player.transform.position = tdSpawnPoint.transform.position;
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
