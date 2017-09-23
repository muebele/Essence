using UnityEngine;
using System.Collections;
using Essence.Characters;


namespace Essence.Spells
{
    public abstract class InstantCastSpell : Spell
    {

        public abstract void Affect(Character caster, Character targets);
    }

}

