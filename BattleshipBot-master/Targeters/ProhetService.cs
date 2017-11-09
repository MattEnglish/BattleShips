using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    public class ProphetService
    {
        Map map;
        MoreUniformConfigs MUC = new MoreUniformConfigs();
        CoordinateValues coordValueHolder;
        AdvEnemyShipValueCalc AESVC;
        Dictionary<int, double[,,]> coordValuesAfterAESCV = new Dictionary<int, double[,,]>();
        double[,] originalSpaceValues;

        public ProphetService(CoordinateValues coordValues, AdvEnemyShipValueCalc aesvc, Map map)
        {
            coordValueHolder = coordValues;
            AESVC = aesvc;
            this.map = map;
        }

        private double[,] GetAllSpaceValues()
        {

            var spaceValues = new double[10, 10];

            foreach (int unfoundShipLength in map.GetUnfoundShipsLengths())
            {
                var shipLengthSpaceValue = MUC.GetSpaceValueSumofCoordValuesGivenLegalPos(coordValueHolder.GetCoordinateValues(unfoundShipLength), unfoundShipLength, map);
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

        private void createCoordValues()
        {
            for (int shipLength = 2; shipLength < 6; shipLength++)
            {
                var foundShips = map.GetShips().ToList();
                coordValuesAfterAESCV.Add(shipLength, AESVC.GetShipRecordedValuesRememberThreesAreDoubled(shipLength, foundShips));
            }
        }

        private double [,] AlterSpaceValues(double[,] spaceValues, Vector2 missPoss, List<Vector2>otherMisses)
        {
            foreach (var unfoundShipLength in map.GetUnfoundShipsLengths())
            {
                spaceValues = AlterSpaceValueService.alterSpaceValues(spaceValues, coordValuesAfterAESCV[unfoundShipLength], missPoss, map, otherMisses, unfoundShipLength);
            }
            return spaceValues;
        }
        public possibleShotSequence findPossibleShotSequence()
        {
            createCoordValues();
            this.MUC = MUC;
            originalSpaceValues = GetAllSpaceValues();
            var shotSequences = getNextPossibleShotSequences(null,originalSpaceValues);
            shotSequences = shotSequences.OrderByDescending(shotSequence => shotSequence.TotalValue()).Take(10).ToList();
            var nextShotSequences = new List<possibleShotSequence>();

            for (int i = 0; i < 10; i++)
            {
                foreach (var seq in shotSequences)
                {
                    var newSpaceValues = new double[10, 10];
                    Array.Copy(originalSpaceValues, newSpaceValues, 100);
                    newSpaceValues = seq.getNewSpaceValues(map, newSpaceValues, coordValuesAfterAESCV);
                    var partialNextShotSeq = getNextPossibleShotSequences(seq, newSpaceValues);
                    nextShotSequences.AddRange(partialNextShotSeq);
                }
                shotSequences = nextShotSequences.OrderByDescending(shotSequence => shotSequence.TotalValue()).ToList();
                removeDuplicates(shotSequences);
                shotSequences = shotSequences.Take(15).ToList();
            }
            return shotSequences[0];


        }



        public int[] FindNewShipProphet(Map map, MoreUniformConfigs MUC)
        {/*
            createCoordValues();
            this.map = map;
            this.MUC = MUC;
            var shotSequences = getNextPossibleShotSequences(map, null);
            originalSpaceValues = GetAllSpaceValues(map);

            shotSequences = shotSequences.OrderByDescending(shotSequence => shotSequence.TotalValue()).Take(10).ToList();
            for (int i = 0; i < 10; i++)
            {
                var nextShotSequences = new List<possibleShotSequence>();
                foreach (var seq in shotSequences)
                {
                    var nextMap = map.CreateCopy();
                    recordsMissesOnMap(nextMap, seq);
                    var partialNextShotSeq = getNextPossibleShotSequences(nextMap, seq).OrderByDescending(shotSequence => shotSequence.TotalValue()).Take(10 - i);
                    nextShotSequences.AddRange(partialNextShotSeq);
                }
                shotSequences = nextShotSequences.OrderByDescending(shotSequence => shotSequence.TotalValue()).ToList();
                removeDuplicates(shotSequences);
                shotSequences = shotSequences.Take(15).ToList();
            }
            var firstShot = shotSequences[0].getFirstShot();
            int[] target = new int[2] { firstShot.x, firstShot.y };
            return target;
            */
            return new int[2];

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

        private List<possibleShotSequence> getNextPossibleShotSequences(possibleShotSequence prevSeq, double[,] spaceValues)
        {
            var shotSequences = new List<possibleShotSequence>();
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

    public class possibleShotSequence
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

        public double[,] getNewSpaceValues(Map map, double[,] originalSpaceValues, Dictionary<int, double[,,]> coordinateValues)
        {
            return getNewSpaceValues(map, originalSpaceValues, new List<Vector2>(), coordinateValues);
        }

        public double[,] getNewSpaceValues(Map map, double[,] spaceValues, List<Vector2> missesLaterInSeq, Dictionary<int, double[,,]> coordinateValues)
        {
            for (int i = 2; i <= 5; i++)
            {
                spaceValues = AlterSpaceValueService.alterSpaceValues(spaceValues, coordinateValues[i], Shot, map, missesLaterInSeq, i);
            }
            missesLaterInSeq.Add(Shot);
            if (PrevShotSequence != null)
            {
                return PrevShotSequence.getNewSpaceValues(map, spaceValues, missesLaterInSeq, coordinateValues);
            }
            return spaceValues;
        }
       


    }
}
