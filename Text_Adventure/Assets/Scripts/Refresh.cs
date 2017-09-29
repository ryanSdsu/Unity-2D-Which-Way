using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TextAdventure/InputActions/Refresh")]
public class Refresh : InputAction {

	public override void RespondToInput (GameController controller, string[] separatedInputWords)
	{
		controller.DisplayRoomText ();
	}

}
