using System.Linq;
using UnityEngine;

namespace movement_and_Camera_Scripts
{
    public class GuardController : MonoBehaviour
    {
        public Transform[] points;
        private Vector3[] _vertices;
        private Vector3 _current;
        private Vector3 _prev;
        private int _index;
        private float _timer;
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
            _vertices = new [] { transform.position };
            foreach (Transform t in points)
            {
                _vertices = _vertices.Concat(new [] {t.position}).ToArray();
            }
            // add the robot's initial transform to the list
            _index = 1;
            _current = _vertices[_index];
            _prev = transform.position;
            _timer = 0;
        }

        // Update is called once per frame
        void Update()
        {
            _timer += Time.deltaTime * speed;
            if (transform.position != _current)
            {
                transform.position = Vector3.Lerp(_prev, _current, _timer);
            }
            else
            {
                _timer = 0;
                if (_forwards)
                {
                    if (_index < points.Length - 1)
                    {
                        _index++;
                        _prev = _current;
                        _current = _vertices[_index];
                    }
                    else
                    {
                        _forwards = false;
                        _prev = _current;
                        _index--;
                        _current = _vertices[_index];
                    }
                }
                else
                {
                    if (_index > 0)
                    {
                        _index--;
                        _prev = _current;
                        _current = _vertices[_index];
                    }
                    else
                    {
                        _forwards = true;
                        _prev = _current;
                        _index++;
                        _current = _vertices[_index];
                    }
                }
            }
        }
    }
}
