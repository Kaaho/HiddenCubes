using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    public AudioClip click;
    public AudioClip cubeFound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnable()
    {
        AudioClicker.OnClicked += PlayClick;
    }

    void OnDisable()
    {
        AudioClicker.OnClicked -= PlayClick;
    }

    private void PlayClick()
    {
        GetComponent<AudioSource>().clip = click;
        GetComponent<AudioSource>().Play();
    }

    public void PlayCubeFound()
    {
        GetComponent<AudioSource>().clip = cubeFound;
        GetComponent<AudioSource>().Play();
    }

    public IEnumerator LoadDevice(string newDevice)
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
    }
    }
