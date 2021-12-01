using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioSource HitSound;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private Image HealthBar;
    [SerializeField] private float totalHealth = 100;
    [SerializeField] private Animator _animator;

    private float _health;

    private void Start() {
        _health = totalHealth;
        InitHealth();
    }
    
    
    public void ReduceHealth(float damage) {
        _health -= damage;
        HitSound.Play();
        InitHealth();
        _animator.SetTrigger("takeDamage");
        if (_health <= 0) {
            Die();
        }
    }

    private void InitHealth() {
        HealthBar.fillAmount = _health / totalHealth;
    }

    private void Die() {
        _animator.SetTrigger("die");
        // PlayerController playerController = gameObject.GetComponent<PlayerController>();
        // playerController.gameObject.SetActive(false);
    }
}
