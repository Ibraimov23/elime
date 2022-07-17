namespace ElimeProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public int Coll { get; set; }
        public int Dopy { get; set; }
        public string Comment { get; set; }
        public int Price { get; set; }
        public int Discount { get; set; }
        public int Total { get; set; }
    }
}
