using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapController : MonoBehaviour
{
	#region Fields & Properties

	List<GameObject> _lapTriggers = new List<GameObject>();

	#endregion

	#region Getters


	#endregion

	#region MonoBehaviour Methods

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
	}
	#endregion
}
