using UnityEngine;
using System.IO;

namespace Magic.Data
{


    public class SaveSystem : MonoBehaviour
    {
        private static string savePath = Path.Combine(Application.persistentDataPath, "save.json");

        public static void Save(SaveData data)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(savePath)!);

                string json = JsonUtility.ToJson(data, true);
                File.WriteAllText(savePath, json);
                Debug.Log("Game Saved to: " + savePath);
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error guardando en {savePath}\n{e}");
            }
        }

        public static SaveData Load()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    string json = File.ReadAllText(savePath);
                    SaveData data = JsonUtility.FromJson<SaveData>(json);
                    return data;
                }
                else
                {
                    Debug.LogWarning("Save file not found, returning new data.");
                    return new SaveData();
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error cargando {savePath}\n{e}");
                return new SaveData();
            }
        }

        public static void Delete()
        {
            try
            {
                if (File.Exists(savePath))
                {
                    File.Delete(savePath);
                    Debug.Log("Save file deleted.");
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"[SaveSystem] Error borrando {savePath}\n{e}");
            }
        }
    }
}
