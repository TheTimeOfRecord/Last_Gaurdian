public class DataManager : SingleTonBase<DataManager>
{
    public ItemDB itemDB;

    private void Start()
    {
        itemDB = new ItemDB();
    }
}