using UnityEngine;
using System.IO;

namespace Magic.Data
{


    public class SaveSystem : MonoBehaviour
    {
        private static string SavePath => Path.Combine(Application.persistentDataPath, "save.json");

#if UNITY_WEBGL && !UNITY_EDITOR
        [DllImport("__Internal")] private static extern void SyncFiles();
#endif

        public static void Save(SaveData data)
        {
            try
            {
                var dir = Path.GetDirectoryName(SavePath);
                if (!string.IsNullOrEmpty(dir))
                    Directory.CreateDirectory(dir);

                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(SavePath, json);
              
#if UNITY_WEBGL && !UNITY_EDITOR
                SyncFiles();
#endif

                Debug.Log("Game Saved to: " + SavePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error guardando en {SavePath}\n{e}");
            }
        }

        public static SaveData Load()
        {
            try
            {
                if (File.Exists(SavePath))
                {
                    string json = File.ReadAllText(SavePath);
                    return JsonUtility.FromJson<SaveData>(json);
                }

                Debug.LogWarning("Save file not found, returning new data.");
                return new SaveData();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error cargando {SavePath}\n{e}");
                return new SaveData();
            }
        }

        public static void Delete()
        {
            try
            {
                if (File.Exists(SavePath))
                {
                    File.Delete(SavePath);

#if UNITY_WEBGL && !UNITY_EDITOR
                    SyncFiles();
#endif

                    Debug.Log("Save file deleted: " + SavePath);
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error borrando {SavePath}\n{e}");
            }
        }
    }
}

