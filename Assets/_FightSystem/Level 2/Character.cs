using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Character
    {
        int _baseHealth;
        int _baseAttack;
        int _baseDefense;
        int _baseSpeed;
        TYPE _baseType;

        Equipment _currentEquipement;
        Skill _currentAttack;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
            CurrentHealth = _baseHealth;
        }

        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}

        public int MaxHealth {
            get { return _baseHealth + (_currentEquipement != null ? CurrentEquipment.BonusHealth : 0); }
        }

        public int Attack {
            get { return _baseAttack + (_currentEquipement != null ? CurrentEquipment.BonusAttack : 0); }
        }

        public int Defense {
            get { return _baseDefense + (_currentEquipement != null ? CurrentEquipment.BonusDefense : 0); }
        }

        public int Speed {
            get { return _baseSpeed + (_currentEquipement != null ? CurrentEquipment.BonusSpeed : 0); }
        }

        public Equipment CurrentEquipment { 
            get { return _currentEquipement; }
            private set { _currentEquipement = value; }
        }

        public Skill CurrentAttack {
            get { return _currentAttack; }
            set { _currentAttack = value; }
        }

        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive => CurrentHealth > 0;


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            CurrentHealth -= amount;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void ReceiveAttack(float amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException(nameof(amount));
            CurrentHealth -= Mathf.FloorToInt(amount);
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }
        
        public void ReceiveAttack(Skill s)
        {
            int damage = 0;

            if (s == null)
                throw new ArgumentNullException(nameof(s));
            damage = (s.Power - this.Defense) < 0 ? 1 : (s.Power - this.Defense);
            CurrentHealth -= damage;
            Debug.Log("Damage : " + damage + " Health : " + CurrentHealth);
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void ReceiveAttack(Skill s, int attack)
        {
            int damage = 0;

            if (s == null)
                throw new ArgumentNullException(nameof(s));
            damage = Mathf.FloorToInt((s.Power * (attack / this.Defense)) / 50);
            CurrentHealth -= damage;
            if (s.Status != StatusPotential.NONE)
                CurrentStatus = StatusEffect.GetNewStatusEffect(s.Status);
            Debug.Log("Damage : " + damage + " Health : " + CurrentHealth);
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void ReceiveAttack(Character attacker)
        {
            int damage = 0;
            float modifier = 0;

            if (attacker == null)
                throw new ArgumentNullException(nameof(attacker));
            modifier = TypeResolver.GetFactor(attacker.BaseType, this.BaseType) * TypeResolver.GetSTAB(attacker.BaseType, attacker.CurrentAttack.Type);
            damage = Mathf.FloorToInt(((attacker.CurrentAttack.Power * ((float)attacker.Attack / (float)this.Defense)) / 5) * modifier);
            Debug.Log(damage);
            CurrentHealth -= damage;
            if (attacker.CurrentAttack.Status != StatusPotential.NONE) {
                CurrentStatus = StatusEffect.GetNewStatusEffect(attacker.CurrentAttack.Status);
                Debug.Log("Status : " + CurrentStatus.Status);
            }
            Debug.Log("Damage : " + damage + " Health : " + CurrentHealth);
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }

        public void Heal(int amount)
        {
            CurrentHealth += amount;
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }
        
        public void CureStatus()
        {
            CurrentStatus = null;
        }

        public void ChangeHPStats(int newMaxHealth)
        {
            _baseHealth = newMaxHealth + (_currentEquipement != null ? CurrentEquipment.BonusHealth : 0);
            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }
        public void Equip(Equipment newEquipment)
        {
            if (newEquipment == null)
                throw new ArgumentNullException(nameof(newEquipment));
            CurrentEquipment = newEquipment;
        }
        
        public void Unequip()
        {
            CurrentEquipment = null;
        }

    }
}
