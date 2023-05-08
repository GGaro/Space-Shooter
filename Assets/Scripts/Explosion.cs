using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Explosion : MonoBehaviour {
	[SerializeField]GameObject explosion;
	[SerializeField]GameObject blowup;
	[SerializeField]Rigidbody rigidBody;
	[SerializeField]float laserHitModifier = 50f;
	[SerializeField]Shield shield;

	void IveBeenHit(Vector3 pos)
	{
		GameObject go = Instantiate(explosion, pos, Quaternion.identity, transform);
		Destroy(go, 6f);

		if(shield == null)
		{
			return;
		}
        if (Random.Range(0, 5) == 3)
        {
            shield.TakeDamage();
        }
    }

	void OnCollisionEnter(Collision collision)
	{
		foreach(ContactPoint contact in collision.contacts)
		{
			IveBeenHit(contact.point);
		}
	}

	public void AddForce(Vector3 hitPosition, Transform hitSource)
	{
		IveBeenHit(hitPosition);
		if(rigidBody == null)
		{
			return;
		}
		Vector3 direction =(hitSource.position - hitPosition).normalized;
		rigidBody.AddForceAtPosition(-direction * laserHitModifier, hitPosition, ForceMode.Impulse);
	}
	public void BlowUp()
	{
    	GameObject temp = Instantiate(blowup, transform.position, Quaternion.identity) as GameObject;

		Destroy(gameObject);
	}
}
