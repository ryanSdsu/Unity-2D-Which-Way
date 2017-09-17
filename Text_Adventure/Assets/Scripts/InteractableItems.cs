using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItems : MonoBehaviour {

	public Dictionary<string, string> examineDictionary = new Dictionary<string, string> ();
	public Dictionary<string, string> takeDictionary = new Dictionary<string, string> ();


	[HideInInspector] public List<string> nounsInRoom = new List<string>();

	List<string> nounsInInventory = new List<string> ();
	GameController controller;

	void Awake() {

		controller = GetComponent<GameController> ();
	}

	public string GetObjectsNotInInventory(Room currentRoom, int i) {

		InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom [i];

		if (!nounsInInventory.Contains (interactableInRoom.noun)) {

			nounsInRoom.Add (interactableInRoom.noun);
			return interactableInRoom.description;
		}

		return null;
	}

	public void ClearCollections() {

		examineDictionary.Clear();
		takeDictionary.Clear ();
		nounsInRoom.Clear();
	}

	public Dictionary<string, string> Take (string[] seperatedInputwords) {

		string noun = seperatedInputwords [1];

		if (nounsInRoom.Contains (noun)) {
			nounsInInventory.Add (noun);
			nounsInRoom.Remove (noun);
			return takeDictionary;
		} else {
			controller.LogStringWithReturn ("There is no " + noun + " here to take.");
			return null;
		}
	}

}
