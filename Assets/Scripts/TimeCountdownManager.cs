using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TimeCountdownManager : MonoBehaviourPun
{
	#region Fields & Properties


	Text _timeUIText;
	float _timeToStartRace = 5.0f;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		_timeUIText = RacingGameManager.Instance.TimeUIText;
	}

	void Update()
	{
		if (!PhotonNetwork.IsMasterClient) return;

		if (_timeToStartRace >= 0.0f)
		{
			_timeToStartRace -= Time.deltaTime;
			photonView.RPC(nameof(RpcSetTime), RpcTarget.AllBuffered, _timeToStartRace);
		}
		else
		{
			photonView.RPC(nameof(RpcStartRace), RpcTarget.AllBuffered);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	[PunRPC]
	void RpcSetTime(float time)
	{
		if (time > 0)
		{
			_timeUIText.text = time.ToString("F0");
		}
		else
		{
			_timeUIText.text = string.Empty;
		}
	}

	[PunRPC]
	void RpcStartRace()
	{
		GetComponent<CarMovement>().ControlsEnabled = true;
		enabled = false;
	}
	#endregion
}
