using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Essence.Characters;
using Essence.Constants;

namespace Essence.Spells
{
    public class Slow : StatusEffect
    {
        public float percentage;

        public Slow(int duration, float percentage)
        {
            this.duration = duration;
            this.percentage = percentage;
            this.debuff = Debuff.SLOW;
            this.isBuff = false;
        }
        
        public override void Apply(Character target)
        {
            Slow existing = (Slow)target.GetEffect(this);
            existing = null;
            if (existing == null)
            {
                target.statusEffects.Add(this);
                target.speed = target.speed * percentage;
            }
            else
            {
                existing.Remove(target);
                this.percentage = this.percentage * existing.percentage;
                // something something
            }
        }

        public override void Remove(Character target)
        {
            target.statusEffects.Remove(this);
            target.speed = target.speed / percentage;
        }
    }
}
