using Highlander_Logic;
using System;
using System.Diagnostics;
using System.IO;
using System.ServiceModel.Channels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Highlander
{
    public sealed partial class MainPage : Page
    {
        private SimulationManager simulationManager;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void StartSimulation_Click(object sender, RoutedEventArgs e)
        {
            // Get user input and start simulation
            if (AutoGenerateCheckBox.IsChecked == true)
            {
                Random rand = new Random();
                int gridSize = rand.Next(101);
                int goodHighlanders = rand.Next((int)(0.2 * gridSize));
                int badHighlanders = rand.Next((int)(0.3 * gridSize));
                Debug.WriteLine(gridSize + " " + goodHighlanders + " " + badHighlanders);
                simulationManager = new SimulationManager(gridSize, goodHighlanders, badHighlanders);
                simulationManager.StartSimulation();
                SimulationResultTextBlock.Text = simulationManager.GetSimulationResult();
                Highlander_Logic.Logger.logEvent(simulationManager.GetSimulationResult());
                Highlander_Logic.Logger.writeEvents();
            }
            else if (Int32.TryParse(GridSizeTextBox.Text, out int gridSize)
            && Int32.TryParse(GoodHighlandersTextBox.Text, out int goodHighlanders)
            && Int32.TryParse(BadHighlandersTextBox.Text, out int badHighlanders))
            {
                simulationManager = new SimulationManager(gridSize, goodHighlanders, badHighlanders);
                simulationManager.StartSimulation();
                SimulationResultTextBlock.Text = simulationManager.GetSimulationResult();
                Highlander_Logic.Logger.logEvent(simulationManager.GetSimulationResult());
                Highlander_Logic.Logger.writeEvents();
            }
        }

        private void StopSimulation_Click(object sender, RoutedEventArgs e)
        {
            // Stop the simulation
            simulationManager.StopSimulation();

            // Display simulation results
            SimulationResultTextBlock.Text = simulationManager?.GetSimulationResult();
            Highlander_Logic.Logger.logEvent(simulationManager.GetSimulationResult());
        }
    }
}
