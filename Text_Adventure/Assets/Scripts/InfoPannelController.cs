using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPannelController : MonoBehaviour {

	public GameObject HelpPanel;
	public InputField inputField;

	public void toggleHelpPannel () {

		if (HelpPanel.activeSelf) {
			HelpPanel.SetActive(false);

			inputField.ActivateInputField();



		} else {
			HelpPanel.SetActive(true);
		}
	}


}
