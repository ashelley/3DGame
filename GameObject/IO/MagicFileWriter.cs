﻿using GameObject.AbilityLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameObject.IO
{
    /// <summary>
    /// Writes various files used by the game systems.
    /// </summary>
    public class MagicFileWriter
    {
        FileStream stream;
        BinaryWriter writer;
        public MagicFileWriter()
        {


        }
        /// <summary>
        /// Writes a list of modular abilities to a binary file.
        /// </summary>
        /// <param name="Abilities">List of modular abilities to write.</param>
        /// <param name="FileName">Filename if not default.</param>
        public void WriteAbilityFile(List<ModularAbility> Abilities, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\abilities.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Abilities.Count);
            foreach (ModularAbility a in Abilities)
                WriteAbility(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }

        public void WriteClassFile(List<CharacterTemplate> Classes, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\classes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Classes.Count);
            foreach (CharacterTemplate a in Classes)
                WriteClass(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }

        public void WriteItemTypeDefinitionFile(List<ItemTypeDefinition> ItemTypeDefinitions, string FileName="")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemtypes.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(ItemTypeDefinitions.Count);
            foreach (ItemTypeDefinition def in ItemTypeDefinitions)
                WriteItemTypeDefinition(def);
            writer.Close();
            writer.Dispose();
            stream.Dispose();

        }

        public void WriteItemMasterTemplateFile(List<ItemMasterTemplate> Templates, string FileName = "")
        {
            string fileName;
            if (FileName == "")
                FileName = "gamedata\\itemmasters.gdf";
            fileName = FileName;
            stream = new FileStream(fileName, FileMode.OpenOrCreate);
            writer = new BinaryWriter(stream);
            writer.Write(Templates.Count);
            foreach (ItemMasterTemplate a in Templates)
                WriteItemMasterTemplate(a);
            writer.Close();
            writer.Dispose();
            stream.Dispose();
        }
        


        public void WriteItemMasterTemplate(ItemMasterTemplate a)
        {

        }


        private void WriteClass(CharacterTemplate a)
        {
            writer.Write(a.ID);
            writer.Write(a.Name);
            writer.Write(a.Description);
            writer.Write(a.HPperVIT);
            writer.Write(a.MPperINT);
            writer.Write(a.HPperLVL);
            writer.Write(a.MPperLVL);
            writer.Write((int)a.DamageStat);
            WriteDictionary(a.BaseStats);
            writer.Write(a.SkillTree.Entries.Count);
            foreach (SkillTreeEntry e in a.SkillTree.Entries)
                WriteSkillTreeEntry(e);
            writer.Write(a.StarterEquipment.Length);
            for(int i=0;i<a.StarterEquipment.Length;i++)
            {
                if(a.StarterEquipment[i]==null)
                {
                    writer.Write("null");
                }
                else
                {
                    writer.Write(a.StarterEquipment[i].ID);
                }
            }
        }

        void WriteAbility(ModularAbility ability)
        {
            writer.Write(ability.ID);
            writer.Write(ability.Name);
            writer.Write(ability.DescriptionString);
            writer.Write(ability.Icon);
            WriteDictionary(ability.BaseValues);
            WriteDictionary(ability.GrowthValues);
            writer.Write(ability.GetModules().Count);
            foreach (ITimedEffect effect in ability.GetModules())
                WriteEffect(effect);
        }

        void WriteItemTypeDefinition(ItemTypeDefinition typedef)
        {
            writer.Write(typedef.ID);
            writer.Write(typedef.Name);
            writer.Write((byte)typedef.SlotID);
            for (int i = 0; i < 6; i++)
                writer.Write(typedef.MainStatMultipliers[i]);
            WriteListOfInt(typedef.Icons);
            writer.Write((int)typedef.ItemCategory);
        }

        private void WriteSkillTreeEntry(SkillTreeEntry e)
        {
            writer.Write(e.SkillID);
            writer.Write(e.LearnLevel);
            writer.Write(e.TrainingLevel);
            writer.Write(e.MaxLevel);
            writer.Write(e.ExpBase);
            writer.Write(e.ExpDelta);
            writer.Write(e.RequireItemID??"null");
            writer.Write(e.Column);
            writer.Write(e.ManualOffset.X);
            writer.Write(e.ManualOffset.Y);
            WritePreRequisiteSkills(e.PreRequisiteSkills);
        }

        private void WritePreRequisiteSkills(List<Tuple<string, int>> preRequisiteSkills)
        {
            if(preRequisiteSkills==null)
            {
                writer.Write(0);
                return;
            }
            writer.Write(preRequisiteSkills.Count);
            foreach(Tuple<string,int> prereq in preRequisiteSkills)
            {
                writer.Write(prereq.Item1);
                writer.Write(prereq.Item2);
            }
        }


        
        void WriteEffect(ITimedEffect effect)
        {
            writer.Write(effect.EffectType);
            writer.Write(effect.BaseTime);
            writer.Write(effect.DeltaTime);
            writer.Write(effect.BaseDuration);
            writer.Write(effect.DeltaDuration);
            writer.Write(effect.GetParamValues().Length);
            foreach (string s in effect.GetParamValues())
                writer.Write(s);
        }


        void WriteDictionary(Dictionary<string, float> dictionary)
        {
            writer.Write(dictionary.Count);
            foreach (KeyValuePair<string, float> entry in dictionary)
            {
                writer.Write(entry.Key);
                writer.Write(entry.Value);
            }
        }

        void WriteListOfInt(List<int> list)
        {
            writer.Write(list.Count);
            foreach (int i in list)
                writer.Write(i);
        }
    }
}
