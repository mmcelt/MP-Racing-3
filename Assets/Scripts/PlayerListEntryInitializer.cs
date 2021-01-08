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
	}
	#endregion

	#region Private Methods


	#endregion
}
