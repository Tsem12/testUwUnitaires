using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer les TU sur le reste et de les implémenter

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type

        [Test]
        [TestCase(100, 100)]
        public void CantHealMoreThanMaxHp(int healValue, int maxHeal)
        {
            Character c = new Character(maxHeal, 10, 10, 1, TYPE.NONE);
            c.Heal(healValue);

            Assert.That(c.CurrentHealth <= c.MaxHealth);
        }

        [Test]
        [TestCase(100, 50)]
        [TestCase(100, 200)]
        public void HpCannotBeyondMaxHp(int maxHeal, int newMaxHeal)
        {
            Character c = new Character(maxHeal, 10, 10, 1, TYPE.NONE);
            c.ChangeHPStats(newMaxHeal);

            Assert.That(c.CurrentHealth <= c.MaxHealth);
        }

        [Test]
        [TestCase(2, 5, true)]
        [TestCase(5, 2, true)]
        public void PrioriryAttackOnProrityEquipement(int p1Speed, int p2Speed, bool expected)
        {
            Character p1 = new Character(100, 10, 10, p1Speed, TYPE.NONE);
            Character p2 = new Character(100, 10, 10, p2Speed, TYPE.NONE);
            p1.Equip(new Equipment(0, 0, 0, 0, EquipementBonus.AttackPriority));
            Fight fight = new Fight(p1, p2);

            Assert.That(fight.IsPriority(fight.Character1, fight.Character2) == expected);
        }

        [Test]
        [TestCase(2, 5, false)]
        [TestCase(5, 2, true)]
        public void NoMorePrioriryAttackOnPriorityEquipementRemove(int p1Speed, int p2Speed, bool expected)
        {
            Character p1 = new Character(100, 10, 10, p1Speed, TYPE.NONE);
            Character p2 = new Character(100, 10, 10, p2Speed, TYPE.NONE);
            p1.Equip(new Equipment(0, 0, 0, 0, EquipementBonus.AttackPriority));
            p1.Unequip();
            Fight fight = new Fight(p1, p2);

            Assert.That(fight.IsPriority(fight.Character1, fight.Character2) == expected);
        }


        [Test]
        [TestCase(TYPE.FIRE, TYPE.WATER, 0.5f)]
        [TestCase(TYPE.FIRE, TYPE.ELECTRIC, 1f)]
        [TestCase(TYPE.FIRE, TYPE.GRASS, 2f)]
        [TestCase(TYPE.BUG, TYPE.FIRE, 0.5f)]
        [TestCase(TYPE.STEEL, TYPE.FIGHTING, 1f)]
        [TestCase(TYPE.GROUND, TYPE.ELECTRIC, 2f)]
        public void TypeFactorMulitiplicator(TYPE attacker, TYPE receiver, float expected)
        {
            Assert.That(TypeResolver.GetFactor(attacker, receiver) == expected);
        }

        [Test]
        [TestCase(TYPE.NONE, TYPE.DARK)]
        [TestCase(TYPE.NONE, TYPE.NONE)]
        [TestCase(TYPE.FIRE, TYPE.NONE)]
        public void TypeFactorNotNoneORNone(TYPE attacker, TYPE receiver)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var result = TypeResolver.GetFactor(attacker, receiver);
            });
        }


        [Test]
        public void GetStatus()
        {
            Character p1 = new Character(10, 10, 10, 10, TYPE.NORMAL);
            Character p2 = new Character(10, 10, 10, 10, TYPE.NORMAL);

            p2.CurrentAttack = new FireBall();

            p1.ReceiveAttack(p2);

            Assert.That(p1.CurrentStatus.Status == StatusPotential.BURN);
        }


        [Test]
        public void CheckBurnEffect()
        {
            Character p1 = new Character(1000, 30, 20, 10, TYPE.NORMAL);
            Character p2 = new Character(1000, 30, 20, 10, TYPE.NORMAL);

            p1.CurrentAttack = new FireBall();
            p2.CurrentAttack = new FireBall();

            Fight fight = new Fight(p1, p2);
            fight.PlayAttack(p1, p2, true);

            Assert.That(p2.MaxHealth - 5 - 7 == p2.CurrentHealth);
        }

        [Test]
        public void CheckSleepEffect()
        {
            Character p1 = new Character(1000, 30, 20, 10, TYPE.NORMAL);
            Character p2 = new Character(1000, 30, 20, 10, TYPE.NORMAL);

            p1.CurrentAttack = new SleepPowder();
            p2.CurrentAttack = new FireBall();
            int lifeBeforeHit = p1.CurrentHealth;

            Fight fight = new Fight(p1, p2);
            fight.PlayAttack(p1, p2, true);

            Assert.That(lifeBeforeHit == p1.CurrentHealth);
        }

        [Test]
        public void CheckCrazyEffect()
        {
            Character p1 = new Character(1000, 30, 20, 10, TYPE.NORMAL);
            Character p2 = new Character(1000, 30, 20, 10, TYPE.NORMAL);

            p1.CurrentAttack = new Supersonic();
            p2.CurrentAttack = new FireBall();
            int lifeBeforeHit = p1.CurrentHealth;

            Fight fight = new Fight(p1, p2);
            fight.PlayAttack(p1, p2, true);

            Assert.That((float)(p2.MaxHealth * 0.7f) == p2.CurrentHealth);
        }
    }

}
