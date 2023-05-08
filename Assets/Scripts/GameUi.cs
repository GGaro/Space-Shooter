using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class GameUi : MonoBehaviour {
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject gameUi;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] GameObject playerStartPosition;

    void Start()
	{
        DelayMainMenuDisplay();
    }

	void OnEnable()
	{
		EventManager.onStartGame += ShowGameUi;
        EventManager.onPlayerDeath += ShowMainMenu;
    }

	void OnDisable()
	{
		EventManager.onStartGame -= ShowGameUi;
		EventManager.onPlayerDeath -= ShowMainMenu;
	}

	void ShowMainMenu()
	{
        Invoke("DelayMainMenuDisplay", Asteroids.destuctionDelay * 3f);
    }

    void DelayMainMenuDisplay()
    {
        mainMenu.SetActive(true);
        gameUi.SetActive(false);
        
    }

    

    void ShowGameUi()
	{
        mainMenu.SetActive(false);
        gameUi.SetActive(true);
		Instantiate(playerPrefab, playerStartPosition.transform.position, playerStartPosition.transform.rotation);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

	public void PlayGame()
	{
		EventManager.StartGame();
	}
}
