using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TrackerLibrary.Models;

namespace TrackerUI
{
    public partial class TournamentViewerForm : Form
    {
        private TournamentModel tournament;


        private List<MatchupModel> matchups = new List<MatchupModel>();


        public TournamentViewerForm(TournamentModel model)
        {
            InitializeComponent();

            tournament = model;

            LoadFormData();

            roundDropDown.SelectedIndexChanged += roundDropDown_SelectedIndexChanged;
            MatchupListBox.SelectedIndexChanged += MatchupListBox_SelectedIndexChanged;
        }

        private void LoadFormData()
        {
            tournamentName.Text = tournament.TournamentName;
            WireUpRoundsList();
            LoadMatchups(1);
            WireUpMatchupList();
            LoadMatch();
        }

        private void WireUpRoundsList()
        {
            roundDropDown.DataSource = Enumerable.Range(1, tournament.Rounds.Count).ToList();
        }
        private void WireUpMatchupList()
        {
            MatchupListBox.DataSource = null;
            MatchupListBox.DataSource = matchups;
            MatchupListBox.DisplayMember = "DisplayName";
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
            WireUpMatchupList();
        }

        /// <summary>
        /// populates your matchups list with the current round selected in the dropdown
        /// </summary>
        private void LoadMatchups(int roundNumber)
        { 

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                if (round.First().MatchupRound == roundNumber)
                {
                    matchups = round;
                    break;
                }
            }
          
        }
        private void LoadMatch()
        {
            MatchupModel selectedMatchup = (MatchupModel)MatchupListBox.SelectedItem;
            if (selectedMatchup != null)
            {
                if (selectedMatchup.Entries.Count > 1)
                {
                    if (selectedMatchup.Entries.First().TeamCompeting != null)
                    {
                        teamOneName.Text = selectedMatchup.Entries.First().TeamCompeting.TeamName;
                        teamTwoName.Text = selectedMatchup.Entries.Last().TeamCompeting.TeamName;
                        teamOneScoreValue.Text = selectedMatchup.Entries.First().Score.ToString();
                        teamTwoScoreValue.Text = selectedMatchup.Entries.Last().Score.ToString();
                    }
                    else
                    {
                        teamOneName.Text = "TBD";
                        teamTwoName.Text = "TBD";
                        teamOneScoreValue.Text = null;
                        teamTwoScoreValue.Text = null;
                    }
                }
                else
                {
                    teamOneName.Text = selectedMatchup.Entries.First().TeamCompeting.TeamName;
                    teamTwoName.Text = "Bye";
                    teamOneScoreValue.Text = null;
                    teamTwoScoreValue.Text = null;
                }
            }
            else
            {
                teamOneName.Text = null;
                teamTwoName.Text = null;
                teamOneScoreValue.Text = null;
                teamTwoScoreValue.Text = null;
            }
            
        }
        private void MatchupListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMatch();
        }

        private void unplayedOnlyCheckbox_CheckedChanged(object sender, EventArgs e)
        {
        }
    }
}
