using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Stats
{
    public class TraitStore : MonoBehaviour
    {
        private Dictionary<Trait, int> assignedPoints = new Dictionary<Trait, int>();
        private Dictionary<Trait, int> stagedPoints = new Dictionary<Trait, int>();
        
        public int GetProposedPoints(Trait trait)
        {
            return GetPoints(trait) + GetStagedPoints(trait);
        }

        public int GetPoints(Trait trait)
        {
            return assignedPoints.ContainsKey(trait) ? assignedPoints[trait] : 0;
        }

        public int GetStagedPoints(Trait trait)
        {
            return stagedPoints.ContainsKey(trait) ? stagedPoints[trait] : 0;
        }

        public void AssignPoints(Trait trait, int points)
        {
            if (!CanAssignPoints(trait, points)) return;

            stagedPoints[trait] = GetStagedPoints(trait) + points;
        }

        public bool CanAssignPoints(Trait trait, int points)
        {
            if (GetStagedPoints(trait) + points < 0) return false;
            return GetUnassignedPoints() >= points;
        }

        public int GetUnassignedPoints()
        {
            return GetAssignablePoints() - GetTotalProposedPoints();
        }

        public int GetTotalProposedPoints()
        {
            // return stagedPoints.Keys.Sum(trait => stagedPoints[trait]);

            return assignedPoints.Values.Sum() + stagedPoints.Values.Sum();
        }
        

        public void Commit()
        {
            foreach (var trait in stagedPoints.Keys)
            {
                assignedPoints[trait] = GetProposedPoints(trait);
            }
            
            stagedPoints.Clear();
        }

        public int GetAssignablePoints()
        {
            return (int)GetComponent<BaseStats>().GetStat(Stat.TotalTraitPoints);
        }
    }
}