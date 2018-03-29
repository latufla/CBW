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
        private Transform _debugHitPointTransform;

        private Transform _transform;

        private Vector3 _hitPoint = new Vector3();
        
        void Start()
        {
            _transform = gameObject.GetComponent<Transform>();

            _debugHitPoint = DebugUtil.CreateDot(_debugHitPointPrefab, new Vector3());
            _debugHitPoint.SetActive(false);

            _debugHitPointTransform = _debugHitPoint.GetComponent<Transform>();
        }

        public void SetHitPoint(Vector2 raito)
        {
            var fromPoint = new Vector3(-raito.x * 0.5f, raito.y * 0.5f, 1);
            fromPoint = _transform.TransformPoint(fromPoint);

            var hit = new RaycastHit();
            var success = Physics.Raycast(new Ray(fromPoint, Vector3.back), out hit);
            if (!success)
                return;

            _hitPoint = hit.point;

            _debugHitPointTransform.localPosition = _hitPoint;
            _debugHitPoint.SetActive(true);
        }

        public void Hit()
        {
            
        }
        
        void Update()
        {

        }
    }
}
