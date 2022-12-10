namespace Manager.Domain.Entities
{
    public sealed class Filter
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public static Filter Create(string name)
        {
            Filter filter = new()
            {
                Name = name
            };

            return filter;
        }



        // ---------------------------------------------------------------------------------- Set Name ---------------------------------------------------------------------------
        public void SetName(string name)
        {
            Name = name;
        }
    }
}