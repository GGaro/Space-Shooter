using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class EventManager : MonoBehaviour {
	public delegate void StartGameDelegate();
	public static StartGameDelegate onStartGame;
	public static StartGameDelegate onPlayerDeath;
	public static StartGameDelegate onRespawnPickup;

	public delegate void TakeDamageDelegate(float amt);
	public static TakeDamageDelegate onTakeDamage;

	public delegate void ScorePointsDelegate(int amt);
	public static ScorePointsDelegate onScorePoints;
	public static void StartGame()
	{
		if(onStartGame != null)
		{
			onStartGame(); 
		}
	}
	public static void ReSpawnPick()
	{
		if(onRespawnPickup != null)
		{
			onRespawnPickup(); 
		}
	}

	public static void TakeDamage(float dmgper)
	{
		if(onTakeDamage != null)
		{
			onTakeDamage(dmgper); 
		}
	}

	public static void PlayerDeath()
	{
		if(onPlayerDeath != null)
		{
			onPlayerDeath(); 
		}
	}

	public static void ScorePoints(int score)
	{
		if(onScorePoints != null)
		{
			onScorePoints(score); 
		}
	}

}
