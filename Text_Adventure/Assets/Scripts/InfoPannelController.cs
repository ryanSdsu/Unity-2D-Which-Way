using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPannelController : MonoBehaviour {

	public GameObject HelpPanel;
	public InputField inputField;

	public void toggleHelpPannel (GameController controller) {

		if (HelpPanel.activeSelf) {
			HelpPanel.SetActive(false);
			controller.PlayClip ("closeDrawer");


			inputField.ActivateInputField();



		} else {
			HelpPanel.SetActive(true);
			controller.PlayClip ("openDrawer");

		}
	}


}
