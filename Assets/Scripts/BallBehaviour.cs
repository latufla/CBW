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


        private LineRenderer _trajectory = null;

        void Start()
        {
            _transform = gameObject.GetComponent<Transform>();
            _body = gameObject.GetComponent<Rigidbody>();

            _trajectory = gameObject.GetComponent<LineRenderer>();

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

        public void Hit(Vector3 direction, float speedMs)
        {
            var impulse = Util.CalcImpulse(_body.mass, speedMs);
            _body.AddForceAtPosition(direction * impulse, _contactPoint, ForceMode.Impulse);
            _body.useGravity = true;
        }
        
        void Update()
        {

        }
        

        public void DebugShowHitTrajectory(Vector3 direction, float speedMs)
        {
            var velocity = direction * speedMs;
            var pos = new Vector3();
            var positions = new List<Vector3>();
            for (var i = 0.0f; i < 1.0f; i += 0.05f)
            {
                var deltaTime = i;
                pos.x = _transform.position.x + velocity.x * deltaTime;
                pos.y = _transform.transform.position.y + velocity.y * deltaTime + 0.5f * Physics.gravity.y * deltaTime * deltaTime;
                pos.z = _transform.transform.position.z + velocity.z * deltaTime;
                positions.Add(pos);
            }
            _trajectory.enabled = true;
            BuildTrajectoryLine(positions);
        }

        void BuildTrajectoryLine(List<Vector3> positions)
        {
            _trajectory.positionCount = positions.Count;
            for (var i = 0; i < positions.Count; ++i)
            {
                _trajectory.SetPosition(i, positions[i]);
            }
        }
    }
}
