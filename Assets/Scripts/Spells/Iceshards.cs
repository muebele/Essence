using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Essence.Spells
{
    public class Iceshards : ProjectileSpell
    {
        public Iceshards()
        {

            spellName = "Iceshards";
            description = "Iceshards pew pew!";

            fireReq = 0;
            waterReq = 1;
            windReq = 1;
            earthReq = 0;

            cooldown = 300;
        }

    }
}

