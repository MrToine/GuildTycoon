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
        }

        [ContextMenu("Créer un fait")]
        public void CreateFact()
        {
            SetFact<string>("fact", "Mon premier fait");
            _fact = GetFact<string>("fact");
        }
        
        [ContextMenu("Créer un fait persistant")]
        public void CreatePersistentFact()
        {
            SetFact<string>("fact", "Mon premier fait persistant", FactPersistence.Persistent);
            _fact = GetFact<string>("fact");
        }

        [ContextMenu("Sauver les donénes")]
        public void Sauver()
        {
            SaveFacts();
        }

        [ContextMenu("Charger les données")]
        public void Charger()
        {
            LoadFacts();
            
            GetAllFactsPlease();
        }
        
        [ContextMenu("Supprimer un fait")]
        public void RemoveFact()
        {
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
