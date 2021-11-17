using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform enemyModelTransform;
    [SerializeField] private Animator animator;
    [SerializeField] private float walkDistance = 6;
    [SerializeField] private float patrolSpeed = 1;
    [SerializeField] private float ChasingSpeed = 3;
    [SerializeField] private float timeToWait = 5;
    [SerializeField] private float timeToChase = 3;
    [SerializeField] private float TimeToDie = 5;
    [SerializeField] private Canvas enemyCanvas;

    private Rigidbody2D _rb;
    private Transform _playerTransform;
    private Vector2 _leftBoundaryPosition;
    private Vector2 _rightBoundaryPosition;
    private Vector2 _nextPoint;
    private CapsuleCollider2D _capsuleCollider2D;
    
    private bool _isFacingRight = true; 
    private bool _isWait = false;
    private bool _isDie = false;
    private bool _isChasingPlayer;
    private bool _isColliderWithPlayer;
    private bool _isColliderWithEnemyBoundary;

    private float _walkSpeed;
    private float _waitTime;
    private float _chaseTime;

    public bool isFacingRight {
        get => _isFacingRight;
    }

    public void StartChasingPlayer() {
        _isChasingPlayer = true;
        _chaseTime = timeToChase;
        _walkSpeed = ChasingSpeed;
    }

    private void Start() {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _rb = GetComponent<Rigidbody2D>();
        _leftBoundaryPosition = transform.position;
        _rightBoundaryPosition = _leftBoundaryPosition + Vector2.right * walkDistance;
        _waitTime = timeToWait;
        _chaseTime = timeToChase;
        _walkSpeed = patrolSpeed;
        
    }

    private void Update() {
        if (_isChasingPlayer) {
            StartChasingTimer();
        }
        
        if(_isWait && !_isChasingPlayer)  {
            StartWaitTimer();
        }
        
        if(ShouldWait() || _isColliderWithEnemyBoundary) {
            _isWait = true;
        }

        if (_isDie) {
            TimeToDier();
        }
        
       
    }

    private void FixedUpdate() {
        if (!_isDie) {
            _nextPoint = Vector2.right * _walkSpeed * Time.fixedDeltaTime;
            //if (_isChasingPlayer && Mathf.Abs(DistanceToPlayer()) < minDistanceToPlayer)
            if (_isChasingPlayer && _isColliderWithPlayer) {
                return;
            }

            if (_isChasingPlayer) {
                ChasePlayer();
                animator.SetTrigger("run");
            } else if (!_isWait) {
                Patrol();
                animator.SetTrigger("walk");
            } else {
                animator.SetTrigger("idle");
            }
        }       
    }

    private void Patrol() {
        if(!_isFacingRight) {
            _nextPoint.x *= -1;
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private void ChasePlayer() {
        float distance = DistanceToPlayer();        
        if (distance < 0) {
            _nextPoint.x *= -1;
        }
        if (distance > 0.2f && !_isFacingRight) {
            Flip();
        } else if (distance < 0.2f && isFacingRight) {
            Flip();
        }
        _rb.MovePosition((Vector2)transform.position + _nextPoint);
    }

    private float DistanceToPlayer() {
        return _playerTransform.position.x - transform.position.x; 
    }

    private void StartWaitTimer() {
        _waitTime -= Time.deltaTime;

        if(_waitTime < 0) {
            _isWait = false;
            _waitTime = timeToWait;
            _isWait = false;
            _isColliderWithEnemyBoundary = false;
            Flip();
        }
    }

    private void StartChasingTimer() {
        _chaseTime -= Time.deltaTime;

        if(_chaseTime < 0) {
            _isChasingPlayer = false;
             _chaseTime = timeToChase;
             _walkSpeed = patrolSpeed;
        }
    }

    private bool ShouldWait() {
        bool isOutOfRightBoundary = _isFacingRight && transform.position.x >= _rightBoundaryPosition.x;
        bool isOutOfLeftBoundary = !_isFacingRight && transform.position.x <= _leftBoundaryPosition.x;

        return isOutOfLeftBoundary || isOutOfRightBoundary;
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_leftBoundaryPosition, _rightBoundaryPosition);
    }

     void Flip() {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = enemyModelTransform.localScale;
        playerScale.x *= -1;
        enemyModelTransform.localScale = playerScale;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            _isColliderWithPlayer = true;
        }

        if (other.gameObject.CompareTag("EnemyBoundary")) {
            _isColliderWithEnemyBoundary = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        PlayerController player = other.gameObject.GetComponent<PlayerController>();
        if (player != null) {
            _isColliderWithPlayer = false;
        }
    }

    public void Die() {
        _isDie = true;
        gameObject.layer = 13;    
        enemyCanvas.gameObject.SetActive(false);
        // _capsuleCollider2D.isTrigger = true; 
        // _rb.bodyType = RigidbodyType2D.Kinematic;
        // gameObject.GetComponent<EnemyAttack>()._die = true;
        
    }

    private void TimeToDier() {
        TimeToDie -= Time.deltaTime;

        if(TimeToDie < 0) {
            gameObject.SetActive(false);
        }
    }
}