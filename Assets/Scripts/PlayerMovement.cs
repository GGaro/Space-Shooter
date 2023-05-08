using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PlayerMovement : MonoBehaviour {

	private Transform t;

	[SerializeField] float movementspeed = 50f;
	[SerializeField] float rotationspeed = 60f;
	[SerializeField] Thruster[] thruster;
    bool speeding = false;
    float dSpeed;


    void Awake()
	{
		t = gameObject.transform;
		dSpeed = movementspeed;
    }

	void Update()
	{
		Thrust();
		Turn();
        Bounds();
    }


	private void Thrust()
	{
			
		if(Input.GetAxis("Vertical") > 0)
		{
			foreach(Thruster tr in thruster)
			{
				tr.Intensity(Input.GetAxis("Vertical"));
				t.position += t.forward * movementspeed * Input.GetAxis("Vertical") * Time.deltaTime;
			}
		}

		if(Input.GetKey(KeyCode.LeftShift))
		{
            if (movementspeed < 25)
            {
                movementspeed = movementspeed * 2 * (Time.deltaTime/3) + movementspeed;
            }
			else
			{
                movementspeed = 25f;
            }
            speeding = true;
        }
		if(Input.GetKeyUp(KeyCode.LeftShift))
		{
            movementspeed = dSpeed;
            speeding = false;
        }

	}

	private void Turn()
	{
		float roll = rotationspeed * Input.GetAxis("Horizontal") * Time.deltaTime;
        float pitch = 0;
        float rot = 0;
        if (!Input.GetMouseButton(1) && !speeding)
        {
            pitch = rotationspeed * 10 * Input.GetAxis("Mouse Y") * Time.deltaTime;
        	rot = rotationspeed * 10 * Input.GetAxis("Mouse X") * Time.deltaTime;
        }
        t.Rotate(-pitch,rot,roll);


	}
 
	private void Bounds()
	{
		if(t.position.x > 550 || t.position.y > 550 || t.position.z > 550 )
		{
            if (t.position.x > 550)
            {
                t.position = new Vector3(-490, t.position.y, t.position.z);
            }
			else if (t.position.y > 550)
            {
                t.position = new Vector3(t.position.x, -490, t.position.z);
            }
			else
            {
                t.position = new Vector3(t.position.x, t.position.y, -490);
            }
        }
		else if(t.position.x < -550 || t.position.y < -550 || t.position.z < -550 )
		{
            if (t.position.x < -550)
            {
                t.position = new Vector3(490, t.position.y, t.position.z);
            }
			else if (t.position.y < -550)
            {
                t.position = new Vector3(t.position.x, 490, t.position.z);
            }
			else
            {
                t.position = new Vector3(t.position.x, t.position.y, 490);
            }
        }
	}
		
}
