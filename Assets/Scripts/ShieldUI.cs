using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ShieldUI : MonoBehaviour {
	[SerializeField]RectTransform barRectTransform;
	float maxWidth;


	void Awake()
	{
		maxWidth = barRectTransform.rect.width;
	}

	void OnEnable()
	{
		EventManager.onTakeDamage += UpdateShieldDisplay;
	}

	void OnDisable()
	{
		EventManager.onTakeDamage -= UpdateShieldDisplay;
	}


	void UpdateShieldDisplay(float percent)
	{
		barRectTransform.sizeDelta = new Vector2(maxWidth * percent, 10f);
	}
}
