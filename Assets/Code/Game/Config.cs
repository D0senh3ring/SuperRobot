using UnityEngine;

public static class Config
{
    public static float SfxVolume
    {
        set { PlayerPrefs.SetFloat("SfxVolume", value); }
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetFloat("SfxVolume");
        }
    }

    public static float MusicVolume
    {
        set { PlayerPrefs.SetFloat("MusicVolume", value); }
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetFloat("MusicVolume");
        }
    }

    public static int Deaths
    {
        set { PlayerPrefs.SetInt("Deaths", value); }
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetInt("Deaths");
        }
    }

    public static int Sacrifices
    {
        set { PlayerPrefs.SetInt("Sacrifices", value); }
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetInt("Sacrifices");
        }
    }

    public static void AddRespawn(RespawnReason reason)
    {
        if(reason == RespawnReason.Sacrifice)
        {
            Sacrifices++;
        }
        else
        {
            Deaths++;
        }
    }

    public static bool CompletedFirstLevel
    {
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetInt("Level1") > 0;
        }
        set { PlayerPrefs.SetInt("Level1", value ? 1 : 0); }
    }

    public static bool CompletedSecondLevel
    {
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetInt("Level2") > 0;
        }
        set { PlayerPrefs.SetInt("Level2", value ? 1 : 0); }
    }

    public static bool CompletedThridLevel
    {
        get
        {
            if (!IsValidConfig)
            {
                RestoreDefaultConfig();
            }
            return PlayerPrefs.GetInt("Level3") > 0;
        }
        set { PlayerPrefs.SetInt("Level3", value ? 1 : 0); }
    }

    public static void SetLevelCompleted(int index, bool value)
    {
        PlayerPrefs.SetInt("Level" + index, value ? 1 : 0);
        Save();
    }

    public static bool GetLevelCompleted(int index)
    {
        return PlayerPrefs.GetInt("Level" + index) > 0;
    }

    public static void Save()
    {
        PlayerPrefs.Save();
    }

    private static bool IsValidConfig
    {
        get { return PlayerPrefs.HasKey("SfxVolume") 
                && PlayerPrefs.HasKey("MusicVolume") 
                && PlayerPrefs.HasKey("Deaths") 
                && PlayerPrefs.HasKey("Sacrifices")
                && PlayerPrefs.HasKey("Level1")
                && PlayerPrefs.HasKey("Level2")
                && PlayerPrefs.HasKey("Level3"); }
    }

    private static void RestoreDefaultConfig()
    {
        MusicVolume = 0.5f;
        SfxVolume = 0.5f;
        Sacrifices = 0;
        Deaths = 0;

        CompletedFirstLevel = CompletedSecondLevel = CompletedThridLevel = false;

        Save();
    }
}
