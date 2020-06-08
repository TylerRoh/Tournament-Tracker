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
            WireUpMatchupList();
            LoadMatch();
        }

        private void WireUpRoundsList()
        {
            roundDropDown.DataSource = Enumerable.Range(1, tournament.Rounds.Count).ToList();
        }
        private void WireUpMatchupList()
        {
            LoadMatchups((int)roundDropDown.SelectedItem);
            MatchupListBox.DataSource = null;
            MatchupListBox.DataSource = matchups;
            MatchupListBox.DisplayMember = "DisplayName";
        }

        private void roundDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            WireUpMatchupList();
        }

        /// <summary>
        /// populates your matchups list with the current round selected in the dropdown
        /// </summary>
        private void LoadMatchups(int roundNumber)
        {
            matchups = new List<MatchupModel>();

            foreach (List<MatchupModel> round in tournament.Rounds)
            {
                if (round.First().MatchupRound == roundNumber)
                {
                    if (unplayedOnlyCheckbox.Checked)
                    {
                        matchups = new List<MatchupModel>();
                        foreach (MatchupModel matchup in round)
                        {
                            if (matchup.Entries.First().Score == 0 && matchup.Entries.Last().Score == 0 && matchup.Entries.Count > 1)
                            {
                                matchups.Add(matchup);
                            }
                        }
                    }
                    else
                    {
                        matchups = round;
                        break;
                    }
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
            WireUpMatchupList();
        }

        private void scoreButton_Click(object sender, EventArgs e)
        {
            MatchupModel selectedMatchup = (MatchupModel)MatchupListBox.SelectedItem;
            //Validate Scores Entered
            if (ValidateScoring(selectedMatchup))
            {
                //Add Score to the matchup entry models
                selectedMatchup.Entries.First().Score = double.Parse(teamOneScoreValue.Text);
                selectedMatchup.Entries.Last().Score = double.Parse(teamTwoScoreValue.Text);
                WireUpMatchupList();
            }
            else
            {
                MessageBox.Show("The scoring is invalid. Please check and try again.");
            }
            

            //Determine if it is the last matchup in the round
            //If it is the last in the round generate the next round
            //Save data to the dbs
        }

        private bool ValidateScoring(MatchupModel selectedMatchup)
        {
            bool valid = true;
            double teamOneScore = 0;
            bool teamOneScoreValid = double.TryParse(teamOneScoreValue.Text, out teamOneScore);
            double teamTwoScore = 0;
            bool teamTwoScoreValid = double.TryParse(teamTwoScoreValue.Text, out teamOneScore);

            if (!teamOneScoreValid || !teamTwoScoreValid)
            {
                valid = false;
            }
            if (teamOneScore == teamTwoScore)
            {
                valid = false;
            }
            if (teamOneScore < 0 || teamTwoScore < 0)
            {
                valid = false;
            }
            if (selectedMatchup.Entries.Count < 2)
            {
                valid = false;
            }

            return valid;
        }
    }
}
