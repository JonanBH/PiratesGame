using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PiratesGame.UI;

namespace PiratesGame.Entity
{
    public class BaseShip : MonoBehaviour
    {
        #region Fields

        [Header("Ship Settings")]
        [SerializeField]
        private float _maxHealth = 20;
        [SerializeField]
        private float _maxSpeed = 10;
        [SerializeField]
        private float _turningSpeed = 20;
        [SerializeField]
        private float _maxTurningAngle = 20;
        [SerializeField]
        private float _acceleration = 5;

        [Header("References")]
        [SerializeField]
        private GameObject[] _damageVisualTiers;
        [SerializeField]
        private GameObject _deadVisual;
        [SerializeField]
        private UIHealthBar _healthBar;

        private float _currentSpeed = 0;
        private float _currentTurningAngle = 0;
        private Vector3 _moveDirection = Vector3.up;
        private float _currentHealth;
        #endregion


        #region Lifecycle Methods
        void Start()
        {
            _currentHealth = _maxHealth;
            UpdateVisuals();
        }

        void Update()
        {
            Move(Time.deltaTime);
            UpdateMoveDirection();
        }

        #endregion

        #region Public Methods
        public void IncreaseCurrentSpeed()
        {
            _currentSpeed = Mathf.Min(_currentSpeed + _acceleration * Time.deltaTime, _maxSpeed);
        }

        public void DecreaseCurrentSpeed()
        {
            _currentSpeed = Mathf.Max(_currentSpeed - _acceleration * Time.deltaTime, 0);
        }

        public void IncreaseTurnAngle()
        {
            _currentTurningAngle = Mathf.Min(_currentTurningAngle + _turningSpeed * Time.deltaTime, _maxTurningAngle);
            UpdateMoveDirection();
        }

        public void DecreaseTurnAngle()
        {
            _currentTurningAngle = Mathf.Max(_currentTurningAngle - _turningSpeed * Time.deltaTime, -_maxTurningAngle);
            UpdateMoveDirection();
        }

        public void ResetAngle()
        {
            _currentTurningAngle = Mathf.Lerp(_currentTurningAngle, 0, 0.6f);

            if(Mathf.Abs(_currentTurningAngle) <= 0.5f)
            {
                _currentTurningAngle = 0;
            }
        }

        public void Move(float delta)
        {
            transform.position += _moveDirection * _currentSpeed * delta;
            transform.up = _moveDirection;

            ResetAngle();
        }

        public void TakeDamage(float amount)
        {
            _currentHealth = Mathf.Max(_currentHealth - amount, 0);
            UpdateVisuals();

            if (_currentHealth == 0)
            {
                Destroyed();
            }
        }

        #endregion

        #region Private Methods

        private void UpdateVisuals()
        {
            int index = Mathf.RoundToInt(Mathf.Lerp(_damageVisualTiers.Length - 1, 0, _currentHealth / _maxHealth));
            for (int i = 0; i < _damageVisualTiers.Length; i++)
            {
                _damageVisualTiers[i].SetActive(i == index);
            }

            _healthBar.UpdateHealthBar(_currentHealth / _maxHealth);
        }


        private void UpdateMoveDirection()
        {
            _moveDirection = transform.up;
            _moveDirection = Quaternion.AngleAxis(_currentTurningAngle * Time.deltaTime, Vector3.back) * _moveDirection;
            _moveDirection.Normalize();
        }

        [ContextMenu("Test Damage")]
        private void Take1Damage()
        {
            TakeDamage(1);
        }

        [ContextMenu("Destroy")]
        private void Explode()
        {
            TakeDamage(_maxHealth);
        }

        private void Destroyed()
        {
            for (int i = 0; i < _damageVisualTiers.Length; i++)
            {
                _damageVisualTiers[i].SetActive(false);
            }

            _deadVisual.SetActive(true);
        }
        #endregion
    }
}