using System;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField]
    private AimBehaviour _aimBehaviour;

    [SerializeField]
    private GameObject _ball = null;
    [SerializeField]    
    private BallBehaviour _ballBehaviour;
    private Transform _ballTransform;
    private Rigidbody _ballBody;

    [SerializeField]
    private float _ballServeSpeedKmH = 0.0f;
    private float _ballServeSpeedMs;

    private float _impulse;

    [SerializeField]
    private float _height = 2.43f;

    [SerializeField]
    private GameObject _pointSphere = null;

	// Use this for initialization
	void Start ()
	{
	    _ballBody = _ball.GetComponent<Rigidbody>();
	    _ballTransform = _ball.GetComponent<Transform>();

        _ballServeSpeedMs = Util.ToMs(_ballServeSpeedKmH);
	}


    public void HandleAimClick(Vector2 pos)
    {
        Debug.Log(pos.ToString());
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            var aimPos = _aimBehaviour.CalcRatioPoint();
            _ballBehaviour.SetContactPoint(aimPos);
        }

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
            _ballBody.useGravity = true;
        }

        // serve trajectory
	    if (Input.GetKeyDown(KeyCode.LeftControl))
	    {
	        _ballBehaviour.DebugShowHitTrajectory(Vector3.back, _ballServeSpeedMs);
	    }

        // serve
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _ballBehaviour.Hit(Vector3.back, _ballServeSpeedMs);
        }
    }

    void FixedUpdate()
    {
    }
}
