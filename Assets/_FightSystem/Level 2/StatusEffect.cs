﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Enum des status de chaque attaque (voir plus bas)
    /// </summary>
    public enum StatusPotential { NONE, SLEEP, BURN, CRAZY, POISON }
    
    public class StatusEffect
    {
        /// <summary>
        /// Factory retournant un nouvel objet représentant le statut généré
        /// </summary>
        /// <param name="s">le statut à générer</param>
        /// <returns>Le statut à appliquer sur le character ciblé</returns>
        public static StatusEffect GetNewStatusEffect(StatusPotential s)
        {
            switch (s)
            {
                case StatusPotential.SLEEP:
                    return new SleepStatus();
                case StatusPotential.BURN:
                    return new BurnStatus();
                case StatusPotential.CRAZY:
                    return new CrazyStatus();
                case StatusPotential.POISON:
                    return new PoisonStatus();
                case StatusPotential.NONE:
                default:
                    return null;
            }
        }
        /// <summary>
        /// Un Status ne peut etre crée que par une classe enfant (voir plus bas)
        /// </summary>
        /// <param name="remainingTurn">Nombre de tour de l'effet</param>
        /// <param name="damageEachTurn">Nombre de dégât à la fin de chaque tour</param>
        /// <param name="canAttack">Le personnage peut-il attaquer ?</param>
        /// <param name="damageOnAttack">Portion de l'attaque auto-infligé au moment de l'attaque( 1f:100%, 0.5f:50% etc</param>
        protected StatusEffect(int remainingTurn, int damageEachTurn, bool canAttack, float damageOnAttack)
        {
            RemainingTurn = remainingTurn;
            DamageEachTurn = damageEachTurn;
            DamageOnAttack = damageOnAttack;
            CanAttack = canAttack;
            Status = StatusPotential.NONE;
        }

        public StatusPotential Status { get; protected set; }
        /// <summary>
        /// Nombre de tour de l'effet
        /// </summary>
        public int RemainingTurn { get; protected set; }
        /// <summary>
        /// Nombre de dégât à la fin de chaque tour
        /// </summary>
        public int DamageEachTurn { get; protected set; }
        /// <summary>
        /// Le personnage peut-il attaquer ?
        /// </summary>
        public bool CanAttack { get; protected set; }
        /// <summary>
        /// Portion de l'attaque auto-infligé au moment de l'attaque( 1f:100%, 0.5f:50% etc
        /// </summary>
        public float DamageOnAttack { get; protected set; }

        /// <summary>
        /// Méthode enclenché par le système de combat à la fin de chaque tour
        /// Vous pouvez ajouter du contenu si besoin
        /// </summary>

        public virtual bool BeginTurn()
        {
            return CanAttack;
        }

        public virtual int EffectOnAttack(int maxHealth)
        {
            return (int)(maxHealth * DamageOnAttack);
        }

        public virtual int DamageEndTurn()
        {
            return DamageEachTurn;
        }

        public virtual void EndTurn()
        {
            RemainingTurn--;
        }
    }

    /// <summary>
    /// Endormi, le personnage ne peut pas attaquer
    /// </summary>
    public class SleepStatus : StatusEffect
    {
        public SleepStatus() : base(5, 5, false, 0f)
        {
            Status = StatusPotential.SLEEP;
        }
    }

    /// <summary>
    /// Brûlé, le personnage perd des points de vie à la fin de chaque tour
    /// </summary>
    public class BurnStatus : StatusEffect
    {
        public BurnStatus() : base(5, 5, true, 0)
        {
            Status = StatusPotential.BURN;
        }
    }

    /// <summary>
    /// Folie, le personnage s'attaque contre-lui même (on skip la notion attaque-defense au profit d'une portion de la stat d'attaque
    /// </summary>
    public class CrazyStatus : StatusEffect
    {
        public CrazyStatus() : base(1, 0, false, 0.3f)
        {
            Status = StatusPotential.CRAZY;
        }
    }

    public class PoisonStatus : StatusEffect
    {
        public PoisonStatus() : base(8, 4, true, 0)
        {
            Status = StatusPotential.POISON;
        }
    }
}
