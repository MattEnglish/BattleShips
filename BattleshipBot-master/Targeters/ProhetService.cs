using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ProhetService
    {
        Map map;
        MoreUniformConfigs MUC;
        CoordinateValues coordValueHolder;
        AdvEnemyShipValueCalc AESCV;

        public ProhetService(CoordinateValues coordValues)
        {
            coordValueHolder = coordValues;
        }

        private double[,] GetAllSpaceValues(Map copyOfMap)
        {

            var spaceValues = new double[10, 10];

            foreach (int unfoundShipLength in copyOfMap.GetUnfoundShipsLengths())
            {
                var shipLengthSpaceValue = MUC.GetSpaceValueSumofCoordValuesGivenLegalPos(coordValueHolder.GetCoordinateValues(unfoundShipLength), unfoundShipLength, copyOfMap);
                for (int row = 0; row < 10; row++)
                {
                    for (int col = 0; col < 10; col++)
                    {
                        spaceValues[row, col] += unfoundShipLength * shipLengthSpaceValue[row, col];
                    }
                }

            }
            return spaceValues;
        }

        public int[] FindNewShipProphet(Map map, MoreUniformConfigs MUC)
        {
            this.map = map;
            this.MUC = MUC;
            var shotSequences = getNextPossibleShotSequences(map, null);

            shotSequences = shotSequences.OrderByDescending(shotSequence => shotSequence.TotalValue()).Take(5).ToList();
            for (int i = 0; i < 2; i++)
            {
                var nextShotSequences = new List<possibleShotSequence>();
                foreach (var seq in shotSequences)
                {
                    var nextMap = map.CreateCopy();
                    recordsMissesOnMap(nextMap, seq);
                    var partialNextShotSeq = getNextPossibleShotSequences(nextMap, seq).OrderByDescending(shotSequence => shotSequence.TotalValue()).Take(7 - 2 * i);
                    nextShotSequences.AddRange(partialNextShotSeq);
                }
                shotSequences = nextShotSequences.OrderByDescending(shotSequence => shotSequence.TotalValue()).Take(10).ToList();
                removeDuplicates(shotSequences);
            }
            var firstShot = shotSequences[0].getFirstShot();
            int[] target = new int[2] { firstShot.x, firstShot.y };
            return target;

        }

        private void removeDuplicates(List<possibleShotSequence> orderedSeqs)
        {
            var indexToRemove = new List<int>();
            var alreadyUsedShotsSeq = new List<List<Vector2>>();
            for (int i = 0; i < orderedSeqs.Count; i++)
            {
                var seqShots = orderedSeqs[i].GetAllShots();
                if (seqAlreadyInSeqsOrderUnimportant(alreadyUsedShotsSeq, seqShots))
                {
                    indexToRemove.Add(i);
                }
                else
                {
                    alreadyUsedShotsSeq.Add(seqShots);
                }
            }

            for (int i = indexToRemove.Count-1; i > -1; i--)
            {
                orderedSeqs.RemoveAt(indexToRemove[i]);
            }
        }

        private bool seqAlreadyInSeqsOrderUnimportant(List<List<Vector2>> seqs, List<Vector2> seq)
        {
            foreach (var usedSeq in seqs)
            {
                if(usedSeq.All(seq.Contains))
                {
                    return true;
                }
            }
            return false;
        }

        private void recordsMissesOnMap(Map mapCopy, possibleShotSequence seq)
        {
            mapCopy.shotFired(false, seq.Shot.x, seq.Shot.y);

            if (seq.PrevShotSequence != null)
            {
                recordsMissesOnMap(mapCopy, seq.PrevShotSequence);
            }
        }

        private List<possibleShotSequence> getNextPossibleShotSequences(Map mapCopy, possibleShotSequence prevSeq)
        {
            var shotSequences = new List<possibleShotSequence>();
            var spaceValues = GetAllSpaceValues(mapCopy);
            //MUC.GetAverageNon0ValueOfArray(spaceValues);
            for (int row = 0; row < 10; row++)
            {
                for (int column = 0; column < 10; column++)
                {
                    shotSequences.Add(new possibleShotSequence(prevSeq, new Vector2(row, column), spaceValues[row, column]));
                }
            }
            return shotSequences;
        }



    }

    class possibleShotSequence
    {
        public double ThisShotValue { get; private set; }
        public Vector2 Shot { get; private set; }
        public possibleShotSequence PrevShotSequence { get; }

        public possibleShotSequence(possibleShotSequence prevShotSequence, Vector2 shot, double thisShotValue)
        {
            PrevShotSequence = prevShotSequence;
            Shot = shot;
            ThisShotValue = thisShotValue;
        }

        public double TotalValue()
        {
            if (PrevShotSequence == null)
            {
                return ThisShotValue;
            }
            return ThisShotValue + 1.001 * PrevShotSequence.TotalValue();
        }

        public override string ToString()
        {
            return TotalValue().ToString() + ", " + Shot.x.ToString() + "," + Shot.y.ToString();
        }

        public Vector2 getFirstShot()
        {
            if (PrevShotSequence != null)
            {
                return PrevShotSequence.getFirstShot();
            }
            return Shot;
        }

        public List<Vector2> GetAllShots()
        {
            var shots = new List<Vector2>();
            shots.Add(Shot);
            if (PrevShotSequence != null)
            {
                return PrevShotSequence.GetAllShots(shots);
            }
            return shots;
        }

        public List<Vector2> GetAllShots(List<Vector2> shots)
        {
            shots.Add(Shot);
            if (PrevShotSequence != null)
            {
                return PrevShotSequence.GetAllShots();
            }
            return shots;
        }

    }
}
