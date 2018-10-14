using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Essence.Characters;

namespace Essence.Spells
{
    public abstract class ContinuousSpell : Spell
    {
        public int ticks = 0; // Counter of active frames
        public int frequency; // Spell should "pulse" every *frequency* frames

        public int fireDrain;
        public int waterDrain;
        public int windDrain;
        public int earthDrain;
        
        public bool toggled;

        public void Update(Character caster)
        {
            if (toggled)
            {
                spellObject.transform.position = caster.transform.position;
                spellObject.transform.localEulerAngles = GetEulerAngles(caster);

                if (ticks % frequency == 0)
                {
                    if (caster.CastSpell(this))
                    {
                        Pulse(caster);
                    }
                    else
                    {
                        Toggle(caster);
                    }
                }
            }

            ticks++;
        }

        // Turn spell on/off
        public void Toggle(Character caster)
        {
            if (toggled)
            {
                // End spell
                GameObject.Destroy(spellObject);
            }
            else
            {
                // Start spell
                // Get animation object
                spellObject = Resources.Load<GameObject>("Spells/" + spellName); ;

                // Set starting position/angle of animation
                spellObject.transform.position = caster.transform.position;
                spellObject.transform.localEulerAngles = GetEulerAngles(caster);

                // Instantiate
                spellObject = GameObject.Instantiate(spellObject);
            }

            
            toggled = !toggled;
            ticks = 1;
        }

        public abstract void Pulse(Character caster);
    }
}
