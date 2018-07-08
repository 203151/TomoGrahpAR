using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceCommands : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SelfDestroy()
    {
        Object.Destroy(gameObject);
    }
}
