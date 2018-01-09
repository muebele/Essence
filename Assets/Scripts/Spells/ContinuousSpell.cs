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
        public int ticks = 0;
        public int frequency;

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

        public void Toggle(Character caster)
        {
            if (toggled)
            {
                GameObject.Destroy(spellObject);
                
            }
            else
            {
                spellObject = (GameObject)Resources.Load(spellName, typeof(GameObject));

                spellObject.transform.position = caster.transform.position;
                Vector3 direction = caster.gameObject.GetComponent<Controller>().directionPointing;
                float a = Vector3.Angle(direction, new Vector3(1, 0));
                if (caster.gameObject.GetComponent<Controller>().directionPointing.y > 0) a = -a;
                
                spellObject.transform.localEulerAngles = new Vector3(a, 90, 0);
                spellObject = GameObject.Instantiate(spellObject);

                
            }

            toggled = !toggled;
            ticks = 0;
        }

        public abstract void Pulse(Character caster);
    }
}
