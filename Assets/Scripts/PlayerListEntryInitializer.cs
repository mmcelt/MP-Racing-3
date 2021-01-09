using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerListEntryInitializer : MonoBehaviour
{
	#region Fields & Properties

	[Header("UI References")]
	public Text PlayerNameText;
	public Button PlayerReadyButton;
	public Image PlayerReadyImage;

	bool _isPlayerReady;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		
	}
	#endregion

	#region Public Methods

	public void Initialize(int playerID, string playerName)
	{
		PlayerNameText.text = playerName;

		if (PhotonNetwork.LocalPlayer.ActorNumber != playerID)
		{
			PlayerReadyButton.gameObject.SetActive(false);
		}
		else
		{
			//I'm the Local Player...
			ExitGames.Client.Photon.Hashtable initalProps = new ExitGames.Client.Photon.Hashtable() { { MPRG.PLAYER_READY, _isPlayerReady } };

			PhotonNetwork.LocalPlayer.SetCustomProperties(initalProps);

			PlayerReadyButton.onClick.AddListener(() =>
			{
				_isPlayerReady = !_isPlayerReady;
				SetPlayerReady(_isPlayerReady);

				ExitGames.Client.Photon.Hashtable newProps = new ExitGames.Client.Photon.Hashtable() { { MPRG.PLAYER_READY, _isPlayerReady } };

				PhotonNetwork.LocalPlayer.SetCustomProperties(newProps);
			});
		}
	}

	public void SetPlayerReady(bool playerReady)
	{
		PlayerReadyImage.enabled = playerReady;

		if (playerReady)
		{
			PlayerReadyButton.GetComponentInChildren<Text>().text = "Ready!";
		}
		else
		{
			PlayerReadyButton.GetComponentInChildren<Text>().text = "Ready?";
		}
	}
	#endregion

	#region Private Methods


	#endregion
}
