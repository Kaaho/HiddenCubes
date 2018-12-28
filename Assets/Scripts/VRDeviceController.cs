using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class VRDeviceController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!Application.isEditor)
        {
            if (VRDeviceVariables.Device == "Magic Window") StartCoroutine(SetVRDevice("Cardboard", false));
            else StartCoroutine(SetVRDevice(VRDeviceVariables.Device, true));
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("StartScene");
        }

        if (!Application.isEditor && !UnityEngine.XR.XRSettings.enabled) Camera.main.GetComponent<Transform>().localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(XRNode.CenterEye);
	}

    private IEnumerator SetVRDevice(string device, bool vrEnabled)
    {
        Debug.Log(device);
        XRSettings.LoadDeviceByName(device);
        yield return null;
        if (!XRSettings.loadedDeviceName.Equals(device, System.StringComparison.OrdinalIgnoreCase))
        {
            VRDeviceVariables.DaydreamSupported = false;
            SceneManager.LoadScene("StartScene");
        }
        else if (vrEnabled)
        {
            XRSettings.enabled = vrEnabled;
        }
        else
        {
            yield return null;
            XRSettings.enabled = vrEnabled;
        }
    }

/*    public void ToggleCardboard()
    {
        if (!Application.isEditor)
        {
            UnityEngine.XR.XRSettings.enabled = !UnityEngine.XR.XRSettings.enabled;
        }
    }*/

}

public static class VRDeviceVariables
{
    private static string device = "Cardboard";
    private static bool daydreamsupported = true;

    public static string Device
    {
        get
        {
            return device;
        }
        set
        {
            device = value;
        }
    }

    public static bool DaydreamSupported
    {
        get
        {
            return daydreamsupported;
        }
        set
        {
            daydreamsupported = value;
        }
    }

}

