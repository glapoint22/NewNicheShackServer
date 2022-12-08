namespace Manager.Domain.Entities
{
    public sealed class Subproduct
    {
        public Guid Id { get; set; }
        public string ProductId { get; set; } = string.Empty;
        public Guid? ImageId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Value { get; set; }
        public int Type { get; set; }

        public Media Media { get; set; } = null!;
        public Product Product { get; set; } = null!;


        public static Subproduct Create(string productId, int type)
        {
            Subproduct subproduct = new()
            {
                ProductId = productId,
                Type = type
            };

            return subproduct;
        }



        public void Update(string? name, string? description, Guid? imageId, double value)
        {
            Name = name;
            Description = description;
            ImageId = imageId;
            Value = value;
        }
    }
}