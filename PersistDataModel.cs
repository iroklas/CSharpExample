using Microsoft.EntityFrameworkCore;

public class ApiContext : DbContext
{
    public DbSet<ApiCall> Calls { get; set; }

    public string? DbPath { get; }

    public ApiContext(DbContextOptions<ApiContext> options) : base(options)
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "blogging.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}

public class ApiCall
{
    public int Id { get; set; }
    public string? Body { get; set; }
    public DateTime TimeSent { get; set; }
}