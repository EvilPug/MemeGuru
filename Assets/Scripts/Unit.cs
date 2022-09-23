using SQLite4Unity3d;

public class Unit
{

    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Cost { get; set; }
    public int Income { get; set; }

    public override string ToString()
    {
        return string.Format("[Unit: Id={0}, Name={1},  Description={2}, Cost={3}, Income={4}]", Id, Name, Description, Cost, Income);
    }
}