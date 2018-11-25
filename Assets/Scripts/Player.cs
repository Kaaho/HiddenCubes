using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Player : MonoBehaviour {
    public GameObject currentCameraCylinder;
    public GameObject[] infos;
    public GameObject[] cardboardTogglers;

    public Sprite[] dudeinfos;
    public Sprite[] dudeinfos_nocardboard;
    public Sprite[] cardboardOnOff;
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (!Application.isEditor && !UnityEngine.XR.XRSettings.enabled) Camera.main.GetComponent<Transform>().localRotation = UnityEngine.XR.InputTracking.GetLocalRotation(XRNode.CenterEye);
    }

    public void ToggleCardboard()
    {
        if (!Application.isEditor)
        {
            UnityEngine.XR.XRSettings.enabled = !UnityEngine.XR.XRSettings.enabled;
            if (UnityEngine.XR.XRSettings.enabled)
            {
                infos[0].GetComponent<SpriteRenderer>().sprite = dudeinfos[0];
                infos[1].GetComponent<SpriteRenderer>().sprite = dudeinfos[1];
                foreach (GameObject go in cardboardTogglers) go.GetComponent<SpriteRenderer>().sprite = cardboardOnOff[0];
            } else
            {
                infos[0].GetComponent<SpriteRenderer>().sprite = dudeinfos_nocardboard[0];
                infos[1].GetComponent<SpriteRenderer>().sprite = dudeinfos_nocardboard[1];
                foreach (GameObject go in cardboardTogglers) go.GetComponent<SpriteRenderer>().sprite = cardboardOnOff[1];
            }
        }
    }
/*
    public void StartGame(int mode)
    {
        UnityEngine.XR.XRSettings.enabled = true;
        StartCoroutine(LoadCardBoard(mode==0));
    }

    private IEnumerator LoadCardBoard(bool enable)
    {
        UnityEngine.XR.XRSettings.LoadDeviceByName("cardboard");
        yield return null;
        UnityEngine.XR.XRSettings.enabled = true;
    }

    */
    }
