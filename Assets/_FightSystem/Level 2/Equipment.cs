
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Défintion simple d'un équipement apportant des boost de stats
    /// </summary>
    public class Equipment
    {
<<<<<<< Updated upstream
        
=======
        private int _bonusHealth;
        private int _bonusAttack;
        private int _bonusDefense;
        private int _bonusSpeed;

>>>>>>> Stashed changes
        public Equipment(int bonusHealth, int bonusAttack, int bonusDefense, int bonusSpeed)
        {
            BonusAttack = bonusAttack;
            BonusDefense = bonusDefense;
            BonusSpeed = bonusSpeed;
            BonusHealth = bonusHealth;
        }

        public int BonusHealth { get => _bonusHealth; protected set => _bonusHealth = value; }
        public int BonusAttack { get => _bonusAttack; protected set => _bonusAttack = value; }
        public int BonusDefense { get => _bonusDefense; protected set => _bonusDefense = value; }
        public int BonusSpeed { get => _bonusSpeed; protected set => _bonusSpeed = value; }
    }
}
