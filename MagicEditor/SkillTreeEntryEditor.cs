﻿using GameObject;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MagicEditor
{
    public partial class SkillTreeEntryEditor : Form
    {
        public GameObject.SkillTreeEntry Entry;
        public MainForm AbilityProvider;
        public SkillTreeEntryEditor(MainForm Parent,SkillTreeEntry Entry)
        {
            InitializeComponent();
            this.Entry = Entry;
            this.AbilityProvider = Parent;
            ModularAbility a = Parent.FindAbility(Entry.SkillID);
            SkillName.Text = a.Name;
            learnlevel.Value = (decimal)Entry.LearnLevel;
            column.Value = (decimal)Entry.Column;
            traininglevel.Value = (decimal)Entry.TrainingLevel;
            maxlvl.Value = (decimal)Entry.MaxLevel;
            expbase.Value = (decimal)Entry.ExpBase;
            expdelta.Value = (decimal)Entry.ExpDelta;
            requireitemid.Text = Entry.RequireItemID;
            ReloadReqList();
        }

        private void ReloadReqList()
        {
            if (Entry.PreRequisiteSkills == null)
                return;
            foreach(Tuple<string, int> t in Entry.PreRequisiteSkills)
            {
                AddReqItem(t.Item1, t.Item2);
            }
        }

        private void AddReqItem(string id, int lvl)
        {
            ModularAbility a = AbilityProvider.FindAbility(id);
            string[] ItemProps = new string[] { a.Name, lvl.ToString() };
            ListViewItem line = new ListViewItem(ItemProps);
            line.Tag = id;
            requisitelist.Items.Add(line);
        }

        private List<Tuple<string, int>> GetPreReqs()
        {
            List<Tuple<string, int>> result = new List<Tuple<string, int>>();
            foreach(ListViewItem item in requisitelist.Items)
            {
                string id = (string)item.Tag;
                string slvl = item.SubItems[1].Text;
                int lvl = int.Parse(slvl);
                result.Add(new Tuple<string, int>(id, lvl));
            }
            return result;
        }

        private void requisitelistmenu_Opening(object sender, CancelEventArgs e)
        {
            requisitelistmenu.Items[1].Enabled = requisitelist.SelectedItems.Count == 1;
        }

        private void addRequisiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SkillSelector selector = new SkillSelector(this.AbilityProvider);
            if(selector.ShowDialog()==DialogResult.OK)
            {
                AddReqItem(selector.SelectedID, selector.SelectedLevel);

            }
        }

        private void removeRequisiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (requisitelist.SelectedItems.Count != 1)
                return;
            requisitelist.Items.Remove(requisitelist.SelectedItems[0]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<Tuple<string, int>> g = GetPreReqs();
            int f = g.Count;
        }
    }
}
