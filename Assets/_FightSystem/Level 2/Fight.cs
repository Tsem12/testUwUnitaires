
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null)
                throw new ArgumentNullException(nameof(character1));
            if (character2 == null)
                throw new ArgumentNullException(nameof(character2));
            Character1 = character1;
            Character2 = character2;
        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished => !Character1.IsAlive || !Character2.IsAlive;

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            Character1.CurrentAttack = skillFromCharacter1;
            Character2.CurrentAttack = skillFromCharacter2;
            if (IsPriority(Character1, Character2))
                PlayAttack(Character1, Character2, true);
            else
                PlayAttack(Character2, Character1, true);
        }

        public bool IsPriority(Character character1, Character character2)
        {
            if (character1.Speed > character2.Speed)
                return true;
            else if (character1.CurrentEquipment != null && character1.CurrentEquipment.EType == EquipementType.AttackPriority)
                return true;
            return false;
        }

        public void PlayAttack(Character attacker, Character defender, bool first)
        {
            bool canAttack = true;

            if (attacker.CurrentStatus != null)
                canAttack = attacker.CurrentStatus.BeginTurn();
            if (canAttack)
                defender.ReceiveAttack(attacker.CurrentAttack, attacker.Attack);
                if (attacker.CurrentStatus != null)
                    attacker.CurrentStatus.EffectOnAttack(attacker.MaxHealth);
            if (first && defender.IsAlive)
                PlayAttack(defender, attacker, !first);
            if (attacker.CurrentStatus != null) {
                attacker.CurrentStatus.DamageEndTurn();
                attacker.CurrentStatus.EndTurn();
            }
            if (attacker.CurrentStatus.RemainingTurn == 0)
                attacker.CureStatus();
        }
    }
}
