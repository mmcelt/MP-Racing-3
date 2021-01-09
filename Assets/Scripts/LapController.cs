using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.UI;

public class LapController : MonoBehaviourPun
{
	#region Fields & Properties

	public enum RaiseEventCode
	{
		WhoFinishedEventCode = 0
	}

	List<GameObject> _lapTriggers = new List<GameObject>();

	int _finishOrder;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void OnEnable()
	{
		PhotonNetwork.NetworkingClient.EventReceived += OnWhoFinishedEventReceived;
	}

	void OnDisable()
	{
		PhotonNetwork.NetworkingClient.EventReceived -= OnWhoFinishedEventReceived;
	}

	void Start() 
	{
		foreach(GameObject lapTrigger in RacingGameManager.Instance.LapTriggers)
		{
			_lapTriggers.Add(lapTrigger);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (_lapTriggers.Contains(other.gameObject))
		{
			int indexOfTrigger = _lapTriggers.IndexOf(other.gameObject);
			_lapTriggers[indexOfTrigger].SetActive(false);

			if (other.name == "FinishTrigger")
			{
				//game is over...
				GameFinished();
			}
		}
	}
	#endregion

	#region Public Methods


	#endregion

	#region Private Methods

	void GameFinished()
	{
		GetComponent<PlayerSetup>().PlayerCamera.transform.parent = null;
		GetComponent<CarMovement>().enabled = false;

		_finishOrder++;

		string nickName = photonView.Owner.NickName;
		int viewID = photonView.ViewID;

		//event data
		object[] data = new object[] { nickName, _finishOrder, viewID };

		//event options
		RaiseEventOptions eventOptions = new RaiseEventOptions
		{
			Receivers = ReceiverGroup.All,
			CachingOption = EventCaching.AddToRoomCache
		};

		//send options

		SendOptions sendOptions = new SendOptions { Reliability = false };

		PhotonNetwork.RaiseEvent((byte)RaiseEventCode.WhoFinishedEventCode, data, eventOptions, sendOptions);
	}

	void OnWhoFinishedEventReceived(EventData eventData)
	{
		//data contents: string(nickName),int (finishOrder),int (viewID)
		if (eventData.Code != (byte)RaiseEventCode.WhoFinishedEventCode) return;

		object[] recData = (object[])eventData.CustomData;

		string nickNameOfFinshedPlayer = (string)recData[0];
		_finishOrder = (int)recData[1];
		int viewID = (int)recData[2];

		Debug.Log(nickNameOfFinshedPlayer + " " + _finishOrder);

		GameObject orderUITextObject = RacingGameManager.Instance.FinishOrderGameObjects[_finishOrder - 1];
		orderUITextObject.SetActive(true);

		if (viewID == photonView.ViewID)
		{
			//the player is me...
			orderUITextObject.GetComponent<Text>().text = _finishOrder + " - " + nickNameOfFinshedPlayer + " (YOU)";
			orderUITextObject.GetComponent<Text>().color = Color.red;
		}
		else
			orderUITextObject.GetComponent<Text>().text = _finishOrder + " - " + nickNameOfFinshedPlayer;
	}
	#endregion
}
