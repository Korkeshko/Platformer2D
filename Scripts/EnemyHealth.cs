using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private float totalHealth = 100;

    private Animator _animator;
    private float _health;

    private void Start() {
        _health = totalHealth;
        _animator = GetComponent<Animator>();
        InitHealth();
    }

    public void ReduceHealth(float damage) {
        _health -= damage;
        InitHealth();
        _animator.SetTrigger("takeDamage");
        if (_health <= 0) {
            Die();
        }
    }

    private void InitHealth() {
        healthSlider.value = _health / totalHealth;
    }

    private void Die() {
        _animator.SetTrigger("die");
    }
}
