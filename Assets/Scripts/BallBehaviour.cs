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
        private Rigidbody _body;

        private Vector3 _contactPoint = new Vector3();
        
        void Start()
        {
            _transform = gameObject.GetComponent<Transform>();
            _body = gameObject.GetComponent<Rigidbody>();

            _debugHitPoint = DebugUtil.CreateDot(_debugHitPointPrefab, new Vector3());
            _debugHitPoint.SetActive(false);

            _debugHitPointTransform = _debugHitPoint.GetComponent<Transform>();
        }

        public void SetContactPoint(Vector2 raito)
        {
            var fromPoint = new Vector3(-raito.x * 0.5f, raito.y * 0.5f, 1);
            fromPoint = _transform.TransformPoint(fromPoint);

            var hit = new RaycastHit();
            //var success = Physics.Raycast(new Ray(fromPoint, Vector3.back), out hit, Mathf.Infinity, LayerMask.NameToLayer("Ball"));
            var success = Physics.Raycast(new Ray(fromPoint, Vector3.back), out hit);
            if (!success)
                return;

            _contactPoint = hit.point;

            _debugHitPointTransform.position = hit.point;
            _debugHitPoint.SetActive(true);
        }

        public void Serve(float speedMs)
        {
            var impulse = Util.CalcImpulse(_body.mass, speedMs);
            _body.AddForceAtPosition(Vector3.back * impulse, _contactPoint, ForceMode.Impulse);
            _body.useGravity = true;
        }
        
        void Update()
        {

        }
    }
}
