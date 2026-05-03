using Assets.Scripts.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Services
{
    public static class SaveService
    {
        private static string path = Application.persistentDataPath + "/save.json";
        private static readonly JsonSerializerSettings Settings = new()
        {
            MissingMemberHandling = MissingMemberHandling.Ignore,
            NullValueHandling = NullValueHandling.Ignore,
            Converters = { new StringEnumConverter() },
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };
        public static void Save()
        {
            SaveData data = new SaveData
            {
                config = GameManager.Instance.Config,
                player = new PlayerData
                {
                    level = GameManager.Instance.Player.LevelSystem.Level,
                    experience = GameManager.Instance.Player.LevelSystem.Experience,
                    moves = GameManager.Instance.Player.Moves,
                    selectedMoves = GameManager.Instance.Player.Hero.Moves
                },
                defeatedEnemiesId = GameManager.Instance.DefeatedEnemies.ToList()
            };

            string json = JsonConvert.SerializeObject(data, Settings);
            File.WriteAllText(path, json);

            Debug.Log("Game saved to: " + path);
        }

        public static SaveData Load()
        {
            if (!File.Exists(path))
            {
                Debug.LogWarning("No save file found!");
                return null;
            }

            string json = File.ReadAllText(path);
            SaveData data = JsonConvert.DeserializeObject<SaveData>(json, Settings);

            return data;
        }
    }
}
