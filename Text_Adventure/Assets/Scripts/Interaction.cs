using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Interaction {

	public InputAction inputAction;
	[TextArea]
	public string currentTextResponse;
	[TextArea]
	public string originalTextResponse;
	public ActionResponse actionResponse;


}
