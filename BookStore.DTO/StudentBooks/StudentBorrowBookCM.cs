namespace BookStore.DTO.StudentBooks
{
    public class StudentBorrowBookCM
    {
        ///<summary> 书籍id </summary>
        public int BookId { get; set; }

        ///<summary> 学生id </summary>
        public int StudentId { get; set; }

        ///<summary> 借出时间 </summary>
        public DateTime LendTime { get; set; }

        ///<summary> 数量 </summary>
        public int Count { get; set; }

        ///<summary> 还书时间 </summary>
        public DateTime? ReturnTime { get; set; }
    }
}
