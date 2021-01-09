using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class RacingGameManager : MonoBehaviour
{
	#region Fields & Properties

	public static RacingGameManager Instance;

	public GameObject[] PlayerPrefabs;
	public Transform[] InstantiatePositions;
	public Text TimeUIText;
	public List<GameObject> LapTriggers;
	public GameObject[] FinishOrderGameObjects;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		DontDestroyOnLoad(gameObject);
	}

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

			foreach (GameObject finishOrder in FinishOrderGameObjects)
				finishOrder.SetActive(false);
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods


	#endregion
}
