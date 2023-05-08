using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerCamera : MonoBehaviour {
	[SerializeField] Transform player;
	[SerializeField]Vector3 distance = new Vector3(0f, 2f, -5f);
	[SerializeField]float distanceDamp = 0.2f;
	Vector3 velocity = Vector3.one;

	private Transform t;
	private float defaultDamp;


	void Awake()
	{
		t = transform;
        defaultDamp = distanceDamp;
    }

	void LateUpdate()
	{
		if(!FindTarget())
		{
			Invoke("DeleteParticle",3f);
			return;
		}
		SmoothFollow();
	}

	bool FindTarget()
	{
		if(player == null)
		{
			GameObject temp = GameObject.FindGameObjectWithTag("Player");
			if(temp!=null)	
			{
				player = temp.transform;
			}	
		}
		if(player == null)
		{
			return false;
		}
		return true;
	}

	void SmoothFollow()
	{
        CinematicReentry();
        Vector3 toPos = player.position + (player.rotation * distance);
		Vector3 curPos = Vector3.SmoothDamp(t.position, toPos, ref velocity, distanceDamp);
        t.position = curPos;
		 
		t.LookAt(player, player.up);	
	}

	public void DeleteParticle()
	{
		Destroy(GameObject.FindGameObjectWithTag("Explosion"));
	}

	private void CinematicReentry()
	{
		if(player.position.x > 470 || player.position.y > 470 || player.position.z > 470 || player.position.x < -470 || player.position.y < -470 || player.position.z < -470)
		{
            distanceDamp = 1f;
        }
		else
		{
            distanceDamp = defaultDamp;
        }
	}
}
