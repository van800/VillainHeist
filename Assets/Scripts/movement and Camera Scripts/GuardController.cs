using System.Linq;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class GuardController : MonoBehaviour
    {
        public Transform[] points;
        private Transform _start;
        private Transform _end;
        private Transform _current;
        private Transform _prev;
        private int _index;
        private bool _forwards = true;
        [SerializeField] private float speed;
        [SerializeField] private float viewAngle;
        [SerializeField] private float range;
        [SerializeField] private float pauseTime;
        [SerializeField] [Tooltip("If true guard will freeze for pauseTime Seconds" +
                                  "at each point in points[], if False, guard will pause only at the ends")] 
        private bool pauseOnAll;

        // Start is called before the first frame update
        void Start()
        {
            // add the robot's initial transform to the list
            _start = transform;
            _end = points[^1];
            _current = points[0];
            _prev = _start;
            points = new [] { transform }.Concat(points).ToArray();
            _index = 1;
        }

        // Update is called once per frame
        void Update()
        {
            transform.position = Vector3.Lerp(_prev.position, _current.position, Time.deltaTime * speed);
            if (_forwards)
            {
                if (transform.position == _current.position)
                {
                    
                    if (_current == _end)
                    {
                        _forwards = false;
                        _current = points[^2];
                        _prev = _end;
                    }
                    else
                    {
                        _index += 1;
                        _prev = _current;
                        _current = points[_index];
                    }
                }
            }
            else
            {
                if (transform.position == _current.position)
                {
                    
                    if (_current == _start)
                    {
                        _forwards = true;
                        _current = points[1];
                        _prev = _start;
                    }
                    else
                    {
                        _index -= 1;
                        _prev = _current;
                        _current = points[_index];
                    }
                }
            }
        }
    }
}
