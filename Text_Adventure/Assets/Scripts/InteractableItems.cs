using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableItems : MonoBehaviour {

	public List<InteractableObject> ActionResponsesInitList;

	public Dictionary<string, string> examineDictionary = new Dictionary<string, string> ();
	public Dictionary<string, string> takeDictionary = new Dictionary<string, string> ();
	public Dictionary<string, string> findDictionary = new Dictionary<string, string> ();


	public Image BackgroundOfRoom;

	[HideInInspector] public List<string> nounsInRoom = new List<string>();

	Dictionary <string,ActionResponse> useDictionary = new Dictionary<string, ActionResponse> ();
	public List<string> nounsInInventory = new List<string> ();
	GameController controller;

	void Awake() {

		controller = GetComponent<GameController> ();
	}

	public string GetObjectsNotInInventory(Room currentRoom, int i) {

		InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom [i];

		if (interactableInRoom.removeItem) {
			return null;
		}

		if (interactableInRoom.takeableItem) {
			nounsInRoom.Add (interactableInRoom.noun);
		}

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


				if (interaction.actionResponse == null) {
					Debug.Log ("actionresponse is null");
					continue;
				}
				//Debug.Log ("actionresponse is not null");
				Debug.Log (!(useDictionary.ContainsKey (noun)));
				Debug.Log (interaction.inputAction.keyWord.Equals("use"));
				Debug.Log (interaction.inputAction.keyWord.ToString());
				if (!useDictionary.ContainsKey (noun) && interaction.inputAction.keyWord.Equals("use")) {
					Debug.Log ("Added to dictionary");
					useDictionary.Add (noun, interaction.actionResponse);
				}
			}
		}
	}

	InteractableObject GetInteractableObjectfromUsableList(string noun) {

		for (int i = 0; i < ActionResponsesInitList.Count; i++) {

			if (ActionResponsesInitList[i].noun == noun) {
				return ActionResponsesInitList[i];
			}
		}
		return null;

	}

	public void DisplayInventory() {

		controller.LogStringWithReturn ("You look in your utility belt, you have: ");

		for (int i = 0; i < nounsInInventory.Count; i++) {

			controller.LogStringWithReturn(controller.inventoryDictionary[nounsInInventory [i]]);

		}
	}

	public void ClearCollections() {

		examineDictionary.Clear();
		takeDictionary.Clear ();
		findDictionary.Clear ();
		nounsInRoom.Clear();
	}
		

	public Dictionary<string, string> Take (string[] seperatedInputwords) {

		string noun = seperatedInputwords [1];

		if (nounsInRoom.Contains (noun)) {
			nounsInInventory.Add (noun);
			AddActionResponsesToUseDictionary ();
			nounsInRoom.Remove (noun);

			//Perform any action responses
			InteractableObject interactableObjectInInventory = GetInteractableObjectfromUsableList (noun);

			Debug.Log (noun);
			Debug.Log ("Over here");

			Debug.Log (interactableObjectInInventory.interactions[0]);

			Debug.Log (interactableObjectInInventory.interactions.Length);
			for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++) {

				Interaction interaction = interactableObjectInInventory.interactions [j];

				if (interaction.inputAction.keyWord.Equals("take")) {
					if (interaction.actionResponse == null)
						continue;
					else
						interaction.actionResponse.DoActionResponse(controller);
				}
			}

			return takeDictionary;
		} else {
			controller.LogStringWithReturn ("There is no \"" + noun + "\" here to take.");
			return null;
		}
	}


	public void Examine (string noun) {

			//Perform any action responses
			InteractableObject interactableObjectInInventory = GetInteractableObjectfromUsableList (noun);
			Debug.Log("examining examine action repsonses");
			for (int j = 0; j < interactableObjectInInventory.interactions.Length; j++) {

				Interaction interaction = interactableObjectInInventory.interactions [j];

				if (interaction.inputAction.keyWord.Equals("examine")) {
					if (interaction.actionResponse == null)
						continue;
					else
						interaction.actionResponse.DoActionResponse(controller);
				}
			}

		}

	public void UseItem(string[] separatedInputWords) {

		string nounToUse = separatedInputWords [1];

		if (nounsInInventory.Contains (nounToUse)) 
		{
			Debug.Log (nounToUse + " is in the nounsInInventory");
			if (useDictionary.ContainsKey (nounToUse)) 
			{
				Debug.Log (nounToUse + " is in the Dictionary: " + useDictionary[nounToUse]);
				bool actionResult = useDictionary [nounToUse].DoActionResponse (controller);
				Debug.Log (actionResult + " is the action result");

				if (!actionResult) {

					controller.LogStringWithReturn ("Hmmm. Nothing happens...");
				} else {

					//To destroy an item in inventory
					if (useDictionary [nounToUse].breakableItem) {
						nounsInInventory.Remove (nounToUse);
						controller.inventoryDictionary.Remove(nounToUse);
					}

					//To change an item in inventory
					if (useDictionary [nounToUse].changeRoomColor) {
						BackgroundOfRoom.color = useDictionary [nounToUse].roomColor;
					}

					if (useDictionary [nounToUse].displayUseMessage) {
						controller.LogStringWithReturn (useDictionary [nounToUse].useMessage);
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

