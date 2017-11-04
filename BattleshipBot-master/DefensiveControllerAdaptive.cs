using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class DefensiveControllerAdaptive
    {
        private const int startingAverage = 45;
        private Dictionary<DefensiveStrategy, double> DefensiveStrategyAverages;
        private double defensiveStrategyMovingAverage = startingAverage;
        DefensiveStrategy previousDefensiveStrategy;
        public DefensiveStrategy[] DefensiveStrategiesInUse = new DefensiveStrategy[] { DefensiveStrategy.Mixed, DefensiveStrategy.Uniform, DefensiveStrategy.Shield, DefensiveStrategy.Avoid, DefensiveStrategy.Drift };

        public DefensiveControllerAdaptive()
        {
            int numberOfDefensiveStrategies = Enum.GetNames(typeof(DefensiveStrategy)).Length;

            DefensiveStrategyAverages = new Dictionary<DefensiveStrategy, double>();
            foreach (var strat in DefensiveStrategiesInUse)
            {
                DefensiveStrategyAverages.Add(strat, startingAverage);
            }

        }
        public DefensiveStrategy GetDefensiveStrategy(bool wonLastBattle, int MatchNumber, int numberOfShots)
        {
            DefensiveStrategy nextDefensiveStrategy = DefensiveStrategy.Mixed;
            if (MatchNumber > 1)
            {
                if (!wonLastBattle)
                {
                    UpdateAverage(numberOfShots);
                    if (numberOfShots < 25)
                    {
                        nextDefensiveStrategy = GetHighestAverageStrategy();
                    }
                    else if (numberOfShots < defensiveStrategyMovingAverage)
                    {
                        nextDefensiveStrategy = GetHighestAverageStrategy();
                    }
                }
                else if (wonLastBattle && numberOfShots > DefensiveStrategyAverages[previousDefensiveStrategy] && numberOfShots > defensiveStrategyMovingAverage)
                {
                    UpdateAverage(numberOfShots);
                    nextDefensiveStrategy = previousDefensiveStrategy;
                }
                else
                {
                    nextDefensiveStrategy = previousDefensiveStrategy;
                }

                previousDefensiveStrategy = nextDefensiveStrategy;
                return nextDefensiveStrategy;

            }
            else
            {
                previousDefensiveStrategy = DefensiveStrategy.Mixed;
                return DefensiveStrategy.Mixed;
            }
        }

        private DefensiveStrategy GetHighestAverageStrategy()
        {

            double highestAverage = 0;
            DefensiveStrategy highestAverageDefensiveStrategy = DefensiveStrategy.Mixed;
            foreach (var stratAverage in DefensiveStrategyAverages)
            {
                if (stratAverage.Value > highestAverage)
                {
                    highestAverage = stratAverage.Value;
                    highestAverageDefensiveStrategy = stratAverage.Key;

                }
            }
            return highestAverageDefensiveStrategy;
        }

        private void UpdateAverage(int numberOfShots)
        {
            defensiveStrategyMovingAverage = defensiveStrategyMovingAverage * 4d / 5d + ((double)numberOfShots) / 5d;
            DefensiveStrategyAverages[previousDefensiveStrategy] = DefensiveStrategyAverages[previousDefensiveStrategy] * 3d / 4d + ((double)numberOfShots) / 4d;
        }
    }
}
