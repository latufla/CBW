using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField]
    private GameObject _aimUi;

    [SerializeField]
    private GameObject _ball = null;
    private Rigidbody _ballBody;

    [SerializeField]
    private float _ballServeSpeedKmH = 0.0f;
    private float _ballServeSpeedMs;

    private float _impulse;

    [SerializeField]
    private float _height = 2.43f;

    [SerializeField]
    private GameObject _pointSphere = null;

    private LineRenderer _trajectory = null;
	// Use this for initialization
	void Start ()
	{
	    _ballBody = _ball.GetComponent<Rigidbody>();
        _ballServeSpeedMs = Util.ToMs(_ballServeSpeedKmH);

        _impulse = Util.CalcImpulse(_ballBody.mass, _ballServeSpeedMs);
	    _trajectory = _ball.GetComponent<LineRenderer>();

	    var aim = _aimUi.GetComponent<AimBehaviour>();
	    aim.OnClick = HandleAimClick;
	}

    public void HandleAimClick(Vector2 pos)
    {
        Debug.Log(pos.ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (Input.GetKeyDown(KeyCode.Return))
	    {
            SceneManager.LoadScene("Main");
	    }

	    // throw to height
	    if (Input.GetKeyDown(KeyCode.Space))
	    {
	        var vMs = Mathf.Sqrt(2 * -Physics.gravity.y * _height);
            var imp = Util.CalcImpulse(_ballBody.mass, vMs);
            _ballBody.AddForce(Vector3.up * imp, ForceMode.Impulse);
        }

        // serve trajectory
	    if (Input.GetKeyDown(KeyCode.LeftControl))
	    {
	        var velocity = _ballServeSpeedMs * (Vector3.forward * -1);
            var pos = new Vector3();
	        var positions = new List<Vector3>();
	        for (var i = 0.0f; i < 1.0f; i += 0.05f)
	        {
                var deltaTime = i;
                pos.x = _ballBody.transform.position.x + velocity.x * deltaTime;
                pos.y = _ballBody.transform.position.y + velocity.y * deltaTime + 0.5f * Physics.gravity.y * deltaTime * deltaTime;
                pos.z = _ballBody.transform.position.z + velocity.z * deltaTime;
                positions.Add(pos);
            }
	        _trajectory.enabled = true;
	        BuildTrajectoryLine(positions);
	    }

        // serve
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // x = r * cos(s) * sin(t)
            // y = r * sin(s) * sin(t)
            // z = r * cos(t)

            var r = _ballBody.transform.localScale.x / 2; // whatewer its ball
            var t = 45 * Mathf.Deg2Rad;
            var s = 90 * Mathf.Deg2Rad;

            var relPos = new Vector3
            {
                x = r * Mathf.Cos(s) * Mathf.Sin(t),
                y = r * Mathf.Cos(t),
                z = r * Mathf.Sin(s) * Mathf.Sin(t)
            };

            //var relPos = new Vector3(0, 0.21f / 2, 0);
            var position = _ballBody.position + relPos;

           // var dot = DebugUtil.CreateDot(_pointSphere, position);

            // _ballBody.AddForceAtPosition(Vector3.forward * -1 * _impulse, position, ForceMode.Impulse);
            _ballBody.AddForceAtPosition(Vector3.forward * -1 * _impulse, new Vector3(0, 0, 0), ForceMode.Impulse);
            _ballBody.useGravity = true;
        }
    }

    void FixedUpdate()
    {
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
