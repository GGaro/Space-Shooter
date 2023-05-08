using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Explosion))] 
public class Asteroids : MonoBehaviour {

	private Transform t;
	private Vector3 randomRotation;

	[SerializeField] float minScale = .8f;
	[SerializeField] float maxScale = 1.2f;
	[SerializeField] float rotationOffset = 50f;

    public static float destuctionDelay = 1.0f;

    void Awake()
	{
		t = transform;
	}

	void Start()
	{
		Vector3 scale = Vector3.zero;

		scale.x = Random.Range(minScale, maxScale);
		scale.y = Random.Range(minScale, maxScale);		
		scale.z = Random.Range(minScale, maxScale);

		t.localScale = scale;


		randomRotation.x = Random.Range(-rotationOffset, rotationOffset);
		randomRotation.y = Random.Range(-rotationOffset, rotationOffset);		
		randomRotation.z = Random.Range(-rotationOffset, rotationOffset);

	}


	void Update()
	{
		t.Rotate(randomRotation*Time.deltaTime);
	}

	public void SelfDestruct()
	{
        float timer = Random.Range(0, destuctionDelay);
        Invoke("GoBoom", timer);
    }

    public void GoBoom()
    {
            GetComponent<Explosion>().BlowUp();
    }
}
