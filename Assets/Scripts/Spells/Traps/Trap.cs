using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Essence.Spells
{
    public class Trap : TrapSpell
    {
        public Trap()
        {
            spellName = "Trap";
            description = "Snare an enemy";

            fireReq = 0;
            waterReq = 0;
            windReq = 0;
            earthReq = 1;

            maxCooldown = 240;
        }
    }
}
