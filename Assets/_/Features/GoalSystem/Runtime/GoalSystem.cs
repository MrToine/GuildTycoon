using System.Collections.Generic;
using Core.Runtime;
using UnityEngine;

namespace Goals.Runtime
{
    public class GoalSystem : BaseMonobehaviour
    {

        #region Publics

        List<Goal> AllGoals { get; set; } = new List<Goal>();

        #endregion


        #region Unity API

        //

        #endregion


        #region Main Methods

        public void AddGoal(Goal goal)
        {
            SetFact<Goal>(goal.Id, goal, FactPersistence.Persistent);
            AllGoals.Add(goal);
        }

        public void EvaluateGoals(FactDictionnary goalsFacts)
        {
            //
        }

        public List<Goal> GetGoalsByState(GoalState state)
        {
            return AllGoals != null ? AllGoals.FindAll(goal => goal.State == state) : new List<Goal>();
        }
        
        public bool AreAllGoalsCompleted()
        {
            return AllGoals != null && AllGoals.TrueForAll(goal => goal.State == GoalState.Completed);
        }

        #endregion


        #region Utils

        /* Fonctions privées utiles */

        #endregion


        #region Privates and Protected

        // Variables privées

        #endregion
    }
}

