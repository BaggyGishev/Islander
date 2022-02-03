using System;
using Gisha.Islander.Photon;
using Gisha.Islander.UI;
using Photon.Pun;
using UnityEngine;

namespace Gisha.Islander.Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Player Settings")] [SerializeField]
        private float maxHealth = 100;

        [Header("Other")] [SerializeField] private float swimmingDamagePerSecond = 10;

        public Action Destroyed;

        private float _health;
        private FPSMover _fpsMover;
        private PhotonView _pv;

        private void Awake()
        {
            _pv = GetComponent<PhotonView>();
            _fpsMover = GetComponent<FPSMover>();
        }

        private void Start()
        {
            _health = maxHealth;
            UIManager.Instance.UpdateHealthBar(_health, maxHealth);
        }

        private void Update()
        {
            if (!_pv.IsMine)
                return;

            if (_fpsMover.IsSwimming)
                GetDamage(swimmingDamagePerSecond * Time.deltaTime);
        }

        public void GetDamage(float dmg)
        {
            _health -= dmg;

            if (_health < 0)
            {
                _health = 0;

                Destroyed?.Invoke();
            }

            UIManager.Instance.UpdateHealthBar(_health, maxHealth);
        }
    }
}