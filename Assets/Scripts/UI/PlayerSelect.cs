﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

namespace Essence.UI
{
    public class PlayerSelect : MonoBehaviour
    {
        private int PortraitIndex = 1;
        private int ColorIndex = 1;

        public int PortraitCount = 10;
        public int ColorCount = 9;

        public GameObject Portrait;
        public GameObject Color;

        public PlayerSelect[] OtherPlayers = new PlayerSelect[3];

        // Use this for initialization
        void Start()
        {
            CheckOtherPlayers(true);

            UpdateColor();
            UpdatePortrait();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void IncrementPortrait()
        {
            if (PortraitIndex == PortraitCount)
            {
                PortraitIndex = 1;
            }
            else
            {
                PortraitIndex++;
            }

            CheckOtherPlayers(true);

            UpdatePortrait();
        }

        public void DecrementPortrait()
        {
            if (PortraitIndex == 1)
            {
                PortraitIndex = PortraitCount;
            }
            else
            {
                PortraitIndex--;
            }

            CheckOtherPlayers(false);

            UpdatePortrait();
        }

        public void IncrementColor()
        {
            if (ColorIndex == ColorCount)
            {
                ColorIndex = 1;
            }
            else
            {
                ColorIndex++;
            }

            CheckOtherPlayers(true);

            UpdateColor();
        }

        public void DecrementColor()
        {
            if (ColorIndex == 1)
            {
                ColorIndex = ColorCount;
            }
            else
            {
                ColorIndex--;
            }

            CheckOtherPlayers(false);

            UpdateColor();
        }

        private void CheckOtherPlayers(bool increment)
        {
            
            foreach (PlayerSelect other in OtherPlayers)
            {
                if (PortraitIndex == other.PortraitIndex)
                {
                    if (increment)
                    {
                        IncrementPortrait();
                    }
                    else
                    {
                        DecrementPortrait();
                    }
                }

                if (ColorIndex == other.ColorIndex)
                {
                    if (increment)
                    {
                        IncrementColor();
                    }
                    else
                    {
                        DecrementColor();
                    }
                }
            }
            
        }
        
        private void UpdatePortrait()
        {
            Image portrait = Portrait.GetComponent<Image>();

            portrait.sprite = Resources.Load<Sprite>("UI/Portraits/Portrait" + PortraitIndex);
        }

        private void UpdateColor()
        {
            Image portrait = Color.GetComponent<Image>();

            float r, g, b;
            if (ColorIndex <= 3)
            {
                r = 255;
                g = ((ColorIndex - 1) % 3) * 127;
                b = 0;
            }
            else if (ColorIndex <= 6)
            {
                r = 0;
                g = 255;
                b = ((ColorIndex - 1) % 3) * 127;
            }
            else
            {
                r = ((ColorIndex - 1) % 3) * 127;
                g = 0;
                b = 255;

            }

            r = r / 255.0f;
            g = g / 255.0f;
            b = b / 255.0f;
            portrait.color = new Color(r, g, b);
        }
    }
}
