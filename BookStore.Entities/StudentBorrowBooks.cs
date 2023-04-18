namespace BookStore.Entities;

public class StudentBorrowBooks
{
    public int Id { get; set; }

    ///<summary> 书籍id </summary>
    public int BookId { get; set; }

    ///<summary> 数量 </summary>
    public int Count { get; set; }

    public Book Book { get; set; } = default!;

    ///<summary> 学生id </summary>
    public int StudentId { get; set; }

    public Student Student { get; set; }  = default!;

    ///<summary> 借出时间 </summary>
    public DateTime LendTime { get; set; }

    ///<summary> 还书时间 </summary>
    public DateTime? ReturnTime { get; set; }

    /// <summary> 归还状态 </summary>
    public StudentBorrowBooksStatus Status { get; set; }
}

public enum StudentBorrowBooksStatus
{
    未归还 = 0,
    已归还 = 1
}