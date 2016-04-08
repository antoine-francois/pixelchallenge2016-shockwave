﻿using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{
    public Rigidbody _tRigidbody { get; private set; }

	void Start()
    {
        BallManager.Instance.AddBall( this );
        _tRigidbody = GetComponent<Rigidbody>();
	}

	void Update()
    {
	}

    void FixedUpdate()
    {

    }

    void OnDestroy()
    {
        BallManager.Instance.RemoveBall( this );
    }
}
