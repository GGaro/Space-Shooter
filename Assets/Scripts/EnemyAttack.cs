using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyAttack : MonoBehaviour {
	[SerializeField]Transform target;
	[SerializeField]Lasser lasser;
    [SerializeField] float accuracy = 15.0f;
    

    Vector3 hitPos;

	void Update()
	{
		if(!FindTarget())
		{
			return;
		}
		InFront();
		HaveLineOfSightRaycast();
		if(InFront() && HaveLineOfSightRaycast())
		{
			FireLaser();
		}
	}

	bool InFront()
	{
		Vector3 directionToTarget = transform.position - target.position;
		float angle = Vector3.Angle(transform.forward, directionToTarget);
		if(Mathf.Abs(angle) > 90 && Mathf.Abs(angle) < 270)
		{
			Debug.DrawLine(transform.position, target.position, Color.green);
			return true;
		}

		Debug.DrawLine(transform.position, target.position, Color.yellow);
		return false;
	}

	bool HaveLineOfSightRaycast()
	{
		RaycastHit hit;

		Vector3 direction = target.position - transform.position;
		if(Physics.Raycast(lasser.transform.position, direction, out hit, lasser.Distance))
		{
			if(hit.transform.CompareTag("Player"))
			{
				Debug.DrawRay(lasser.transform.position , direction, Color.green);
				hitPos = new Vector3(hit.transform.position.x + Random.Range(-accuracy, accuracy), hit.transform.position.y + Random.Range(-accuracy, accuracy), hit.transform.position.z + Random.Range(-accuracy, accuracy));;
				return true;
			}
		}
		return false;

	}

	void FireLaser()
	{

		lasser.FireLaser(hitPos, target);
	}

	bool FindTarget()
	{
		if(target == null)
		{
			GameObject temp = GameObject.FindGameObjectWithTag("Player");
			if(temp!=null)	
			{
				target = temp.transform;
            }	
		}
		if(target == null)
		{
			return false;
		}
		return true;
	}

}
