using System;
using System.Collections.Generic;
using Core.Runtime;
using Core.Runtime.Interfaces;
using UnityEngine;

namespace TestFacts.Runtime
{
    public class TestFacts : BaseMonobehaviour
    {
        string _fact;

        void Start()
        {
            GameManager.Instance.Profile = "Guilde_de_Toine";
            Info($"Test de Facts sur le profile : {GameManager.Instance.Profile}");
        }

        [ContextMenu("Créer un fait")]
        public void CreateFact()
        {
            SetFact<string>("fact", "Mon premier fait");
            _fact = GetFact<string>("fact");
            Info($"Le Fact {_fact} à été créer avec succès");
        }
        
        [ContextMenu("Créer un fait persistant")]
        public void CreatePersistentFact()
        {
            SetFact<string>("fact", "Mon premier fait persistant", FactPersistence.Persistent);
            _fact = GetFact<string>("fact");
            Info($"Le Fact {_fact} à été créer avec succès et est persistant");
        }

        [ContextMenu("Sauver les donénes")]
        public void Sauver()
        {
            SaveFacts();
            Info("Faits sauvegardés");
        }

        [ContextMenu("Charger les données")]
        public void Charger()
        {
            LoadFacts();
            
            Info("Liste des faits chargés :");
            GetAllFactsPlease();
        }
        
        [ContextMenu("Supprimer un fait")]
        public void RemoveFact()
        {
            Info("Le fait à été supprimé");
            RemoveFact<string>("fact");
        }

        [ContextMenu("Supprimer la sauvegarde")]
        public void DeleteSave()
        {
            DeleteSaveFile();
        }

        [ContextMenu("Voir tous les faits")]
        public void GetAllFactsPlease()
        {
            foreach (var fact in GetAllFacts())
            {
                Info($"Fact[{fact.Key}] = {fact.Value}");
            }
        }
    }
}
