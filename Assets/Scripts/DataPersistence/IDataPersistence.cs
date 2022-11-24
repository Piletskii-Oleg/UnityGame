using DataPersistence.GameDataFiles;

namespace DataPersistence
{
    public interface IDataPersistence
    {
        void OnSave(GameData data);

        void OnLoad(GameData data);
    }
}