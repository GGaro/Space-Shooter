using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Shield : MonoBehaviour {
	[SerializeField]int maxHealth = 10;
	[SerializeField]int curHealth;
	[SerializeField]float regenrate = 2f;
	[SerializeField]int regenamount = 1;


	void Start()
	{
		curHealth = maxHealth;
		InvokeRepeating("Regenerate", regenrate, regenrate);
	}

	public void Regenerate()
	{
		if(curHealth< maxHealth)
		{
			curHealth += regenamount;
		}
		if(curHealth > maxHealth)
		{
			curHealth = maxHealth;
		}
		EventManager.TakeDamage(curHealth/(float)maxHealth);
	}

	public void TakeDamage(int dmg = 1)
	{
		curHealth -= dmg;
		if(curHealth < 0)
		{
			curHealth = 0;
		}

		EventManager.TakeDamage(curHealth/(float)maxHealth);
		if(curHealth < 1)
		{
			EventManager.PlayerDeath();
			GetComponent<Explosion>().BlowUp();
        }
	}
}
