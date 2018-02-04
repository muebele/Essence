using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Essence.Characters;
using Essence.Constants;

namespace Essence.Essence
{
    public class Essence : MonoBehaviour
    {
        public GameObject Orb;

        private List<Element> elements;
        private int quantity;

        void Start()
        {
            SetElements();

            quantity = 1;
        }

        void Update()
        {

        }

        void OnTriggerEnter2D(Collider2D col)
        {
            GameObject obj = col.gameObject;

            Character character = (Character)obj.GetComponent(typeof(Character));
            if (character != null)
            {
                foreach (Element element in elements)
                {
                    character.CollectEssence(element, quantity);
                }

                Destroy(this.gameObject);
            }
        }

        private void SetElements()
        {
            
            elements = new List<Element>();

            if (Orb.name.Contains("Fire")) elements.Add(Element.FIRE);
            if (Orb.name.Contains("Water")) elements.Add(Element.WATER);
            if (Orb.name.Contains("Wind")) elements.Add(Element.WIND);
            if (Orb.name.Contains("Earth")) elements.Add(Element.EARTH);


        }
    }
}
