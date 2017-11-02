using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public Text displayText;
	public Text soundText;
	public InputAction[] inputActions;
	public Animator StartGameAnimator;
	public Animator LoadLevelAnimator;
	public GameObject StartTextScreen;
	public GameObject DialogTextScreen;
	public InputField inputField;
	public Dictionary<string, string> inventoryDictionary = new Dictionary<string, string> ();

	[SerializeField]
	private GameObject CreditsTextScreen;
	[SerializeField]
	private Text RedButtonStartScreenText;


	[HideInInspector] public RoomNavigation roomNavigation;
	[HideInInspector] public List<string> interactionDescriptionsInRoom = new List<string> ();
	[HideInInspector] public InteractableItems interactableItems;
	// Use this for initialization
	public List<InteractableObject> resetRemoveItemsAtStartOfGameToBeFalse;
	public List<InteractableObject> resetRemoveItemsAtStartOfGameToBeTrue;
	public List<Room> resetDefaultRoomDescriptions;


	List<string> actionLog = new List<string> ();
	void Awake () {
		interactableItems = GetComponent<InteractableItems> ();
		roomNavigation = GetComponent<RoomNavigation> ();

		for (int i = 0; i < resetRemoveItemsAtStartOfGameToBeFalse.Count; i++) {
			resetRemoveItemsAtStartOfGameToBeFalse[i].removeItem = false;
			for (int j = 0; j < resetRemoveItemsAtStartOfGameToBeFalse[i].interactions.Length; j++) {
				resetRemoveItemsAtStartOfGameToBeFalse[i].interactions[j].currentTextResponse = resetRemoveItemsAtStartOfGameToBeFalse[i].interactions[j].originalTextResponse; 
			}
		}

		for (int i = 0; i < resetRemoveItemsAtStartOfGameToBeTrue.Count; i++) {
			resetRemoveItemsAtStartOfGameToBeTrue[i].removeItem = true;
			for (int j = 0; j < resetRemoveItemsAtStartOfGameToBeTrue[i].interactions.Length; j++) {
				resetRemoveItemsAtStartOfGameToBeTrue[i].interactions[j].currentTextResponse = resetRemoveItemsAtStartOfGameToBeTrue[i].interactions[j].originalTextResponse; 
			}
		}

		for (int i = 0; i < resetDefaultRoomDescriptions.Count; i++) {
			resetDefaultRoomDescriptions [i].description = resetDefaultRoomDescriptions [i].originalDescription;
		}

	}

		

	public void showCredits() {

		if (StartTextScreen.activeSelf){
			StartTextScreen.SetActive (false);
			DialogTextScreen.SetActive (false);
			CreditsTextScreen.SetActive (true);
			RedButtonStartScreenText.text = "Go Back";
		} else {
			StartTextScreen.SetActive (true);
			DialogTextScreen.SetActive (true);
			CreditsTextScreen.SetActive (false);
			RedButtonStartScreenText.text = "Credits";
		}
	}

	public void LoadLevel(string levelName) {

		SceneManager.LoadScene (levelName);

	}

	public void RestartGame () {

		SceneManager.LoadScene ("Main");

	}

	public void QuitGame () {

		Application.Quit ();

	}

	public void StartGame() {

		StartGameAnimator.SetBool ("IsActive", false);
		inputField.ActivateInputField();

	}
		

	void Start(){
		DisplayRoomText ();
		DisplayLoggedText ();
	}

	public void DisplayLoggedText() {
		string logAsText = string.Join ("\n", actionLog.ToArray ());

		displayText.text = logAsText;
	}

	public void DisplayRoomText()
	{
		ClearCollectionForNewRoom ();

		UnpackRoom ();

		string joinedInteractionDescriptions = string.Join ("\n", interactionDescriptionsInRoom.ToArray ());

		string combinedText = roomNavigation.currentRoom.description + "\n" + joinedInteractionDescriptions;

		if (!combinedText.Equals ("")) {
			LogStringWithReturn (combinedText);
		}
	}

	void UnpackRoom() {
		roomNavigation.UnpackExitsInRoom ();
		PrepareObjectsToTakeOrExamine (roomNavigation.currentRoom);
	}

	void PrepareObjectsToTakeOrExamine(Room currentRoom) {
	
		for (int i = 0; i < currentRoom.interactableObjectsInRoom.Length; i++) {

			string descriptionNotInInventory = interactableItems.GetObjectsNotInInventory (currentRoom, i);
			if (descriptionNotInInventory != null) {

				interactionDescriptionsInRoom.Add (descriptionNotInInventory);
			}
				

			InteractableObject interactableInRoom = currentRoom.interactableObjectsInRoom [i];

			for (int j = 0; j < interactableInRoom.interactions.Length; j++) {
				Interaction interaction = interactableInRoom.interactions [j];
				if (interaction.inputAction.keyWord == "examine" && !(interactableItems.nounsInInventory.Contains(interactableInRoom.noun)) && !(interactableInRoom.removeItem)) {
					interactableItems.examineDictionary.Add(interactableInRoom.noun, interaction.currentTextResponse);
				}

				if (interaction.inputAction.keyWord == "take") {
					interactableItems.takeDictionary.Add(interactableInRoom.noun, interaction.currentTextResponse);
	
				}

				Debug.Log ("Checking for Find");
				if (interaction.inputAction.keyWord == "find") {
					Debug.Log ("Find is found");
					Debug.Log (interactableInRoom.noun);
					Debug.Log (interaction.currentTextResponse);
					interactableItems.findDictionary.Add(interactableInRoom.noun, interaction.currentTextResponse);

				}

			}
		}
	}

	public string TestVerbDictionarywithNoun(Dictionary<string, string> verbDictionary, string TestVerbDictionarywithNoun, string noun) {

		if (verbDictionary.ContainsKey (noun)) {

			if (TestVerbDictionarywithNoun.Equals ("take")) {
				//Examine items in inventory
				string itemDescription = verbDictionary[noun];
				int lengthDescription = itemDescription.Length;
				itemDescription = itemDescription.Substring(itemDescription.IndexOf("the") + 4);
				string itemName = noun;
				itemDescription = itemName + " (" + itemDescription;
				inventoryDictionary.Add(itemName, itemDescription);
			}

			if (TestVerbDictionarywithNoun.Equals ("examine")) {
				interactableItems.Examine(noun);
			}

			return verbDictionary [noun];
		}



		return "You can't " + TestVerbDictionarywithNoun + " \"" + noun +"\"";
	}

	void ClearCollectionForNewRoom() {
		interactableItems.ClearCollections ();
		interactionDescriptionsInRoom.Clear ();
		roomNavigation.ClearExits ();
	}

	public void LogStringWithReturn(string stringToAdd) {
	
		actionLog.Add (stringToAdd + "\n");
	}

	public void PlayClip(string clipName) {
		//Create and audiosource and add it to the camera
		AudioSource a = Camera.main.gameObject.AddComponent<AudioSource>();

		//Assign the right clip to it
		a.clip = Resources.Load<AudioClip>("Audio/" + clipName);

		//Play the clip
		a.Play();

		//Destroy clip
		Destroy(a, a.clip.length);
	}

	public void ToggleSound() {
		if (soundText.text.Equals("Sound Off")){
			soundText.text = "Sound On";
			AudioListener.pause = false;
		} else {
			soundText.text = "Sound Off";
			AudioListener.pause = true;
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
