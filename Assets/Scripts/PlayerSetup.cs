using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviourPun
{
	#region Fields & Properties

	public GameObject PlayerCamera;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (photonView.IsMine)	//local player
		{
			GetComponent<CarMovement>().enabled = true;
			PlayerCamera.SetActive(true);
		}
		else
		{
			GetComponent<CarMovement>().enabled = false;
			PlayerCamera.SetActive(false);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
