using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPun
{
	#region Fields & Properties

	[SerializeField] GameObject _camera;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (photonView.IsMine)	//local player
		{
			GetComponent<CarMovement>().enabled = true;
			_camera.SetActive(true);
		}
		else
		{
			GetComponent<CarMovement>().enabled = false;
			_camera.SetActive(false);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
