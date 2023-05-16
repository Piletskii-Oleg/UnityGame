using System.Collections.Generic;
using DataPersistence.DataFiles;
using Inventory;
using NPC.Dialogue;
using UnityEngine;
using Weapons;

namespace DataPersistence
{
    /// <summary>
    /// Scriptable object that manages saving and loading from file.
    /// </summary>
    [CreateAssetMenu(menuName = "Managers/Game Data Manager")]
    public class GameDataManager : BaseDataManager<GameData, DataManager<GameData>>
    {
        [Header("Dialogue Managers")]
        [SerializeField] private DialogueManager[] dialogueManagers;
        [Header("New Game Data")]
        [SerializeField] private Quaternion baseRotation;
        [SerializeField] private Vector3 basePosition;

        [SerializeField] private List<GunItem> startingWeapons;
        [SerializeField] private List<InventoryItem> startingItems;
        
        public void NewGame()
        {
            var data = new GameData
            {
                storedWeapons = startingWeapons,
                storedItems = startingItems,
                playerPosition = basePosition,
                playerRotation = baseRotation,
            };

            foreach (var dialogueManager in dialogueManagers)
            {
                dialogueManager.ResetDialogue();
            }
            
            LoadFrom(data);
            Save();
        }
    }
}