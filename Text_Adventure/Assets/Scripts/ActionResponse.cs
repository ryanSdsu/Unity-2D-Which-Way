using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionResponse : ScriptableObject {

	public bool breakableItem;
	public abstract bool DoActionResponse (GameController controller);
	public Color roomColor = Color.black;
	public bool changeRoomColor;
	public bool displayUseMessage;
	[TextArea]
	public string useMessage;

}
