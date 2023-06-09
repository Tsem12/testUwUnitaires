﻿
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Défintion simple d'un équipement apportant des boost de stats
    public enum EquipementBonus
    {
        None,
        AttackPriority
    };
    /// </summary>
    public class Equipment
    {

        private int _bonusHealth;
        private int _bonusAttack;
        private int _bonusDefense;
        private int _bonusSpeed;
        private EquipementBonus _eBonus = EquipementBonus.None;
        private TYPE _eType = TYPE.NORMAL;

        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed)
        {
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
            BonusHealth = bonusHealth;
        }

        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed, EquipementBonus equipement)
        {
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
            BonusHealth = bonusHealth;
            EBonus = equipement;
        }

        public Equipment(TYPE type, int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed, EquipementBonus equipement)
        {
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
            BonusHealth = bonusHealth;
            EBonus = equipement;
            EType = type;
        }

        public EquipementBonus EBonus { get => _eBonus; protected set => _eBonus = value; }
        public TYPE EType { get => _eType; protected set => _eType = value; }
        public int BonusHealth { get => _bonusHealth; protected set => _bonusHealth = value; }
        public int BonusAttack { get => _bonusAttack; protected set => _bonusAttack = value; }
        public int BonusDefense { get => _bonusDefense; protected set => _bonusDefense = value; }
        public int BonusSpeed { get => _bonusSpeed; protected set => _bonusSpeed = value; }
    }
}
