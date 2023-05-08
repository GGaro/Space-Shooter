using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Light))]
[DisallowMultipleComponent]
public class Lasser : MonoBehaviour {
	LineRenderer lr;
	Light playerLight;

	bool canFire = true;
	[SerializeField] float laserOffTime = 0.5f;
	[SerializeField] float maxDistance = 500f;
	[SerializeField] float delay = 2f;

	void Awake()
	{
		lr = GetComponent<LineRenderer>();
		playerLight = GetComponent<Light>();
	}

	void Start()
	{
		lr.enabled = false;
		playerLight.enabled = false;
		canFire = true;
	} 

	void Update()
	{
		Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance);	
	}

	Vector3 CastRay()
	{
		RaycastHit hit;
		Vector3 fwd = transform.TransformDirection(Vector3.forward) * maxDistance;
		if(Physics.Raycast(transform.position, fwd, out hit))
		{
			Debug.Log(hit.transform.name);
			spawnExplosion(hit.point, hit.transform);
			if(hit.transform.CompareTag("PickUp"))
			{
                Debug.Log("Laser Hit");
                hit.transform.GetComponent<PickUps>().PickUpHit();
            }
			return hit.point;
		}
		return  transform.position + (transform.forward * maxDistance);
	}

	void spawnExplosion(Vector3 hitPosition, Transform target)
	{
		Explosion temp = target.GetComponent<Explosion>();
		if(temp != null)
		{
			temp.AddForce(hitPosition,transform);
		}
	}

	public void FireLaser()
	{
		Vector3 pos = CastRay();
		FireLaser(pos);
	}

	public void FireLaser(Vector3 targetPosition, Transform target = null)
	{
		if(canFire)
		{
			if(target != null)
			{
				spawnExplosion(targetPosition,target);
                if(target.CompareTag("Player") &&FindObjectOfType<PlayerScore>().getScore() > 0)
				{
					EventManager.ScorePoints(-50);
				}
			}
			lr.SetPosition(0, transform.position);
			lr.SetPosition(1, targetPosition);
			lr.enabled = true;
			playerLight.enabled = true;
			canFire = false;
			Invoke("TurnOffLaser", laserOffTime);
			Invoke("CanFire", delay);
		}
	}

	void TurnOffLaser()
	{
		lr.enabled = false;
		playerLight.enabled = false;
	}

	public float Distance
	{
		get{return maxDistance;}
	}

	void CanFire()
	{
		canFire = true;
	}
}
