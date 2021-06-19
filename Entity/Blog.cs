using FreeSql.DataAnnotations;

namespace Entity
{
    public class Blog
    {
        [Column(IsIdentity = true, IsPrimary = true)]
        public int BlogId { get; set; }

        public string Url { get; set; }
        public int Rating { get; set; }
        public string Name { get; set; }
    }
}