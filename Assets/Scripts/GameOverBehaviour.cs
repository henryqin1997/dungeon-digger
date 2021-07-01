using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.Analytics;

public class GameOverBehaviour : MonoBehaviour
{
    public UnityEvent gameOverEvent;

    public void GameOver()
    {
        Analytics.CustomEvent("gameOver", new Dictionary<string, object>
          {
              { "survive time", Time.time }
          });

        gameObject.SetActive(true);

        RoomsEnteredTracker.SendRoomsEntered();

        gameOverEvent.Invoke();
    }

    public void goToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
