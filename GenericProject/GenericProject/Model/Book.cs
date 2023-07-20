using System.ComponentModel.DataAnnotations.Schema;

namespace GenericProject.Model
{

    [Table("books")]
    public class Book
    {
        [Column("id")]
        private long Id;

        [Column("author")]
        private string Author;

        [Column("title")]
        private string Title;

        [Column("launch_date")]
        private string LauchDate;

        [Column("price")]
        private string Price;

    }
}
