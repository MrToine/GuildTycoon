using System.Timers;
using Core.Runtime;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player.Runtime
{
    public class Deplacements : BaseMonobehaviour
    {

        #region Publics

        //

        #endregion


        #region Unity API

        private void Start()
        {
            _agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            _animator = GetComponent<Animator>();
            GoToCurrentTarget();
        }

        private void Update()
        {
            _animator.SetFloat("Speed", _agent.speed);
            _timer += Time.deltaTime;

            if (_isMoving)
            {
                if (_timer >= _moveDuration)
                {
                    StopMoving();
                    _agent.speed = 0;
                }
            }
            else
            {
                if (_timer >= _pauseDuration)
                {
                    _agent.speed = 2;
                    GoToCurrentTarget();
                }
            }
        }

        #endregion


        #region Main Methods

        private void GoToCurrentTarget()
        {
            _timer = 0f;
            _isMoving = true;
            _agent.isStopped = false;

            // Choisir un point aléatoire dans la zone définie
            Vector3 randomTarget = new Vector3(
                Random.Range(_zoneXZMin.x, _zoneXZMax.x),
                transform.position.y,
                Random.Range(_zoneXZMin.y, _zoneXZMax.y)
            );

            _agent.SetDestination(randomTarget);
        }

        private void StopMoving()
        {
            _timer = 0f;
            _isMoving = false;
            _agent.isStopped = true;
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected
        
        [FormerlySerializedAs("zoneXZMin")]
        [Header("Zone de déplacement aléatoire")]
        [SerializeField] private Vector2 _zoneXZMin = new Vector2(-10f, -10f);
        [FormerlySerializedAs("zoneXZMax")]
        [SerializeField] private Vector2 _zoneXZMax = new Vector2(10f, 10f);
        
        [FormerlySerializedAs("moveDuration")]
        [SerializeField] private float _moveDuration = 3f;
        [FormerlySerializedAs("pauseDuration")]
        [SerializeField] private float _pauseDuration = 2f;

        private UnityEngine.AI.NavMeshAgent _agent;
        private float _timer = 0f;
        private bool _isMoving = true;
        private Animator _animator;

        #endregion
    }
}
