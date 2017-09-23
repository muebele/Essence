using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Essence.Characters;
using Essence.Constants;

namespace Essence.Spells
{
    public class Immobilize : StatusEffect
    {
        private float victimSpeed;

        public Immobilize(int duration)
        {
            this.duration = duration;
            this.debuff = Debuff.IMMOBILIZE;
            this.isBuff = false;
        }

        public override void Apply(Character target)
        {
            victimSpeed = target.maxSpeed;
            target.statusEffects.Add(this);
            target.speed = 0;
        }

        public override void Remove(Character target)
        {
            target.statusEffects.Remove(this);
            target.speed = victimSpeed;
            foreach (StatusEffect effect in target.statusEffects)
            {
                if (effect is Slow)
                {
                    Slow slow = (Slow)effect;
                    target.speed *= slow.percentage;
                }
            }
        }
    }
}
