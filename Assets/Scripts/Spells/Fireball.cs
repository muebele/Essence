using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Essence.Spells
{
	public class Fireball: ProjectileSpell
	{
        public Fireball()
        {

            spellName = "Fireball";
            description = "Fire a fireball!";

            fireReq = 1;
            waterReq = 0;
            windReq = 0;
            earthReq = 0;

            maxCooldown = 180;

        }

    }
}

