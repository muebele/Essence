using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Essence.Characters;
using Essence.Constants;

namespace Essence.Spells
{
    public class Wet : StatusEffect
    {
       public Wet(int duration)
        {
            this.duration = duration;
            this.debuff = Debuff.WET;

        }
    }
}
