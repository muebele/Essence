using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Essence.Spells
{
    public class Gust : ProjectileSpell
    {
        public Gust()
        {

            spellName = "Gust";
            description = "Knock enemies back with a gust of wind";

            fireReq = 0;
            waterReq = 0;
            windReq = 1;
            earthReq = 0;

            maxCooldown = 100;

        }

    }
}

