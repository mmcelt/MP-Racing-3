using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

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

	[Header("Inside Room Panel")]
	public GameObject InsideRoomUIPanel;

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
	}
	#endregion

	#region Private Methods


	#endregion
}
