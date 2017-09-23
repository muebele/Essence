using UnityEngine;
using System.Collections;
using System;
using Essence.Characters;

namespace Essence.Constants
{

    public class GlobalConstants
    {
        public static float Movement_Scale = 3;
    }

    public class Helpers
    {
        public static RaycastHit2D[] Union(RaycastHit2D[] a, RaycastHit2D[] b)
        {
            Debug.Log(a.Length);
            Debug.Log(b.Length);
            //RaycastHit2D[] union = new RaycastHit2D[Math.Max(a.Length, b.length)];
            int index = a.Length;
            for (int i = 0;  i < a.Length; i++)
            {
                if (a[i].collider == null)
                {
                    index = i;
                    break;
                }
            }
            

            foreach (RaycastHit2D x in b)
            {
                bool add = true;
                foreach (RaycastHit2D y in a)
                {
                    if (x.collider != null && y.collider != null)
                    {
                        if (x.collider.gameObject.GetComponent<Character>() == y.collider.gameObject.GetComponent<Character>())
                        {
                            add = false;
                        }
                    }
                }
                if (add)
                {
                    a[index] = x;
                    index++;
                }
            }

            return null;
        }
    }

    public enum Element
    {
        FIRE = 1,
        WATER = 2,
        WIND = 3,
        EARTH = 4
    }

    public enum Debuff
    {
        SLOW = 1,
        STUN = 2,
        IMMOBILIZE = 3,
        SILENCE = 4,
        BLIND = 5,
        POISON = 6,
        STASIS = 7,

        BURN = 10,
        WET = 11,
    }

    public enum Buff
    {
        HASTE = 1,
        REGEN = 2,
        SHIELD = 3
    }
}

