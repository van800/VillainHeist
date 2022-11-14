using System.Collections;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace movement_and_Camera_Scripts
{
    public class GuardController : MonoBehaviour
    {
        public Transform[] points;
        private Vector3[] _vertices;
        private Vector3 _next;
        private Vector3 _prev;
        private int _index;
        private bool _forwards = true;
        private bool _moving;
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
            if (_vertices.Length > 1)
            {
                _index = 1;
                _next = _vertices[_index];
                _prev = _vertices[0];
                _moving = true;
                Rotate();
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            if (_moving)
            {
                Move();
            }
            AttackPlayer();
        }
        
        private void AttackPlayer()
        { 
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 toTarget = player.transform.position - transform.position;
            if (Vector3.Angle(transform.forward, toTarget) <= viewAngle)
            {
                Debug.DrawRay(transform.position + Vector3.up, toTarget.normalized * range, Color.green);
                if (Physics.Raycast(transform.position + Vector3.up, toTarget, out RaycastHit hit, range))
                {
                    if (hit.transform == player.transform)
                    {
                        Debug.Log("HIT");
                        PlayerController playerController = player.GetComponent<PlayerController>();
                        playerController.Respawn();
                    }
                }
            }
        }

        private void Move()
        {
            if (transform.position != _next)
            {
                transform.position = Vector3.MoveTowards(transform.position, _next,
                    speed * Time.deltaTime);
            }
            else
            {
                if (_forwards)
                {
                    if (_index < points.Length)
                    {
                        _index++;
                        _prev = _next;
                        _next = _vertices[_index];
                        if (pauseOnAll)
                        {
                            _moving = false;
                            Invoke(nameof(StartMoving), pauseTime);
                        }
                    }
                    else
                    {
                        _forwards = false;
                        _moving = false;
                        _prev = _next;
                        _index--;
                        _next = _vertices[_index];
                        Invoke(nameof(StartMoving), pauseTime);
                    }
                }
                else
                {
                    if (_index > 0)
                    {
                        _index--;
                        _prev = _next;
                        _next = _vertices[_index];
                        if (pauseOnAll)
                        {
                            _moving = false;
                            Invoke(nameof(StartMoving), pauseTime);
                        }
                    }
                    else
                    {
                        _forwards = true;
                        _moving = false;
                        _prev = _next;
                        _index++;
                        _next = _vertices[_index];
                        Invoke(nameof(StartMoving), pauseTime);
                    }
                }
                Rotate();
            }
        }

        private void Rotate()
        {
            transform.rotation = Quaternion.LookRotation(_next - _prev, Vector3.up);
        }
        
        private void StartMoving()
        {
            _moving = true;
        }
    }
}
