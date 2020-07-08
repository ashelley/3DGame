﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject
{
    public class CharacterTemplate : Interfaces.IGameID
    {
        public enum MainStats
        {
            STR, DEX, INT, VIT
        }

        public string Name { get; set; }
        public string ID { get; set; }
        public string Description { get; set; }
        public SkillTree SkillTree { get; set; }
        public Dictionary<string, float> BaseStats { get; set; }
        public MainStats DamageStat { get; set; }
        public int HPperVIT { get; set; }
        public int MPperINT { get; set; }
        public int HPperLVL { get; set; }
        public int MPperLVL { get; set; }
        public Items.ItemEquip[] StarterEquipment { get; set; }

        public static  CharacterTemplate CreateEmpty(string id)
        {
            return new CharacterTemplate()
            {
                Name = "<untitled class>",
                ID = id,
                Description = "Insert description here!",
                SkillTree = new SkillTree(),
                BaseStats = new Dictionary<string, float>()
                {
                    ["HP"] = 100f,
                    ["hpregen"] = 10f,
                    ["mpregen"] = 10f,
                    ["movement_speed"] = 5f
                },
                DamageStat = MainStats.STR,
                HPperVIT = 10,
                MPperINT = 10,
                HPperLVL = 10,
                MPperLVL = 10,
                StarterEquipment = new Items.ItemEquip[Items.ItemEquip.EquipSlot.Max],
            };
            
        }
    }
}
