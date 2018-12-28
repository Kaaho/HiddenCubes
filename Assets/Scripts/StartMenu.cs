using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour {
    public Button DayDreamButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //And let's quit the app!
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    public void LoadScene(string mode)
    {
        //Load Scene with specified mode
        VRDeviceVariables.Device = mode;
        Debug.Log("Starting with " + VRDeviceVariables.Device);
        SceneManager.LoadScene("Main");
    }

    private void OnEnable()
    {
        if (!VRDeviceVariables.DaydreamSupported && DayDreamButton!=null) DayDreamButton.interactable = false;
    }


}
