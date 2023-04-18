namespace BookStore.Entities;

public class Class
{
    public int Id { get; set; }

    ///<summary> 名称 </summary>
    public string Name { get; set; } = "";

    ///<summary> 年份 </summary>
    public int Year { get; set; }

    public List<Student> Students { get; set; } = new();
}