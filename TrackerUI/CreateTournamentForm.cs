using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class CreateTournamentForm : Form, IPrizeRequester, ITeamRequester
    {
        private ITournamentRequester callingForm;
        private List<TeamModel> availableTeams = new List<TeamModel>(GlobalConfig.Connection.GetTeam_All());
        private List<TeamModel> selectedTeams = new List<TeamModel>();
        private List<PrizeModel> selectedPrizes = new List<PrizeModel>();

        public CreateTournamentForm(ITournamentRequester caller)
        {
            InitializeComponent();

            WireUpLists();

            callingForm = caller;
        }

        private void WireUpLists()
        {
            selectTeamDropDown.DataSource = null;

            selectTeamDropDown.DataSource = availableTeams;
            selectTeamDropDown.DisplayMember = "TeamName";

            tournamentPlayersListBox.DataSource = null;

            tournamentPlayersListBox.DataSource = selectedTeams;
            tournamentPlayersListBox.DisplayMember = "TeamName";

            prizesListBox.DataSource = null;

            prizesListBox.DataSource = selectedPrizes;
            prizesListBox.DisplayMember = "PlaceName";

        }

        private void CreateTournamentForm_Load(object sender, EventArgs e)
        {

        }

        private void removeSelectedPrizeButton_Click(object sender, EventArgs e)
        {
            PrizeModel p = (PrizeModel)prizesListBox.SelectedItem;

            if (p != null)
            {
                selectedPrizes.Remove(p);

                WireUpLists();
            }
        }

        private void tournamentPrizesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void addTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)selectTeamDropDown.SelectedItem;

            if (t != null)
            {
                availableTeams.Remove(t);
                selectedTeams.Add(t);

                WireUpLists();
            }
        }

        private void removeSelectedTeamButton_Click(object sender, EventArgs e)
        {
            TeamModel t = (TeamModel)tournamentPlayersListBox.SelectedItem;

            if (t != null)
            {
                selectedTeams.Remove(t);
                availableTeams.Add(t);

                WireUpLists();
            }
        }

        private void createPrizeButton_Click(object sender, EventArgs e)
        {
            // Call the Create Prize Form
            CreatePrizeForm frm = new CreatePrizeForm(this);
            frm.Show();
            
        }

        public void PrizeComplete(PrizeModel model)
        {
            //Get the PrizeModel back from form
            //Put the prize model into our selected prizes list
            selectedPrizes.Add(model);

            WireUpLists();
        }

        private void createNewTeamLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            CreateTeamForm frm = new CreateTeamForm(this);
            frm.Show();
        }

        public void TeamComplete(TeamModel model)
        {
            selectedTeams.Add(model);

            WireUpLists();
        }

        private bool ValidateForm()
        {
            bool output = true;
            decimal entryFee = 0;
            bool entryFeeValid = decimal.TryParse(entryFeeValue.Text, out entryFee);
            

            if (String.IsNullOrWhiteSpace(tournamentNameValue.Text))
            {
                output = false;
            }
            if (!entryFeeValid || entryFee < 0)
            {
                output = false;
            }
            if (selectedTeams.Count < 2)
            {
                output = false;
            }
            

            return output;
        }

        private void createTournamentButton_Click(object sender, EventArgs e)
        {


            //Create matchups
            if (ValidateForm())
            {
                TournamentModel tournament = new TournamentModel(
                    tournamentNameValue.Text,
                    entryFeeValue.Text,
                    selectedTeams,
                    selectedPrizes);

                //Wire up Matchups
                TournamentLogic.CreateRounds(tournament);



                GlobalConfig.Connection.CreateTournament(tournament);
            }
            else
            {
                MessageBox.Show("This form has invalid information. Please check and try again.", 
                    "Invalid Information",
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error);
                
            }
        }
    }
}
