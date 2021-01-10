using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class PlayerSetup : MonoBehaviourPun
{
	#region Fields & Properties

	public GameObject PlayerCamera;
	public TextMeshProUGUI PlayerNameText;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (photonView.IsMine)	//local player
		{
			GetComponent<CarMovement>().enabled = true;
			GetComponent<LapController>().enabled = true;
			PlayerCamera.SetActive(true);
		}
		else
		{
			GetComponent<CarMovement>().enabled = false;
			GetComponent<LapController>().enabled = false;
			PlayerCamera.SetActive(false);
		}
		SetPlayerUI();
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void SetPlayerUI()
	{
		if (PlayerNameText == null) return;

		PlayerNameText.text = photonView.Owner.NickName;

		if (photonView.IsMine)
			PlayerNameText.gameObject.SetActive(false);
	}
	#endregion
}
