using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float damage = 20;
    [SerializeField] private float timeToDamage = 0.9f;

    private float _damageTime;
    private bool _isDamage = true;    
    private Animator _animator; 

    private void Start() {
        _damageTime = timeToDamage;
        _animator = GetComponent<Animator>();
    }

    private void Update() {
        if (!_isDamage) {
            _damageTime -= Time.deltaTime;
            if (_damageTime <= 0) {
                _isDamage = true;
                _damageTime = timeToDamage;
            }
        }
    }
    
    private void OnCollisionStay2D(Collision2D other) {
        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();

        if (playerHealth != null && _isDamage) {
            _animator.SetTrigger("attack");
            playerHealth.ReduceHealth(damage);
            _isDamage = false;
        }
    }
}
