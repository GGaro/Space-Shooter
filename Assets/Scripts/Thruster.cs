using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
[RequireComponent(typeof(Light))]
[DisallowMultipleComponent]
public class Thruster : MonoBehaviour {
    TrailRenderer tr;
    Light playerLight;

	void Awake()
	{
		playerLight = GetComponent<Light>();
		tr = GetComponent<TrailRenderer>();
	}

	void Start()
	{
		playerLight.intensity = 0;
	}
		

	public void Intensity(float inten)
	{
		playerLight.intensity = inten * 0.5f;
	}
}
