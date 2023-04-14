
using _2023_GC_A2_Partiel_POO.Level_2;
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition des types dans le jeu
    /// </summary>
    public enum TYPE  {
      NONE = -1,
      NORMAL = 0,
      FIRE = 1,
      WATER = 2,
      GRASS = 3,
      ELECTRIC = 4,
      ICE = 5,
      FIGHTING = 6,
      POISON = 7,
      GROUND = 8,
      FLY = 9,
      PSY = 10,
      BUG = 11,
      ROCK = 12,
      SPECTER = 13,
      DRAGON = 14,
      DARK = 15,
      STEEL = 16,
      FAIRY = 17,
    };

    public class TypeResolver
    {

        /// <summary>
        /// Récupère le facteur multiplicateur pour la résolution des résistances/faiblesses
        /// WATER faible contre GRASS, resiste contre FIRE
        /// FIRE faible contre WATER, resiste contre GRASS
        /// GRASS faible contre FIRE, resiste contre WATER
        /// </summary>
        /// <param name="attacker">Type de l'attaque (le skill)</param>
        /// <param name="receiver">Type de la cible</param>
        /// <returns>
        /// Normal returns 1 if attacker or receiver
        /// 0.8 if resist
        /// 1.0 if same type
        /// 1.2 if vulnerable
        /// </returns>
        /// 
        private static float[,] _typeChart = {
                         /*Normal    Fire     Water   Grass   Electric    Ice    Fighting  Poison   Ground   Flying   Psychic   Bug      Rock    Ghost    Dragon    Dark    Steel    Fairy  */ // Defensive Type
            /*Normal*/  {    1f   ,   1f   ,   1f   ,   1f   ,   1f    ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,  0.5f  ,   0f   ,   1f   ,   1f   ,  0.5f  ,   1f   },
            /*Fire*/    {    1f   ,  0.5f  ,  0.5f  ,   2f   ,   1f    ,   2f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   2f   ,  0.5f  ,   1f   ,  0.5f  ,   1f   ,   2f   ,   1f   },
            /*Water*/   {    1f   ,   2f   ,  0.5f  ,  0.5f  ,   1f    ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   ,  0.5f  ,   1f   ,   1f   ,   1f   },
            /*Grass*/   {    1f   ,  0.5f  ,   2f   ,  0.5f  ,   1f    ,   1f   ,   1f   ,  0.5f  ,   2f   ,  0.5f  ,   1f   ,  0.5f  ,   2f   ,   1f   ,  0.5f  ,   1f   ,  0.5f  ,   1f   },
            /*Electric*/{    1f   ,   1f   ,   2f   ,  0.5f  ,  0.5f   ,   1f   ,   1f   ,   1f   ,   0f   ,   2f   ,   1f   ,   1f   ,   1f   ,   1f   ,  0.5f  ,   1f   ,   1f   ,   1f   },
            /*Ice*/     {    1f   ,  0.5f  ,  0.5f  ,   2f   ,   1f    ,  0.5f  ,   1f   ,   1f   ,   2f   ,   2f   ,   1f   ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   ,  0.5f  ,   1f   },
            /*Fighting*/{    2f   ,   1f   ,   1f   ,   1f   ,   1f    ,   2f   ,   1f   ,  0.5f  ,   1f   ,  0.5f  ,  0.5f  ,  0.5f  ,   2f   ,   0f   ,   1f   ,   2f   ,   2f   ,  0.5f  },
            /*Poison*/  {    1f   ,   1f   ,   1f   ,   2f   ,   1f    ,   1f   ,   1f   ,  0.5f  ,  0.5f  ,   1f   ,   1f   ,   1f   ,  0.5f  ,  0.5f  ,   1f   ,   1f   ,   0f   ,   2f   },
            /*Ground*/  {    1f   ,   2f   ,   1f   ,  0.5f  ,   2f    ,   1f   ,   1f   ,   2f   ,   1f   ,   0f   ,   1f   ,  0.5f  ,   2f   ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   },
            /*Flying*/  {    1f   ,   1f   ,   1f   ,   2f   ,  0.5f   ,   1f   ,   2f   ,   1f   ,   1f   ,   1f   ,   1f   ,   2f   ,  0.5f  ,   1f   ,   1f   ,   1f   ,  0.5f  ,   1f   },
            /*Psychic*/ {    1f   ,   1f   ,   1f   ,   1f   ,   1f    ,   1f   ,   2f   ,   2f   ,   1f   ,   1f   ,  0.5f  ,   1f   ,   1f   ,   1f   ,   1f   ,   0f   ,  0.5f  ,   1f   },
            /*Bug*/     {    1f   ,  0.5f  ,   1f   ,   2f   ,   1f    ,   1f   ,  0.5f  ,  0.5f  ,   1f   ,  0.5f  ,   2f   ,   1f   ,   1f   ,  0.5f  ,   1f   ,   2f   ,  0.5f  ,  0.5f  },
            /*Rock*/    {    1f   ,   2f   ,   1f   ,   1f   ,   1f    ,   2f   ,  0.5f  ,   1f   ,  0.5f  ,   2f   ,   1f   ,   2f   ,   1f   ,   1f   ,   1f   ,   1f   ,  0.5f  ,   1f   },
            /*Ghost*/   {    0f   ,   1f   ,   1f   ,   1f   ,   1f    ,   1f   ,   0f   ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   ,   1f   ,   2f   ,   1f   ,  0.5f  ,  0.5f  ,   1f   },
            /*Dragon*/  {    1f   ,   1f   ,   1f   ,   1f   ,   1f    ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   ,  0.5f  ,   0f   },
            /*Dark*/    {    1f   ,   1f   ,   1f   ,   1f   ,   1f    ,   1f   ,  0.5f  ,   1f   ,   1f   ,   1f   ,   0f   ,   1f   ,   1f   ,   2f   ,   1f   ,  0.5f  ,  0.5f  ,  0.5f  },
            /*Steel*/   {    1f   ,  0.5f  ,  0.5f  ,   1f   ,  0.5f   ,   2f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   1f   ,   2f   ,   1f   ,   1f   ,   1f   ,  0.5f  ,   2f   },
            /*Fairy*/   {    1f   ,  0.5f  ,   1f   ,   1f   ,   1f    ,   1f   ,   2f   ,  0.5f  ,   1f   ,   1f   ,   2f   ,   1f   ,   1f   ,   1f   ,   2f   ,   2f   ,  0.5f  ,   1f   }
            //Attacking Type
      };
        public static float GetFactor(TYPE attacker, TYPE receiver)
        {
            if (attacker == TYPE.NONE || receiver == TYPE.NONE)
                throw new ArgumentException("Type cannot be None", "Attacker or Receiver");
            float effectiveness = 1f;

            effectiveness *= _typeChart[(int)attacker, (int)receiver];
            return effectiveness;
        }

        public static float GetSTAB(TYPE Move, TYPE pokemon)
        {
            if (pokemon == Move)
                return 1.5f;
            return 1f;
        }

    }
}
