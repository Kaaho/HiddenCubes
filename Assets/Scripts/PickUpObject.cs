using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {

    public Vector3 carryPosition;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        GameObject dude = GameObject.FindWithTag("dude");
        if (Vector3.Distance(dude.transform.position, transform.position) < 1)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 2;
        }
    }

    public void PickUpObjectClicked()
    {
        GameObject dude = GameObject.FindWithTag("dude");
        dude.GetComponent<Dude>().SetCarryObject(gameObject);
    }

}
