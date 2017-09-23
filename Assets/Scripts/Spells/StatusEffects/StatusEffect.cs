using UnityEngine;
using System.Collections;
using Essence.Characters;
using Essence.Constants;

namespace Essence.Spells
{
    public abstract class StatusEffect
    {
        public int duration;
        

        public Debuff debuff;
        public Buff buff;
        public bool isBuff;

        public virtual void Apply(Character target)
        {
            target.statusEffects.Add(this);
        }
        public virtual void Affect(Character target)
        {
            duration--;
        }
        public virtual void Remove(Character target)
        {
            target.statusEffects.Remove(this);
        }


    }
}


