using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject levelCompleteCanvas;
    [SerializeField] private GameObject messageUI;

    private bool _isActivated = false;
    private GameObject _hint;
    private GameObject _questTxt;

    private void Start() {
        _hint = messageUI.transform.Find("Hint").gameObject;
        _questTxt = messageUI.transform.Find("Quest").gameObject;
    }

    public void Activate(){
        _isActivated = true;
        _questTxt.SetActive(false);
    }

    public void FinishLevel() {
        if (_isActivated){
            levelCompleteCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else {
            _questTxt.SetActive(true);
        }      
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")) {
            _hint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        _hint.SetActive(false);
    }
}
