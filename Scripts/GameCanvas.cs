using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour
{
    [SerializeField] private GameObject pauseCanvas;

    private GameObject[] _enemiesGameObject;
    
    private void Start() {
        _enemiesGameObject = GameObject.FindGameObjectsWithTag("Enemy");
    }
    
    public void PauseHandler() {
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

    public void DeleteEnemy() {
       foreach (GameObject enemyGameObject in _enemiesGameObject)
        {
            enemyGameObject.SetActive(!enemyGameObject.activeSelf);
        }
    }

}
