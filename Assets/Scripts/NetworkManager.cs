using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
	#region Fields & Properties

	[Header("Login UI")]
	public GameObject LoginUIPanel;
	public InputField PlayerNameInput;

	[Header("Connecting Info Panel")]
	public GameObject ConnectingInfoUIPanel;

	[Header("Creating Room Info Panel")]
	public GameObject CreatingRoomInfoUIPanel;

	[Header("GameOptions  Panel")]
	public GameObject GameOptionsUIPanel;

	[Header("Create Room Panel")]
	public GameObject CreateRoomUIPanel;
	public InputField RoomNameInput;
	public string GameMode;

	[Header("Inside Room Panel")]
	public GameObject InsideRoomUIPanel;
	public Text RoomInfoText;
	public GameObject PlayerListPrefab;
	public Transform PlayerListContent;

	[Header("Join Random Room Panel")]
	public GameObject JoinRandomRoomUIPanel;

	#endregion

	#region Getters


	#endregion

	#region Unity Methods

	void Start() 
	{
		ActivatePanel(LoginUIPanel.name);
	}
	
	void Update() 
	{
		
	}
	#endregion

	#region UI Callback Methods

	public void OnLoginButtonClicked()
	{
		string playerName = PlayerNameInput.text;

		if (!string.IsNullOrEmpty(playerName))
		{
			ActivatePanel(ConnectingInfoUIPanel.name);

			//connect to Photon...
			if (!PhotonNetwork.IsConnected)
			{
				PhotonNetwork.LocalPlayer.NickName = playerName;
				PhotonNetwork.ConnectUsingSettings();
			}
		}
		else
		{
			Debug.Log("Player Name is Invalid!");
		}
	}

	public void OnCancelButtonClicked()
	{
		ActivatePanel(GameOptionsUIPanel.name);
	}

	public void OnCreateRoomButtonClicked()
	{
		ActivatePanel(CreatingRoomInfoUIPanel.name);

		if (GameMode != null)
		{
			string roomName = RoomNameInput.text;

			if (string.IsNullOrEmpty(roomName))
			{
				roomName = "Room" + Random.Range(1000, 10000);
			}

			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = 3;

			string[] roomPropsInLobby = { "gm" };   //gm=game mode

			//two game modes:
			//1. racing = "rc"
			//2. death race = "dr"

			ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "gm", GameMode } };

			roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
			roomOptions.CustomRoomProperties = customRoomProperties;

			PhotonNetwork.CreateRoom(roomName, roomOptions);
		}
	}

	public void OnJoinRandomRoomButtonClicked(string gameMode)
	{
		GameMode = gameMode;

		ExitGames.Client.Photon.Hashtable expectedCustomRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "gm", gameMode } };
		PhotonNetwork.JoinRandomRoom(expectedCustomRoomProperties, 0);
	}

	public void OnBackButtonClicked()
	{
		ActivatePanel(GameOptionsUIPanel.name);
	}
	#endregion

	#region Photon Callbacks

	public override void OnConnected()	//1st method called on connection to Photon
	{
		Debug.Log("We Connected to the Internet");
	}

	public override void OnConnectedToMaster()	//called when successfully connected to Photon
	{
		ActivatePanel(GameOptionsUIPanel.name);

		Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is Connected to Photon");
	}

	public override void OnCreatedRoom()
	{
		Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created.");
	}

	public override void OnJoinedRoom()	//only called on joining player's machine
	{
		Debug.Log(PhotonNetwork.LocalPlayer.NickName + " is joined to " + PhotonNetwork.CurrentRoom.Name + " Player count: " + PhotonNetwork.CurrentRoom.PlayerCount);

		ActivatePanel(InsideRoomUIPanel.name);

		if (PhotonNetwork.CurrentRoom.CustomProperties.ContainsKey("gm"))
		{
			//object gameModeName;
			//if(PhotonNetwork.CurrentRoom.CustomProperties.TryGetValue("gm",out gameModeName))
			//{
			//	Debug.Log("Game Mode: " + gameModeName.ToString());
			//}

			RoomInfoText.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name + " " +
				" Players/MaxPlayers: " +
				PhotonNetwork.CurrentRoom.PlayerCount + " / " +
				PhotonNetwork.CurrentRoom.MaxPlayers;

			foreach(Player player in PhotonNetwork.PlayerList)
			{
				GameObject playerListGameObject = Instantiate(PlayerListPrefab, PlayerListContent);
				playerListGameObject.transform.localScale = Vector3.one;
				playerListGameObject.GetComponent<PlayerListEntryInitializer>().Initialize(player.ActorNumber, player.NickName);

			}
		}
	}

	public override void OnJoinRandomFailed(short returnCode, string message)
	{
		Debug.Log($"Failed to join a Random Room: {message}");

		//if there is no room, create one...
		if (GameMode != null)
		{
			string roomName = RoomNameInput.text;

			if (string.IsNullOrEmpty(roomName))
			{
				roomName = "Room" + Random.Range(1000, 10000);
			}

			RoomOptions roomOptions = new RoomOptions();
			roomOptions.MaxPlayers = 3;

			string[] roomPropsInLobby = { "gm" };   //gm=game mode

			//two game modes:
			//1. racing = "rc"
			//2. death race = "dr"

			ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "gm", GameMode } };

			roomOptions.CustomRoomPropertiesForLobby = roomPropsInLobby;
			roomOptions.CustomRoomProperties = customRoomProperties;

			PhotonNetwork.CreateRoom(roomName, roomOptions);
		}
	}
	#endregion

	#region Public Methods

	public void ActivatePanel(string panelNameToBeActivated)
	{
		LoginUIPanel.SetActive(LoginUIPanel.name.Equals(panelNameToBeActivated));
		ConnectingInfoUIPanel.SetActive(ConnectingInfoUIPanel.name.Equals(panelNameToBeActivated));
		CreatingRoomInfoUIPanel.SetActive(CreatingRoomInfoUIPanel.name.Equals(panelNameToBeActivated));
		CreateRoomUIPanel.SetActive(CreateRoomUIPanel.name.Equals(panelNameToBeActivated));
		GameOptionsUIPanel.SetActive(GameOptionsUIPanel.name.Equals(panelNameToBeActivated));
		JoinRandomRoomUIPanel.SetActive(JoinRandomRoomUIPanel.name.Equals(panelNameToBeActivated));
		InsideRoomUIPanel.SetActive(InsideRoomUIPanel.name.Equals(panelNameToBeActivated));
	}

	public void SetGameMode(string gameMode)
	{
		GameMode = gameMode;
	}
	#endregion

	#region Private Methods


	#endregion
}
