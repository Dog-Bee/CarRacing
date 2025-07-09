using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public sealed class JsonSavableValue<T>
{
    [Serializable] private class Wrapper
    {
        public T Value;
    }

    public event Action OnChanged = () => { };

    private readonly string playerPrefsPath;
    private T value;

    public T Value
    {
        get => value;
        set
        {
            PrevValue = this.value;
            this.value = value;
            SaveToPrefs();
            OnChanged?.Invoke();
        }
    }
    public T PrevValue { get; private set; }

    public JsonSavableValue(string playerPrefsPath, T defaultValue = default)
    {
        if (string.IsNullOrEmpty(playerPrefsPath))
        {
            throw new ArgumentException("playerPrefsPath cannot be null or empty.");
        }
        
        this.playerPrefsPath = playerPrefsPath;
        value = defaultValue;
        PrevValue = defaultValue;
        
        LoadFromPrefs();
        
    }

    private void LoadFromPrefs()
    {
        
        if (!PlayerPrefs.HasKey(playerPrefsPath))
        {
            SaveToPrefs();
            return;
        }
        
        var json = PlayerPrefs.GetString(playerPrefsPath);
        try
        {
            Wrapper wrapper = JsonUtility.FromJson<Wrapper>(json);
            if (wrapper.Value != null)
            {
                value = wrapper.Value;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to load JsonSavableValue<{typeof(T).Name}> from PlayerPrefs: {e}");
        }
        
    }
    
    private void SaveToPrefs()
    {
        var wrapper = new Wrapper {Value = value};
        var json = JsonUtility.ToJson(wrapper);
        PlayerPrefs.SetString(playerPrefsPath, json);
    }
}
