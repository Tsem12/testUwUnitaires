using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// Ici est rangé un set de compétences afin d'être prête à être utilisé en combat
/// Vous pouvez changer/adapter ces skills au besoin du moment que c'est pertinent dans le reste du code.
namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Punch : Skill
    {
        public Punch() : base(TYPE.NORMAL, 70, StatusPotential.NONE) { }
    }

    public class FireBall : Skill
    {
        public FireBall() : base(TYPE.FIRE, 50, StatusPotential.BURN) { }
    }

    public class WaterBlouBlou : Skill
    {
        public WaterBlouBlou() : base(TYPE.WATER, 20, StatusPotential.NONE) { }
    }

    public class MagicalGrass : Skill
    {
        public MagicalGrass() : base(TYPE.GRASS, 70, StatusPotential.SLEEP) { }
    }

    public class SleepPowder : Skill
    {
        public SleepPowder() : base(TYPE.GRASS, 0, StatusPotential.SLEEP) { }
    }

    public class WillOWisp : Skill
    {
        public WillOWisp() : base(TYPE.FIRE, 0, StatusPotential.BURN) { }
    }

    public class Supersonic : Skill
    {
        public Supersonic() : base(TYPE.NORMAL, 0, StatusPotential.CRAZY) { }
    }
}
