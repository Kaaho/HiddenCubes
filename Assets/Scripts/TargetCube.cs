using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCube : MonoBehaviour {

    public GameObject shadowCube;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "DropPoint")
        {
            Vector3 position = shadowCube.transform.position;
            transform.position = position;
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            //Debug.Log("You won!");
            GameObject dude = GameObject.FindWithTag("dude");
            dude.GetComponent<Dude>().FoundCube();
            GameObject.Find("AudioPlayer").GetComponent<AudioController>().PlayCubeFound();
        }
    }

}
