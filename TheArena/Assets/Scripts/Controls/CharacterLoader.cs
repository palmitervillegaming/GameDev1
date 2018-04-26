﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Controls
{
    class CharacterLoader : MonoBehaviour
    {
        public static Dictionary<string, Player> loadedChars = new Dictionary<string, Player>();
        
        /**
         * Load a player resource from path in the specified position
         */
        public static Player LoadPlayer(string resourcePath, float x, float y)
        {
            Player p;
            try
            {
                if (loadedChars.ContainsKey(resourcePath) && !loadedChars[resourcePath].IsDead)
                {
                    p = loadedChars[resourcePath];
                }
                else
                {
                    p = Instantiate(Resources.Load(resourcePath, typeof(Player))) as Player;
                    p.character = BaseCharacterRepository.GetBaseCharacter(BaseCharacterRepository.CharacterCode.MAIN);
                    loadedChars.Add(resourcePath, p);
                }
                LoadPlayerStats(ref p);
                p.transform.position = new Vector3(x, y);
            } catch (MissingReferenceException e)
            {
                loadedChars.Remove(resourcePath);
                p = LoadPlayer(resourcePath, x, y);
            }
            return p;
        }
        
        public static Player LoadMain(float x, float y)
        {
            Player p =  LoadPlayer("Prefab/Characters/Main", x, y);
            GameControl.Instance.CurrentPlayer = p;
            return p;
        }

        public static void LoadPlayerStats(ref Player p)
        {
            //Temp until this is loading from file
            p.name = "Old Boi Man";
            p.level = 21;
            p.currentHealth = 800;
            p.maxHealth = 1000;
            p.currentMana = 120;
            p.maxMana = 200;
            p.currentStamina = 20;
            p.maxStamina = 100;
            p.currentFocus = 40;
            p.maxFocus = 100;
        }
    }
} 
