using DataPersistence.DataFiles;

namespace DataPersistence
{
    public interface IOptionsDataPersistence
    {
        void SaveOptionsToFile(OptionsData data);

        void LoadOptionsFromFile(OptionsData data);
    }
}