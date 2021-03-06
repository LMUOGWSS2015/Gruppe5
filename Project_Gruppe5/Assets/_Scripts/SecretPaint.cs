﻿using UnityEngine;
using System.Collections;

public class SecretPaint : MonoBehaviour {
	public float tilingX = 1;
	public float tilingY = 1;
	public float stageWidth= 1;
	public float stageHeight= 1;
    private Renderer rend;
    void Start() {
        rend = GetComponent<Renderer>();
    }
    void Update() {
		Vector3 vec = GameObject.FindGameObjectWithTag ("Light").gameObject.transform.position;
		Vector2 vec2 = new Vector2 (-vec.x / stageWidth * tilingX - tilingX/2 + 0.5f, -vec.z / stageHeight *tilingY - tilingY/2 + 0.5f);
		
		//Debug.Log ("Do:"+rend.material.GetTextureOffset ("_Mask")+","+vec);
        rend.material.SetTextureOffset("_Mask", vec2);
    }
} 