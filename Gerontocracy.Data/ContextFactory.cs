namespace Gerontocracy.Data
{
    public class ContextFactory 
    {
        public GerontocracyContext CreateDbContext() => new DesignTimeDbContextFactory().CreateDbContext(new string[] {});
    }
}
