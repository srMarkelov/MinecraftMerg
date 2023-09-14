
/*
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Characters.Std
{
    public class AxeCharacter : ICharacter
    {
        public CharacterType CharacterType => CharacterType.Axe;
        public DamageType DamageType { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float DamageSpeed { get; set; }
        public float MovementSpeed { get; set; }
        public float TimeLastDamage { get; set; }
        public int KillReward { get; set; }
        private CharacterView _view;
        private float MaxHealth;
        public CharacterView View
        {
            get => _view;
            set
            {
                _view = value;
                Position = _view.transform.position;
            }
        }
        public List<ICharacter> Targets { get; set; }
        public Vector3 Position { get; set; }
        public ICharacter CurrentTarget { get; set; }

        public void Move()
        {
            View.SetPosition(Position);
        }

        public void Init(CharacterConfig config)
        {
            MaxHealth = config.Health;
            Health = config.Health;
            Damage = config.Damage;
            DamageType = config.DamageType;
            MovementSpeed = config.MovementSpeed;
            DamageSpeed = config.DamageSpeed;
        }

        public bool CanAttack()
        {
            if(CurrentTarget == null)
                return false;

            if (DamageType == DamageType.Melee)
                return IsNearTarget() && IsCooldown();

            if (DamageType == DamageType.Range)
                return IsCooldown();

            return false;
        }

        public bool IsCooldown()
        {
            return Time.time > TimeLastDamage + DamageSpeed;
        }

        public bool IsNearTarget()
        {
            return Vector3.Distance(CurrentTarget.View.transform.position, View.transform.position) < 1f;
            /*if (Math.Abs(CurrentTarget.View.transform.position.y - View.transform.position.y) < 0.1f)
            {
                return Vector3.Distance(CurrentTarget.View.transform.position, View.transform.position) < 1f;
            }

            return false;#1#
        }

        private ICharacter _currentTarget;
        public ICharacter FindTarget()
        {
       

            List<ICharacter> aliveTarget = new();

            foreach (var target in Targets)
            {
                if(target.Health > 0)
                    aliveTarget.Add(target);
            }
            if (aliveTarget.Count <= 0)
            {
                return null;
            }
            float distance = Mathf.Infinity;

            Vector3 position = _view.transform.position;
            foreach (var enemy in aliveTarget)
            {
                Vector3 diff = enemy.View.transform.position - position;
                float currentDistance = diff.sqrMagnitude;
                if (currentDistance < distance)
                {
                    distance = currentDistance;
                    _currentTarget = enemy;
                }
            }
            return _currentTarget;
        }

        public void TakeDamage(float value)
        {
            //Debug.Log($"Gigachad got damage={value} / Health{Health}");
            Health -= value;
            View.TakeDamageView();
            View.SetHealthBar(Health / MaxHealth);
        }
    }
}
*/
