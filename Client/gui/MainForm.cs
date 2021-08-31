using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Domain.model;
using Services;

namespace Client.gui
{
    public partial class MainForm : Form
    {
        private readonly MainController _ctrl;
        public readonly IList<string> usersData; 
        
        public MainForm(MainController ctrl)
        {
            InitializeComponent();
            this._ctrl = ctrl;
        }
        
        public void LoadData()
        {
            dataGridViewRace.DataSource = _ctrl.GetDtoRaces();
            dataGridViewRace.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            
            var teams = _ctrl.GetTeamsNames();
            comboBoxTeams.DataSource = teams;
            comboBoxTeams.SelectedItem = null;
            
            comboBoxSelectedTeam.DataSource = teams;
            comboBoxSelectedTeam.SelectedItem = null;
            
            LoadEngineCapacity();
            _ctrl.updateEvent += ParticipantUpdate;
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
            _ctrl.Logout();
            _ctrl.updateEvent -= ParticipantUpdate;
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
            
            dataGridViewMembers.DataSource = _ctrl.GetDtoParticipants(team.ToString());
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
                _ctrl.AddParticipantEntry(name,
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
            
            comboBoxSelectedRace.DataSource = _ctrl.RaceByEngineCapacity((EngineCapacity) Enum.Parse(
                typeof(EngineCapacity), engine.ToString()));
            
            if (comboBoxSelectedRace.Items.Count == 0)
            {
                comboBoxSelectedRace.DataSource = null;
            }
        }
        
        public void ParticipantUpdate(object sender, ContestParticipantEventArgs e)
        {
            if (e.ParticipantEventType == ContestParticipantEvent.ParticipantEntryAdded)
            {
                dataGridViewRace.BeginInvoke(new LoadRacesCallback(this.LoadRaces));
            }
        }

        private void ParticipantAdded()
        {
            var raceNmae = "";
            _ctrl.ParticipantEntryAdded(raceNmae);
            dataGridViewRace.BeginInvoke(new LoadRacesCallback(this.LoadRaces));
        }
        
        private void LoadRaces()
        {
            dataGridViewRace.DataSource = null;
            dataGridViewRace.DataSource = _ctrl.GetDtoRaces();
            dataGridViewRace.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private delegate void LoadRacesCallback();
    }
}