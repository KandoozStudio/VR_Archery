﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] [ImageEffectAllowedInSceneView]
public class PlayerPositionUpdater : MonoBehaviour {
    // Use this for initialization
    public Transform player;
    private Material[] _ground;

	void Start () {
        _ground= this.gameObject.GetComponent<Renderer>().sharedMaterials;
	}
	
	// Update is called once per frame
	void Update () {
        _ground[1].SetVector("_Playerposition", new Vector2(-player.transform.position.x, -player.transform.position.z));
	}
}
