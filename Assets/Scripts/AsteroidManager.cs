using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class AsteroidManager : MonoBehaviour {

	[SerializeField] Asteroids asteroidprefab;
    [SerializeField] GameObject pickupPrefab;
    [SerializeField] int numberOfAsteroids = 10;
	[SerializeField] int spacing = 100;
    List<Asteroids> asteroid = new List<Asteroids>();

    void OnEnable()
	{
		EventManager.onStartGame += PlaceAsteroids;
        EventManager.onPlayerDeath += DestroyAsteroid;
        EventManager.onRespawnPickup += PlacePickup;
    }

	void OnDisable()
	{
		EventManager.onStartGame -= PlaceAsteroids;
		EventManager.onPlayerDeath -= DestroyAsteroid;
		EventManager.onRespawnPickup -= PlacePickup;
	}
	private void PlaceAsteroids()
	{
		for(int x = 0; x < numberOfAsteroids; x++)
		{
				for(int y = 0; y < numberOfAsteroids; y++)
				{
					for(int z = 0; z < numberOfAsteroids; z++)
					{
						InstantiateAsteroid(x,y,z);
					}	
				}
			}
        PlacePickup();
    }

	void InstantiateAsteroid(int x, int y, int z)
	{
		Asteroids temp = Instantiate(asteroidprefab, new Vector3(transform.position.x + (x * spacing) + AsteroidOffset()
			,transform.position.y + (y * spacing) + AsteroidOffset()
			, transform.position.z + (z * spacing) + AsteroidOffset())
			, Quaternion.identity, transform) as Asteroids;

        asteroid.Add(temp);
    }

	void PlacePickup()
	{
        int rnd = Random.Range(0, asteroid.Count);
        Instantiate(pickupPrefab, asteroid[rnd].transform.position, Quaternion.identity);
        gameObject.tag = "PickUp";
        Destroy(asteroid[rnd].gameObject);
        asteroid.RemoveAt(rnd);
    }


	float AsteroidOffset()
	{
		return Random.Range(-spacing/2f, spacing/2f);
	}

	void DestroyAsteroid()
	{
		foreach(Asteroids ast in asteroid)
            {
				ast.SelfDestruct();
			}
        asteroid.Clear();
    }
}
