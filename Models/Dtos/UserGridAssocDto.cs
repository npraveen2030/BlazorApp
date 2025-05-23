namespace BlazorApp.Models.Dtos
{
    public class GridMeta
    {
        public string Width { get; set; } = "";
        public int OrderIndex { get; set; }
        public bool Visible { get; set; } = true;
    }

    public class UserGridAssocDto
    {
        public int Ugaid { get; set; }

        public int UserId { get; set; }

        public int GroupId { get; set; }

        public int TabIs { get; set; }

        public string? Preferences { get; set; }
    }
}
