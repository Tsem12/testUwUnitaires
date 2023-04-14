
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Défintion simple d'un équipement apportant des boost de stats
    /// </summary>
    public class Equipment
    {
        public enum EquipementType
        {
            None,
            AttackPriority
        };

        private int _bonusHealth;
        private int _bonusAttack;
        private int _bonusDefense;
        private int _bonusSpeed;
        private EquipementType _equipement;

        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed, EquipementType equipement)
        {
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
            BonusHealth = bonusHealth;
            Equipement = equipement;
        }

        public EquipementType Equipement { get => _equipement; protected set => _equipement = value; }
        public int BonusHealth { get => _bonusHealth; protected set => _bonusHealth = value; }
        public int BonusAttack { get => _bonusAttack; protected set => _bonusAttack = value; }
        public int BonusDefense { get => _bonusDefense; protected set => _bonusDefense = value; }
        public int BonusSpeed { get => _bonusSpeed; protected set => _bonusSpeed = value; }
    }
}
