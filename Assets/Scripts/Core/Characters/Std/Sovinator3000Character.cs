/*
using System.Collections.Generic;
using UnityEngine;

namespace Core.Characters.Std
{
    public class Sovinator3000Character : ICharacter
    {
        public CharacterType CharacterType => CharacterType.Sovinator3000;
        public DamageType DamageType { get; set; }
        public float Health { get; set; }
        public float Damage { get; set; }
        public float DamageSpeed { get; set; }
        public float MovementSpeed { get; set; }
        public float TimeLastDamage { get; set; }
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
        }

        public ICharacter FindTarget()
        {
            List<ICharacter> aliveTarget = new();
            
            foreach (var target in Targets)
            {
                if(target.Health > 0)
                    aliveTarget.Add(target);
            }

            if (aliveTarget.Count <= 0)
                return null;

            return aliveTarget[Random.Range(0, aliveTarget.Count)];
        }

        public void TakeDamageView(float value)
        {
            //Debug.Log($"Gigachad got damage={value} / Health{Health}");
            Health -= value;
            View.TakeDamageView();
            View.SetHealthBar(Health / MaxHealth);
        }
    }
}
*/

