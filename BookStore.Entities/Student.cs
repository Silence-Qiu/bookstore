namespace BookStore.Entities;

public class Student
{
    public int Id { get; set; }

    ///<summary> 姓名 </summary>
    public string Name { get; set; } = "";

    ///<summary> 性别 </summary>
    public string? Gender { get; set; }

    ///<summary> 班级id </summary>
    public int ClassId { get; set; }

    public Class Class { get; set; } = default!;
}