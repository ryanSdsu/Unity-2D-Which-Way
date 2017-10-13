using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItems : MonoBehaviour {

	public List<InteractableObject> usableItemList;

	public Dictionary<string, string> examineDictionary = new Dictionary<string, string> ();
	public Dictionary<string, string> takeDictionary = new Dictionary<string, string> ();
	public Image BackgroundOfRoom;

	[HideInInspector] public List<string> nounsInRoom = new List<string>();

	Dictionary <string,ActionResponse> useDictionary = new Dictionary<string, ActionResponse> ();
	List<string> nounsInInventory = new List<string> ();
	GameController controller;

	void Awake() {

		controller = GetComponent<GameController> ();
	}

	public string GetObjectsNotInInventory(Room currentRoom, int i) {

		InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom [i];

		if (!nounsInInventory.Contains (interactableInRoom.noun) && !interactableInRoom.description.Equals("")) {

			nounsInRoom.Add (interactableInRoom.noun);
			return interactableInRoom.description;
		}

		return null;
	}

	public void AddActionResponsesToUseDictionary() {

		for (int i = 0; i < nounsInInventory.Count; i++) {

			string noun = nounsInInventory [i];

			InteractableObject interactableObjectInInventory = GetInteractableObjectfromUsableList (noun);
			if (interactableObjectInInventory == null)
				continue;

			for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++) {

				Interaction interaction = interactableObjectInInventory.interactions [j];

				if (interaction.actionResponse == null)
					continue;

				if (!useDictionary.ContainsKey (noun)) {
					useDictionary.Add (noun, interaction.actionResponse);
				}
			}
		}
	}

	InteractableObject GetInteractableObjectfromUsableList(string noun) {

		for (int i = 0; i < usableItemList.Count; i++) {

			if (usableItemList[i].noun == noun) {
				return usableItemList[i];
			}
		}
		return null;

	}

	public void DisplayInventory() {

		controller.LogStringWithReturn ("You look in your utility belt, you have: ");

		for (int i = 0; i < nounsInInventory.Count; i++) {

			controller.LogStringWithReturn (nounsInInventory [i]);
		}
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
			AddActionResponsesToUseDictionary ();
			nounsInRoom.Remove (noun);
			return takeDictionary;
		} else {
			controller.LogStringWithReturn ("There is no \"" + noun + "\" here to take.");
			return null;
		}
	}

	public void UseItem(string[] separatedInputWords) {

		string nounToUse = separatedInputWords [1];

		if (nounsInInventory.Contains (nounToUse)) 
		{

			if (useDictionary.ContainsKey (nounToUse)) 
			{

				bool actionResult = useDictionary [nounToUse].DoActionResponse (controller);
				if (!actionResult) {

					controller.LogStringWithReturn ("Hmmm. Nothing happens...");
				} else {

					//To destroy an item in inventory
					if (useDictionary [nounToUse].breakableItem) {
						nounsInInventory.Remove (nounToUse);
					}

					if (useDictionary [nounToUse].changeRoomColor) {
						BackgroundOfRoom.color = useDictionary [nounToUse].roomColor;
					}


				}

			} else {
				controller.LogStringWithReturn ("You can't use \"" + nounToUse +"\"");
			} 

		} 

		else {

			controller.LogStringWithReturn("There is no \"" + nounToUse + "\" in your inventory to use");
		
		}

	}

}

