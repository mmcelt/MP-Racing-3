using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RacingGameManager : MonoBehaviour
{
	#region Fields & Properties

	public GameObject[] PlayerPrefabs;
	public Transform[] InstantiatePositions;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		if (PhotonNetwork.IsConnectedAndReady)
		{
			object playerSelectionNumber;
			if(PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue(MPRG.PLAYER_SEL_NUM, out playerSelectionNumber))
			{
				int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;

				PhotonNetwork.Instantiate(PlayerPrefabs[(int)playerSelectionNumber].name, InstantiatePositions[actorNumber - 1].position, Quaternion.identity);
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
