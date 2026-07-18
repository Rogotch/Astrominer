using UnityEngine;

public interface IEquipmentService
{
    IDigInstrument currentDigTool {get;}
    void EquipTool(IDigInstrument instrument);
}

public class EquipmentService : IEquipmentService
{
    public IDigInstrument currentDigTool { get; private set;}

    public void EquipTool(IDigInstrument instrument)
    {
        currentDigTool = instrument;
    }
}