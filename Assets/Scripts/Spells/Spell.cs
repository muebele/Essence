using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Essence.Spells
{
	public abstract class Spell
	{
        public GameObject spellObject;

        public string spellName;
        public string description;

		public int fireReq;
        public int waterReq;
        public int windReq;
        public int earthReq;

        public int maxCooldown;
        public int cooldown = 0;
        public bool isAvailable = true;

        public void DecreaseCooldown()
        {
            if (cooldown > 0)
            {
                cooldown--;
            }
            else
            {
                isAvailable = true;
            }
        }
	}
}
