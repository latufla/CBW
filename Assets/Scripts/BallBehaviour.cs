using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class BallBehaviour : MonoBehaviour
    {
        [SerializeField]
        private GameObject _debugHitPointPrefab;
        private GameObject _debugHitPoint;
        private Transform _debugHitPointBody;

        private Transform _body;

        private Vector2 _hitPoint;
        public Vector2 HitPoint
        {
            get { return _hitPoint; }
            set
            {
                _hitPoint = value;
                DebugDrawHitPoint();
            
                Debug.Log(_hitPoint.ToString());
            }
        }
        
        void Start()
        {
            _body = gameObject.GetComponent<Transform>();

            _debugHitPoint = DebugUtil.CreateDot(_debugHitPointPrefab, HitPoint);
            _debugHitPoint.SetActive(false);

            _debugHitPointBody = _debugHitPoint.GetComponent<Transform>();
            _debugHitPointBody.SetParent(_body, false);
            
            HitPoint = new Vector2(0.0f, 0.0f);
        }

        public void Hit()
        {
            
        }
        
        void Update()
        {

        }

        private Vector3 CalcHitPoint()
        {
            var radius = 0.5f;
            var pos = new Vector3(-_hitPoint.x, _hitPoint.y, 0);
            pos.x *= radius;
            pos.y *= radius;
            return pos;
        }

        private void DebugDrawHitPoint()
        {
            _debugHitPointBody.localPosition = CalcHitPoint();
            _debugHitPoint.SetActive(true);
        }

        private float CalcRadius()
        {
            return _body.localScale.x / 2;;
        }

    }
}
