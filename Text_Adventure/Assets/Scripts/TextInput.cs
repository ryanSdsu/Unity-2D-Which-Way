using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInput : MonoBehaviour {


	private string [] smartCombacks = new string[] {

		"Ha ha ha very funny (that did not work).",

		"That does not work.",

		"You got your degree where?",

		"Rest In Peace. :( ",

		"You're trying that again?",

		"Maybe you should just quit...",

		"I'm not being a bully but that was a mistake.",

		"Have you checked the help dialog?",

		"How about we take a break?",

		"Seriously. How about we take a break?",

		"That's not a valid command",

		"Nope.",

		"Maybe we should go get tacos.",

		"Don't forget pressing the / key will refresh the screen (i.e. still not right)",

		"Almost... ok not really.",

		"Take a break. And try not to type that again.",

		"Sorry. That was not a valid command.",

		"Sorry again... That command does not work.",

		"I didn't get that. What did you mean?",

		"There is a giant question mark on the top right of the screen. Try clicking that."



	};

	public InputField inputField;

	GameController controller;

	void Awake() {
		controller = GetComponent<GameController> ();
	}

	void Update() {
		if (Input.GetKeyDown (KeyCode.Return)) {
			AcceptStringInput(inputField.text);
		}
	}

	void AcceptStringInput(string userInput) {
	
		userInput = userInput.ToLower ();
		controller.LogStringWithReturn (userInput);

		char[] delimiterCharacters = { ' ' };
		string[] separatedInputWords = userInput.Split (delimiterCharacters);

		bool commandSuccess = false;
		for (int i = 0; i < controller.inputActions.Length; i++) {

			InputAction inputAction = controller.inputActions [i];
			if (inputAction.keyWord == separatedInputWords [0]) {
				inputAction.RespondToInput (controller, separatedInputWords);
				commandSuccess = true;
			}
		}

		if (commandSuccess == false) {
			issueSmartComebacks ();
		}

		InputComplete ();
	
	}

	void issueSmartComebacks() {

		int index = Random.Range (0, smartCombacks.Length);

		controller.LogStringWithReturn (smartCombacks[index]);

	}

	void InputComplete() {
	
		controller.DisplayLoggedText ();
		inputField.ActivateInputField ();
		inputField.text = null;
	}

}
