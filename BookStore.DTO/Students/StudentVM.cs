using BookStore.DTO.Classes;

namespace BookStore.DTO.Students
{
    public class StudentVM
    {
        public int Id { get; set; }

        ///<summary> 姓名 </summary>
        public string Name { get; set; } = "";

        ///<summary> 性别 </summary>
        public string? Gender { get; set; }

        ///<summary> 班级id </summary>
        public int ClassId { get; set; }
        public ClassVM Class { get; set; } = new();
    }
}
