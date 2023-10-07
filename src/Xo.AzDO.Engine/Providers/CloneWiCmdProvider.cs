namespace Xo.AzDO.Cli.Providers;

internal class CloneWiCmdProvider : IProvider<CloneWiCmd>
{
    public CloneWiCmd Provide() => new CloneWiCmd
    {
        Id = 1116048,
        ParentId = 1101238,
        IterationName = "Sprint 13 2023",
        IterationBasePath = "Software\\Non-Aligned\\Customers and Emerging Markets\\Teams\\N2 Chapmans Peak Project Team\\2023",
        TeamName = "CEM - N2 Chapmans Peak Project Team",
        AreaPath = "Software\\Customers and Emerging Markets\\Rapid Response\\Project\\N2 Chapmans Peak Project Team",
        Tags = "sibot-sat-001",
    };
}
