using BookStore.DTO.Books;
using BookStore.DTO.Classes;
using BookStore.DTO.StudentBooks;
using BookStore.DTO.Students;
using BookStore.Entities;

namespace BookStore.DTO
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<Class, ClassVM>();
            CreateMap<ClassUM, Class>();

            CreateMap<Student, StudentVM>();
            CreateMap<StudentUM, Student>();

            CreateMap<Book, BookVM>();
            CreateMap<BookUM, Book>();

            CreateMap<StudentBorrowBooks, StudentBorrowBookVM>();
            CreateMap<StudentBorrowBookCM, StudentBorrowBooks>();
        }
    }
}
