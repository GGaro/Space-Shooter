using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerInput : MonoBehaviour {
	[SerializeField]Lasser[] laser;

	void Update()
	{
		
		if(Input.GetKeyDown(KeyCode.Space))
		{
			foreach(Lasser l in laser)
			{
			//	Vector3 pos = transform.position + (transform.forward * l.Distance);
				l.FireLaser();
			}
		}
	}

}
