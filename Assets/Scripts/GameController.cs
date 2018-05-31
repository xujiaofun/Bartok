using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entitas;
using Bartok;

public class GameController : MonoBehaviour {

    Systems _system;

	// Use this for initialization
	void Start () {
        var context = Contexts.sharedInstance;

        _system = new ProspectorSystems(context);

        _system.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        _system.Execute();
        _system.Cleanup();
	}

    void OnDestroy() {
        _system.TearDown();
    }
}
