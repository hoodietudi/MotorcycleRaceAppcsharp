using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Domain.model;
using Domain.model.validators;
using MotorcycleContest.service;
using Services;

namespace Client.gui
{
    public partial class MainForm : Form
    {
        private readonly MainController ctrl;
        private readonly IList<string> usersData; 
        
        public MainForm(MainController ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
        }
        
        public void LoadData()
        {
            dataGridViewRace.DataSource = ctrl.GetDtoRaces();
            dataGridViewRace.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            var teams = ctrl.GetTeamsNames();
            comboBoxTeams.DataSource = teams;
            comboBoxTeams.SelectedItem = null;
            
            comboBoxSelectedTeam.DataSource = teams;
            comboBoxSelectedTeam.SelectedItem = null;
            
            LoadEngineCapacity();
            ctrl.updateEvent += participantUpdate;
        }
        
        private void LoadEngineCapacity()
        {
            var engine = Enum.GetValues(typeof(EngineCapacity)).Cast<EngineCapacity>()
                .Select(engineCapacity => engineCapacity.ToString()).ToList();

            comboBoxSelectedEngineCapacity.DataSource = engine;
            comboBoxSelectedEngineCapacity.SelectedItem = null;
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            ctrl.Logout();
            ctrl.updateEvent -= participantUpdate;
            Application.Exit();
        }

        private void buttonSearchMembers_Click(object sender, EventArgs e)
        {
            var team = comboBoxTeams.SelectedItem;
            
            if (team == null)
            {
                MessageBox.Show(@"Select a team!");
                return;
            }
            
            dataGridViewMembers.DataSource = ctrl.GetDtoParticipants(team.ToString());
            dataGridViewMembers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var name = textBoxName.Text;
            var team = comboBoxSelectedTeam.SelectedItem;
            var engineCapacity = comboBoxSelectedEngineCapacity.SelectedItem;
            var race = (Race)comboBoxSelectedRace.SelectedItem;
            
            if (name == null || team == null || engineCapacity == null || race == null)
            {
                MessageBox.Show(@"Input error!");
                return;
            }
            
            try
            {
                ctrl.AddParticipantEntry(name,
                    team.ToString(), engineCapacity.ToString(), race.Name);
            
                MessageBox.Show(@"Participant was succesfully added!");
            }
            catch (ContestException ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void comboBoxSelectedEngineCapacity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var engine = comboBoxSelectedEngineCapacity.SelectedItem;
            
            if (engine == null)
            {
                comboBoxSelectedRace.DataSource = null;
                return;
            }
            
            comboBoxSelectedRace.DataSource = ctrl.RaceByEngineCapacity((EngineCapacity) Enum.Parse(
                typeof(EngineCapacity), engine.ToString()));
            
            if (comboBoxSelectedRace.Items.Count == 0)
            {
                comboBoxSelectedRace.DataSource = null;
            }
        }
        
        public void participantUpdate(object sender, ContestParticipantEventArgs e)
        {
            if (e.ParticipantEventType == ContestParticipantEvent.ParticipantEntryAdded)
            {
                dataGridViewRace.BeginInvoke(new LoadRacesCallback(this.LoadRaces));
            }
        }

        private void ParticipantAdded()
        {
            var raceNmae = "";
            ctrl.ParticipantEntryAdded(raceNmae);
            // BindingList<RaceDTO> races = new BindingList<RaceDTO>();
            //
            // foreach (var race in dataGridViewRace.Rows)
            // {
            //     Console.WriteLine(race);
            // }
            dataGridViewRace.BeginInvoke(new LoadRacesCallback(this.LoadRaces));
        }
        
        private void LoadRaces()
        {
            dataGridViewRace.DataSource = null;
            dataGridViewRace.DataSource = ctrl.GetDtoRaces();
            dataGridViewRace.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        public delegate void LoadRacesCallback();
    }
}