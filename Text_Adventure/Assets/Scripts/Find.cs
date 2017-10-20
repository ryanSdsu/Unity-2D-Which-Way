using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Find")]
public class Find : InputAction {

	public override void RespondToInput (GameController controller, string[] separatedInputWords)
	{
		controller.LogStringWithReturn (controller.TestVerbDictionarywithNoun (controller.interactableItems.findDictionary, separatedInputWords [0], separatedInputWords [1]));
	}

}
