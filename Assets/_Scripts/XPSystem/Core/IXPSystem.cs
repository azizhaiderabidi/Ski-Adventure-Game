// _Scripts/Systems/IXPSystem.cs
public interface IXPSystem
{
    int GetXP();
    void GainXP(int amount);
    void ResetXP();
    void SetXP(int xp); // Needed for loading
}
