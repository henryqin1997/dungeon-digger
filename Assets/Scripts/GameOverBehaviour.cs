using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class GameOverBehaviour : MonoBehaviour
{
    public UnityEvent gameOverEvent;
    public const int GO_TO_MAIN_MENU_TIMEOUT_SECONDS = 3;

    public void GameOver()
    {
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
              { "survive time", Time.time },
              { "Current room", FindCurrentRoom() }
          });

        gameObject.SetActive(true);

        gameOverEvent.Invoke();

        Invoke("GoToMainMenu", GO_TO_MAIN_MENU_TIMEOUT_SECONDS);
    }

    private static string FindCurrentRoom()
    {
        Room currentRoom = Object.FindObjectOfType<Room>();
        Debug.Assert(currentRoom != null);

        return currentRoom.gameObject.name;
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
