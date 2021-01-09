using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
	#region Fields & Properties

	public Vector3 ThrustForce = new Vector3(0f,0f,45f);
	public Vector3 RotationTorque = new Vector3(0f, 8f, 0f);

	Rigidbody _theRB;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		_theRB = GetComponent<Rigidbody>();
	}
	
	void Update() 
	{
		//forward
		if (Input.GetKey(KeyCode.W))
		{
			_theRB.AddRelativeForce(ThrustForce);
		}
		//backward
		if (Input.GetKey(KeyCode.S))
		{
			_theRB.AddRelativeForce(-ThrustForce);
		}
		//left
		if (Input.GetKey(KeyCode.A))
		{
			_theRB.AddRelativeTorque(-RotationTorque);
		}
		//right
		if (Input.GetKey(KeyCode.D))
		{
			_theRB.AddRelativeTorque(RotationTorque);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
