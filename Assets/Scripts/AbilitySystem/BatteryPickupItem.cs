using movement_and_Camera_Scripts;
using UnityEngine;

namespace AbilitySystem
{
    public class BatteryPickupItem : MonoBehaviour
    {
        private PlayerController _player;
        
        [SerializeField]
        [Tooltip("Distance Until the item is picked up")]
        public float killDistance;
        
        // Start is called before the first frame update
        void Start()
        {
            _player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        }

        private void OnDestroy()
        {
           _player.AddMaxBattery(1);
           _player.PlayPickupSound();
        }

        // Update is called once per frame
        void Update()
        {
            if ( Vector3.Distance(_player.transform.position, this.transform.position) < this.killDistance)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
