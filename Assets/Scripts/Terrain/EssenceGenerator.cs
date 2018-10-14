using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Essence.Essence
{
    class EssenceGenerator : MonoBehaviour
    {
        private int counter = 0; // counter for frames
        private int Respawn_Frequency = 3000; // respawn orbs every X frames
        private int mapWidth;
        private int mapHeight;

        private List<GameObject> generators;
        private List<GameObject> fireSpawners = new List<GameObject>();
        private List<GameObject> waterSpawners = new List<GameObject>();
        private List<GameObject> earthSpawners = new List<GameObject>();
        private List<GameObject> airSpawners = new List<GameObject>();

        void Start()
        {
            mapWidth = Screen.width;
            mapHeight = Screen.height;

            for (int i = 1; i <= 10; i++)
            {
                fireSpawners.Add(GameObject.Find("FireSpawn" + i));
                waterSpawners.Add(GameObject.Find("WaterSpawn" + i));
                //earthSpawners.Add(GameObject.Find("EarthSpawn" + i));
                airSpawners.Add(GameObject.Find("AirSpawn" + i));
            }
            
        }

        void Update()
        {
            // Is it time to respawn?
            if (counter % Respawn_Frequency == 0)
            {
                // Check every spawner
                // If empty, create orb object and place on map
                foreach (GameObject spawner in fireSpawners)
                {
                    if (spawner.transform.childCount == 0)
                    {
                        
                        GameObject orb = Resources.Load<GameObject>("Terrain/Orb_Fire");
                        
                        orb = Instantiate(orb);
                        orb.transform.parent = spawner.transform;
                        orb.transform.position = spawner.transform.position;
                    }
                }
                foreach (GameObject spawner in waterSpawners)
                {
                    if (spawner.transform.childCount == 0)
                    {
                        GameObject orb = Resources.Load<GameObject>("Terrain/Orb_Water");
                        orb = Instantiate(orb);
                        orb.transform.parent = spawner.transform;
                        orb.transform.position = spawner.transform.position;
                    }
                }
                foreach (GameObject spawner in airSpawners)
                {
                    if (spawner.transform.childCount == 0)
                    {
                        
                        GameObject orb = Resources.Load<GameObject>("Terrain/Orb_Wind");
                        orb = Instantiate(orb);
                        orb.transform.parent = spawner.transform;
                        orb.transform.position = spawner.transform.position;
                    }
                }
                foreach (GameObject spawner in earthSpawners)
                {
                    if (spawner.transform.childCount == 0)
                    {
                        GameObject orb = Resources.Load<GameObject>("Terrain/Orb_Earth");
                        orb.transform.position = spawner.transform.position;
                        orb = Instantiate(orb);
                        orb.transform.parent = spawner.transform;
                    }
                }
            }

            counter++;

        }
    }
}
