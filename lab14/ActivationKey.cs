namespace lab14;

/// <summary>
/// Активационный ключ, тип данных, требуемый по заданию с join
/// </summary>
public struct ActivationKey
{
    public string GameName; // название игры
    public string Key; // ключ активации игры

    public ActivationKey(string gameName, string key)
    {
        GameName = gameName;
        Key = key;
    }

    public override string ToString()
    {
        return $"Название игры={GameName}; Ключ={Key}";
    }
}