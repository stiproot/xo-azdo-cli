namespace Xo.AzDO.Cli.Providers;

internal class DashboardWorkflowCmdProvider : IProvider<CreateDashboardWorkflowCmd>
{
	public CreateDashboardWorkflowCmd Provide() => new CreateDashboardWorkflowCmd
	{
		IterationName = "Sprint 8",
		IterationBasePath = "Software\\Non-Aligned\\Customers and Emerging Markets\\Teams\\N2 Chapmans Peak Project Team\\2023",
		TeamName = "CEM - N2 Chapmans Peak Project Team",
		DashboardName = "Chapmans Peak Sprint 8",
		QueryFolderBasePath = "Shared Queries/Customers and Emerging Markets/Rapid Response/N2 Chapmans Peak Project Team",
		Initiatives = new List<Initiative>
		{
			new()
			{
				Title = "Address Finder Module - Loqate Hotfix (DGC)",
				Tag = "N2CP-DGC-LoqateHotFix",
				Desc= "<<insert description here please>>",
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "Address Finder Module - Mock Modules",
				Tag = "N2CP-AddressFinderMockModule",
				Desc= "<<insert description here please>>",
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "Axiom USA Template",
				Tag = "N2CP-AxiomUSATemplate",
				Desc = "This goal is to understand, use and configure the USA Market Delivery Template on an Axiom Environment",
				Links = new Dictionary<string, string> { { "Repo", "https://dev.azure.com/Derivco/Software/_git/Internal-Axiom-Build-USAMarketDelivery" } },
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "KYC Waiting Time",
				Tag = "N2CP-KYCWaitingTime",
				Desc = "This goal is to assist players by implementing a mailer notification to inform the risk agent of a failure.",
				Links = new Dictionary<string, string>{ {"Repo", "https://dev.azure.com/Derivco/Software/_git/Platform-Markets-Foghorn-Service" } },
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "Training",
				Tag = "N2CP-Training",
				Desc = "Training...",
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "Account Closure",
				Tag = "N2CP-SwiftAccountClosure",
				Desc = "This goal is to add new Account Closure variants for AZ & VA.",
				Links = new Dictionary<string, string>{ { "Repo", "https://dev.azure.com/Derivco/Software/_git/Platform-Swift-Responsible-Gaming-Store" } },
				QueryFolderName = "Sprint 8"
			},
			 new()
			 {
				 Title = "PTSNet - Kafka - Session Info",
				 Tag = "N2CP-Kafka-SessionInfo",
				 Desc = "This goal is to create a new Kafka data stream containing data from PTS tables tb_Session and tb_Sessioninfo",
				 QueryFolderName = "Sprint 8"
			 },
			new()
			{
				Title = "MFA",
				Tag = "N2CP-MFA",
				Desc= "This goal is to use Nevis as a third party solution to implement **multi factor authentication** in an assortment of Swift flows",
				Links = new Dictionary<string, string>
				{
					{ "MFA Design Doc", "https://confluence.derivco.co.za/display/~Simon.Strathern@derivco.co.za/MFA" },
					{ "Nevis NJ Investigation", "https://confluence.derivco.co.za/pages/viewpage.action?pageId=785051779&src=contextnavpagetreemode" }
				},
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "Reboot",
				Tag = "N2CP-Reboot",
				Desc = "This goal is to spend 30% of our time towards process improvement, fixing technical debt and approved learning.",
				QueryFolderName = "Sprint 8"
			},
			new()
			{
				Title = "Miscellaneous",
				Tag = "N2CP-Misc",
				Desc = "This goal covers any other work or training currently being done by the Chapmans Peak team.",
				QueryFolderName = "Sprint 8"
			},
		},
	};
}
