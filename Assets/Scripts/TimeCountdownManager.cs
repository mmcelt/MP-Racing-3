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
			photonView.RPC(nameof(SetTime), RpcTarget.AllBuffered, _timeToStartRace);
		}
		else
		{
			photonView.RPC(nameof(StartRace), RpcTarget.AllBuffered);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	[PunRPC]
	void SetTime(float time)
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
	void StartRace()
	{
		GetComponent<CarMovement>().ControlsEnabled = true;
		enabled = false;
	}
	#endregion
}
