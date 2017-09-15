using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Text displayText;

	[HideInInspector] public RoomNavigation RoomNavigation;
	// Use this for initialization

	List<string> actionLog = new List<string> ();
	void Awake () {
		RoomNavigation = GetComponent<RoomNavigation> ();
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
		string combinedText = RoomNavigation.currentRoom.description + "\n";
		LogStringWithReturn (combinedText);
	}

	public void LogStringWithReturn(string stringToAdd) {
	
		actionLog.Add (stringToAdd + "\n");
	}

	// Update is called once per frame
	void Update () {
		
	}
}
