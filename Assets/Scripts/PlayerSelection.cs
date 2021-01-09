using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelection : MonoBehaviour
{
	#region Fields & Properties

	public GameObject[] SelectablePlayers;
	public int PlayerSelectionNumber;

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

	void Start() 
	{
		PlayerSelectionNumber = 0;
		ActivatePlayer(PlayerSelectionNumber);
	}
	#endregion

	#region Public Methods

	public void NextPlayer()
	{
		PlayerSelectionNumber++;

		if (PlayerSelectionNumber >= SelectablePlayers.Length)
			PlayerSelectionNumber = 0;

		ActivatePlayer(PlayerSelectionNumber);
	}

	public void PrevPlayer()
	{
		PlayerSelectionNumber--;

		if (PlayerSelectionNumber < 0)
			PlayerSelectionNumber = SelectablePlayers.Length - 1;

		ActivatePlayer(PlayerSelectionNumber);
	}
	#endregion

	#region Private Methods

	void ActivatePlayer(int x)
	{
		foreach(GameObject player in SelectablePlayers)
		{
			player.SetActive(false);
		}

		SelectablePlayers[x].SetActive(true);
	}
	#endregion
}
