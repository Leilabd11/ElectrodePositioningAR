using System.Collections.Generic;
using System.IO; // For file handling
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; private set; }

    private const string FILE_NAME = "C:\\Users\\Leila\\QRCODE\\Unity-ZXing-BarQrCodeHandling-main3\\Assets\\Scripts\\Avatar\\database.json"; // File name for storing data
    private Dictionary<string, UserData> database = new Dictionary<string, UserData>();

    [System.Serializable]
    public class UserData
    {
        public float fibulaLateralDisplacement;
        public float fibulaVerticalDisplacement;

        public float malleoulusLateralDisplacement;
        public float malleoulusVerticalDisplacement;

        public float spinaIliacaLateralDisplacement;
        public float spinaIliacaVerticalDisplacement;

        public float patellaLateralDisplacement;
        public float patellaVerticalDisplacement;
        public float patellaFrontalLateralDisplacement;
        
        public UserData(
            float fibulaLateralDisplacement = 0.0f,
            float fibulaVerticalDisplacement = 0.0f,
            float malleoulusLateralDisplacement = 0.0f,
            float malleoulusVerticalDisplacement = 0.0f,


            float electrodePLLateralDisplacement = 0.0f,
            float electrodePLVerticalDisplacement = 0.0f,


            float spinaIliacaLateralDisplacement = 0.0f,
            float spinaIliacaVerticalDisplacement = 0.0f,

            float patellaLateralDisplacement = 0.0f,
            float patellaVerticalDisplacement = 0.0f,
            float patellaFrontalLateralDisplacement = 0.0f)
        {
            this.fibulaLateralDisplacement = fibulaLateralDisplacement;
            this.fibulaVerticalDisplacement = fibulaVerticalDisplacement;

            this.malleoulusLateralDisplacement = malleoulusLateralDisplacement;
            this.malleoulusVerticalDisplacement = malleoulusVerticalDisplacement;

            this.spinaIliacaLateralDisplacement = spinaIliacaLateralDisplacement;
            this.spinaIliacaVerticalDisplacement = spinaIliacaVerticalDisplacement;

            this.patellaLateralDisplacement = patellaLateralDisplacement;
            this.patellaVerticalDisplacement = patellaVerticalDisplacement;
            this.patellaFrontalLateralDisplacement = patellaFrontalLateralDisplacement; 
        }
    }

    // Wrapper class for serialization
    [System.Serializable]
    private class DatabaseWrapper
    {
        public List<string> keys;
        public List<UserData> values;

        public DatabaseWrapper(Dictionary<string, UserData> dict)
        {
            keys = new List<string>(dict.Keys);
            values = new List<UserData>(dict.Values);
        }

        public Dictionary<string, UserData> ToDictionary()
        {
            var dict = new Dictionary<string, UserData>();
            for (int i = 0; i < keys.Count; i++)
            {
                dict[keys[i]] = values[i];
            }
            return dict;
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Load database when the game starts
            LoadDatabaseFromFile();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        // Save database when the game quits
        SaveDatabaseToFile();
    }

    public UserData LoadOrInitializeUserData(string name)
    {
        if (database.ContainsKey(name))
        {
            return database[name];
        }
        else
        {
            UserData newUser = new UserData();
            database.Add(name, newUser);
            return newUser;
        }
    }

    public void SaveData(string name, UserData data)
    {
        if (database.ContainsKey(name))
        {
            database[name] = data;
            Debug.Log("Data updated and saved for user: " + name);
        }
        else
        {
            database.Add(name, data);
            Debug.Log("New user data added for user: " + name);
        }

        // Save to file immediately to ensure persistence
        SaveDatabaseToFile();
    }

    public void SaveDatabaseToFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        // Serialize the database
        DatabaseWrapper wrapper = new DatabaseWrapper(database);
        string json = JsonUtility.ToJson(wrapper, true);

        // Write to file
        File.WriteAllText(filePath, json);
        Debug.Log($"Database saved to {filePath}");
    }

    public void LoadDatabaseFromFile()
    {
        string filePath = Path.Combine(Application.persistentDataPath, FILE_NAME);

        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);

            // Deserialize using the wrapper
            DatabaseWrapper wrapper = JsonUtility.FromJson<DatabaseWrapper>(json);
            database = wrapper.ToDictionary();

            Debug.Log($"Database loaded from {filePath}");
        }
        else
        {
            Debug.Log($"No database file found at {filePath}, initializing a new database.");
        }
    }

    // Example usage in Unity
    void Start()
    {
        
    }
}
