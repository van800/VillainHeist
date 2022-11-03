using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class CheckPointController : MonoBehaviour
    {
        private Transform _spawnPoint;

        private AreaController _area;

        // Start is called before the first frame update
        void Start()
        {
            _spawnPoint = GetComponentInChildren<Transform>();
            _area = GetComponentInParent<AreaController>();
        }

        public void Respawn(PlayerController player)
        {
            player.transform.position = _spawnPoint.position;
            player.SetRoom(_area);
            _area.Reset();
        }
    }
}
