using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EnemyMovement : MonoBehaviour {
	[SerializeField]Transform target;
	[SerializeField]float movementspeed = 10f;
	[SerializeField]float ratationalDamp = 0.5f;
	[SerializeField]float detectionDistance = 20f;
	[SerializeField]float rayCastOffset = 2.5f;


	void OnEnable()
	{
        EventManager.onPlayerDeath += FindMainCamera;
		EventManager.onStartGame += SelfDestruct;
    }

	void OnDisable()
	{
        EventManager.onPlayerDeath -= FindMainCamera;
		EventManager.onStartGame -= SelfDestruct;
    }

	void SelfDestruct()
	{
		Destroy(gameObject);
	}

	void Update()
	{
		if(!FindTarget())
		{
			return;
		}
		PathFinding();
		Move();
        Bounds();
    }

	void Turn()
	{
		Vector3 pos = target.position - transform.position;
		Quaternion rotation = Quaternion.LookRotation(pos);
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, ratationalDamp * Time.deltaTime);
	}

	void Move()
	{
		transform.position += transform.forward * movementspeed * Time.deltaTime;
	}

	void PathFinding()
	{
		RaycastHit hit;
		Vector3 raycastOffsetV = Vector3.zero;

		Vector3 left = transform.position - transform.right * rayCastOffset;
		Vector3 right = transform.position + transform.right * rayCastOffset;
		Vector3 up = transform.position + transform.up * rayCastOffset;
		Vector3 down = transform.position - transform.up * rayCastOffset;

		Debug.DrawRay(left, transform.forward * detectionDistance, Color.cyan);
		Debug.DrawRay(right, transform.forward * detectionDistance, Color.cyan);
		Debug.DrawRay(up, transform.forward * detectionDistance, Color.cyan);
		Debug.DrawRay(down, transform.forward * detectionDistance, Color.cyan);

		if(Physics.Raycast(left, transform.forward, out hit, detectionDistance))
		{
			raycastOffsetV += Vector3.right;
		}
		else if(Physics.Raycast(right, transform.forward, out hit, detectionDistance))
		{
			raycastOffsetV -= Vector3.right;
			
		}

		if(Physics.Raycast(up, transform.forward, out hit, detectionDistance))
		{
			raycastOffsetV += Vector3.up;
		}
		else if(Physics.Raycast(down, transform.forward, out hit, detectionDistance))
		{
			raycastOffsetV -= Vector3.up;
		}

		if(raycastOffsetV != Vector3.zero)
		{
			transform.Rotate(raycastOffsetV * 5f * Time.deltaTime);
		}
		else
		{
			Turn();
		}

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

	void FindMainCamera()
	{
		target = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}

	private void Bounds()
	{
		if(transform.position.x > 550 || transform.position.y > 550 || transform.position.z > 550 )
		{
            if (transform.position.x > 550)
            {
                transform.position = new Vector3(-490, transform.position.y, transform.position.z);
            }
			else if (transform.position.y > 550)
            {
                transform.position = new Vector3(transform.position.x, -490, transform.position.z);
            }
			else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, -490);
            }
        }
		else if(transform.position.x < -550 || transform.position.y < -550 || transform.position.z < -550 )
		{
            if (transform.position.x < -550)
            {
                transform.position = new Vector3(490, transform.position.y, transform.position.z);
            }
			else if (transform.position.y < -550)
            {
                transform.position = new Vector3(transform.position.x, 490, transform.position.z);
            }
			else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, 490);
            }
        }
	}
}
