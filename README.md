# Xo.AzDO.Engine

![Build Status](https://github.com/stiproot/xo-azdo-cli/workflows/.NET%20Pipeline/badge.svg)
[![NuGet Version](https://img.shields.io/nuget/v/Xo.AzDO.Engine.svg)](https://www.nuget.org/packages/Xo.AzDO.Engine/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Xo.AzDO.Engine.svg)](https://www.nuget.org/packages/Xo.AzDO.Engine/)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-purple.svg)](https://dotnet.microsoft.com/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

A powerful .NET library for automating Azure DevOps operations including work item management, query creation, dashboard automation, and intelligent widget positioning.

## Table of Contents

- [Features](#features)
- [Installation](#installation)
- [Quick Start](#quick-start)
- [Configuration](#configuration)
- [Usage Examples](#usage-examples)
  - [Creating Work Items](#creating-work-items)
  - [Cloning Work Item Hierarchies](#cloning-work-item-hierarchies)
  - [Executing WIQL Queries](#executing-wiql-queries)
  - [Creating Queries and Folders](#creating-queries-and-folders)
  - [Dashboard Automation](#dashboard-automation)
- [Architecture](#architecture)
- [Dependencies](#dependencies)
- [Contributing](#contributing)
- [License](#license)
- [Support](#support)

## Features

### Work Item Management

- **Create work items** individually or bulk import from JSON
- **Clone entire work item trees** with hierarchical relationships preserved
- **Update work items** with flexible field mapping
- **Update work item hierarchies** with tag-based filtering

### Query Management

- **Build and execute WIQL** (Work Item Query Language) queries programmatically
- **Create saved queries** in Azure DevOps
- **Organize queries** in folder structures
- **Retrieve query results** with full work item details

### Dashboard & Widget Automation

- **Automated dashboard creation** with multi-initiative support
- **10+ pre-configured widget types**:
  - Markdown widgets
  - Sprint capacity & overview
  - Velocity charts
  - Burndown analytics
  - Work item charts and views
  - Query scalar widgets
  - Team members widgets
  - New work item widgets
- **Intelligent widget positioning** using `Xo.Algo.RectangleCluster` for collision-free layouts
- **Dynamic widget configuration** with JSON-based settings

### Advanced Capabilities

- **Provider-Processor pattern** for clean separation of concerns
- **Workflow orchestration** for multi-step operations
- **Built-in dependency injection** support
- **HTTP client factory** with PAT authentication
- **Async/await throughout** for optimal performance

## Installation

Install via .NET CLI:

```bash
dotnet add package Xo.AzDO.Engine
```

Or via Package Manager Console:

```powershell
Install-Package Xo.AzDO.Engine
```

Or add directly to your `.csproj`:

```xml
<PackageReference Include="Xo.AzDO.Engine" Version="3.3.3" />
```

**Requirements:**

- .NET 8.0 or later
- Azure DevOps organization with valid Personal Access Token (PAT)

## Quick Start

### 1. Configure appsettings.json

```json
{
  "secrets": {
    "pat": "YOUR_AZURE_DEVOPS_PAT_TOKEN"
  }
}
```

### 2. Set up dependency injection

```csharp
using Microsoft.Extensions.DependencyInjection;
using Xo.AzDO.Engine.Extensions;

var services = new ServiceCollection();
services.AddServices(); // Registers all providers and processors
var serviceProvider = services.BuildServiceProvider();
```

### 3. Create a work item

```csharp
using Xo.AzDO.Engine.Models;
using Xo.AzDO.Engine.Abstractions;

var createCmd = new CreateWiCmd
{
    type = "User Story",
    title = "Implement user authentication",
    description = "Add OAuth 2.0 authentication flow",
    area_path = "MyProject\\Backend",
    iteration_path = "MyProject\\Sprint 1",
    state = "New",
    tags = "authentication;security",
    story_points = "5"
};

var processor = serviceProvider.GetServiceType<IProcessor<CreateWiCmd, WiRes>>();
var result = await processor.ProcessAsync(createCmd);

Console.WriteLine($"Created work item: {result.Id}");
```

## Configuration

### Personal Access Token (PAT)

Generate a PAT in Azure DevOps with the following scopes:

- **Work Items**: Read, Write, & Manage
- **Analytics**: Read
- **Dashboards**: Read & Manage

Store your PAT in `appsettings.json`:

```json
{
  "secrets": {
    "pat": "your_personal_access_token_here"
  }
}
```

**Security Note**: Never commit `appsettings.json` with real PAT tokens to source control. Use environment variables or Azure Key Vault in production.

### Service Registration

The library uses .NET dependency injection. Register all services in your startup:

```csharp
services.AddServices(); // Extension method from Xo.AzDO.Engine.Extensions
```

This registers:

- All processors (work items, queries, dashboards)
- All providers (command factories)
- HTTP client with PAT authentication
- Logging infrastructure

## Usage Examples

### Creating Work Items

#### Single Work Item

```csharp
var cmd = new CreateWiCmd
{
    type = "Bug",
    title = "Fix null reference in login",
    description = "NullReferenceException occurs when username is empty",
    area_path = "MyProject\\Frontend",
    iteration_path = "MyProject\\Sprint 2",
    state = "Active",
    tags = "bug;critical",
    assigned_to = "user@domain.com"
};

var processor = serviceProvider.GetServiceType<IProcessor<CreateWiCmd, WiRes>>();
var result = await processor.ProcessAsync(cmd);
```

#### Bulk Import from JSON

Create an `import.json` file:

```json
[
  {
    "type": "User Story",
    "title": "As a user, I want to sign in with valid credentials",
    "area_path": "Software\\Product\\Team Alpha",
    "iteration_path": "Software\\Sprint 1",
    "state": "New",
    "tags": "authentication",
    "story_points": "3"
  },
  {
    "type": "User Story",
    "title": "As a user, I want to receive tokens after logging in",
    "area_path": "Software\\Product\\Team Alpha",
    "iteration_path": "Software\\Sprint 1",
    "state": "New",
    "tags": "authentication",
    "story_points": "2"
  }
]
```

Import using the JSON provider:

```csharp
var provider = serviceProvider.GetServiceType<IProvider<IEnumerable<CreateWiCmd>>>();
var cmds = provider.Provide();
var processor = serviceProvider.GetServiceType<IProcessor<CreateWiCmd, WiRes>>();

await Task.WhenAll(cmds.Select(cmd => processor.ProcessAsync(cmd)));
```

### Cloning Work Item Hierarchies

Clone an entire work item tree (epic with features and stories) to a new area or iteration:

```csharp
var cloneCmd = new CloneWiCmd
{
    Id = 12345, // Source work item ID
    ParentId = 0, // 0 for top-level, or specify parent ID
    AreaPath = "MyProject\\NewTeam",
    IterationPath = "MyProject\\Sprint 5",
    Tags = "cloned;migration"
};

var processor = serviceProvider.GetServiceType<IProcessor<CloneWiCmd, CloneWiRes>>();
var result = await processor.ProcessAsync(cloneCmd);

Console.WriteLine($"Cloned {result.ClonedCount} work items");
Console.WriteLine($"Root item: {result.RootCloneId}");
```

This recursively clones:

- The source work item
- All child work items (maintaining hierarchy)
- Work item relationships
- Field values (updated to new area/iteration)

### Executing WIQL Queries

#### Direct WIQL Query

```csharp
var queryCmd = new QueryByWiqlCmd
{
    Query = @"
        SELECT [System.Id], [System.Title], [System.State]
        FROM WorkItems
        WHERE [System.WorkItemType] = 'User Story'
          AND [System.State] = 'Active'
          AND [System.Tags] CONTAINS 'sprint-goal'
        ORDER BY [System.ChangedDate] DESC"
};

var processor = serviceProvider.GetServiceType<IProcessor<QueryByWiqlCmd, QueryByWiqlRes>>();
var result = await processor.ProcessAsync(queryCmd);

foreach (var workItem in result.WorkItems)
{
    Console.WriteLine($"{workItem.Id}: {workItem.Fields["System.Title"]}");
}
```

#### Programmatic Query Building

```csharp
var queryCmd = new QueryByWiqlCmd
{
    BuildWiqlCmd = new BuildWiqlCmd
    {
        WorkItemType = "Bug",
        State = "Active",
        AreaPath = "MyProject\\Backend",
        Tags = new[] { "security", "authentication" }
    }
};

var result = await processor.ProcessAsync(queryCmd);
```

### Creating Queries and Folders

#### Create a Query Folder

```csharp
var folderCmd = new CreateFolderCmd
{
    FolderName = "Sprint Metrics",
    QueryFolderPath = "Shared Queries/My Team/Sprint 10"
};

var processor = serviceProvider.GetServiceType<IProcessor<CreateFolderCmd, FolderRes>>();
var result = await processor.ProcessAsync(folderCmd);
```

#### Create a Saved Query

```csharp
var queryProvider = serviceProvider.GetServiceType<IProvider<QueryCmd>>();
var createQueryCmd = queryProvider.Provide();

var processor = serviceProvider.GetServiceType<IProcessor<QueryCmd, QueryRes>>();
await processor.ProcessAsync(createQueryCmd);
```

### Dashboard Automation

Create a complete dashboard with multiple initiatives, queries, and widgets:

```csharp
var dashboardCmd = new CreateDashboardWorkflowCmd
{
    TeamName = "Platform Team",
    DashboardName = "Sprint 10 Dashboard",
    IterationPath = "MyProject\\Sprint 10",
    QueryFolderBasePath = "Shared Queries/Dashboards",
    Initiatives = new List<Initiative>
    {
        new Initiative
        {
            Title = "User Authentication",
            Desc = "Implement OAuth 2.0 flow with role-based access",
            Tag = "auth-initiative",
            Links = new Dictionary<string, string>
            {
                { "Design Doc", "https://wiki/auth-design" },
                { "API Spec", "https://wiki/auth-api" }
            }
        },
        new Initiative
        {
            Title = "Performance Optimization",
            Desc = "Reduce API response time by 50%",
            Tag = "perf-initiative"
        }
    }
};

var processor = serviceProvider.GetServiceType<IProcessor<CreateDashboardWorkflowCmd, DashboardWorkflowRes>>();
var result = await processor.ProcessAsync(dashboardCmd);

Console.WriteLine($"Created dashboard: {result.DashboardId}");
Console.WriteLine($"Created {result.WidgetCount} widgets");
```

**Supported Widget Types:**

- `MarkdownWidget` - Rich text and documentation
- `SprintCapacityWidget` - Team capacity tracking
- `SprintOverviewWidget` - Sprint progress summary
- `VelocityWidget` - Historical velocity trends
- `AnalyticsSprintBurndownWidget` - Sprint burndown charts
- `QueryScalarWidget` - Query result counts
- `WitChartWidget` - Work item charts
- `WitViewWidget` - Work item list views
- `TeamMembersWidget` - Team roster
- `NewWorkItemWidget` - Quick work item creation

**Widget Positioning:**

Widgets are automatically positioned using the `Xo.Algo.RectangleCluster` algorithm, which:

- Prevents widget overlap
- Optimizes dashboard layout density
- Maintains visual hierarchy
- Supports responsive grid positioning

## Architecture

Xo.AzDO.Engine follows a **Provider-Processor** pattern:

### Design Pattern

```
┌─────────────┐      ┌──────────────┐      ┌─────────────┐
│  Provider   │─────>│   Command    │─────>│  Processor  │
│  (Factory)  │      │   (Data)     │      │  (Executor) │
└─────────────┘      └──────────────┘      └─────────────┘
                                                   │
                                                   v
                                          ┌─────────────────┐
                                          │ Azure DevOps    │
                                          │ REST API        │
                                          └─────────────────┘
```

### Components

**Providers** (`IProvider<TCmd>`):

- Generate command objects from configuration or JSON
- Examples: `CreateWiCmdProvider`, `CloneWiCmdProvider`, `DashboardWorkflowCmdProvider`

**Commands** (`IProcessorCmd`):

- Immutable data structures representing operations
- Located in `Models/Internal/Cmds/`
- Examples: `CreateWiCmd`, `QueryByWiqlCmd`, `CloneWiCmd`

**Processors** (`IProcessor<TCmd, TRes>`):

- Execute commands via Azure DevOps REST API
- Handle HTTP communication and error handling
- Return strongly-typed responses
- Examples: `CreateWiProcessor`, `QueryProcessor`, `DashboardWorkflowProcessorV3`

**Workflows** (`IWorkflow<TCmd>`):

- Orchestrate multi-step operations
- Handle dependencies between operations
- Examples: `PrerequisitsWorkflow`, `QueryFolderWorkflow`

### Dependency Injection

All components are registered via `ServiceCollectionExtensions.AddServices()`:

- Processors: Singleton registration for performance
- Providers: Factory pattern for command creation
- HTTP clients: Configured with PAT authentication
- Mappers: Transform commands to Azure DevOps API models

## Dependencies

Xo.AzDO.Engine relies on the following packages:

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.Extensions.DependencyInjection | 7.0.0 | Dependency injection framework |
| Microsoft.Extensions.Http | 7.0.0 | HTTP client factory |
| Microsoft.Extensions.Configuration | 7.0.0 | Configuration management |
| Microsoft.Extensions.Logging | 7.0.0 | Logging infrastructure |
| Newtonsoft.Json | 13.0.1 | JSON serialization |
| **Xo.Algo.RectangleCluster** | 1.0.1 | Intelligent widget positioning algorithm |
| **Xo.TaskTree** | 5.8.8 | Workflow orchestration and task graphs |

**Key Dependencies:**

- `Xo.Algo.RectangleCluster`: Provides collision-free rectangle packing for dashboard widget layout
- `Xo.TaskTree`: Enables complex workflow orchestration with dependency management

## Contributing

Contributions are welcome! Here's how to get started:

1. **Fork the repository**: <<https://github.com/stiproot/xo-azdo-cli>>
2. **Create a feature branch**: `git checkout -b feature/my-new-feature`
3. **Make your changes**: Follow existing code patterns and conventions
4. **Test thoroughly**: Ensure all operations work against Azure DevOps
5. **Commit your changes**: `git commit -m 'feat: add amazing feature'`
6. **Push to your fork**: `git push origin feature/my-new-feature`
7. **Open a Pull Request**: Target the `main` branch

### Commit Convention

This project uses conventional commits:

- `feat:` - New features
- `fix:` - Bug fixes
- `chore:` - Maintenance tasks
- `ci:` - CI/CD changes

### Development Setup

```bash
git clone https://github.com/stiproot/xo-azdo-cli.git
cd xo-azdo-cli
dotnet restore
dotnet build
```

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Support

### Issues and Questions

- **Bug Reports**: [Open an issue](https://github.com/stiproot/xo-azdo-cli/issues) with reproduction steps
- **Feature Requests**: [Start a discussion](https://github.com/stiproot/xo-azdo-cli/discussions) describing your use case
- **Questions**: Check existing issues or open a new discussion

### Related Projects

- [Xo.Algo.RectangleCluster](https://www.nuget.org/packages/Xo.Algo.RectangleCluster/) - Rectangle packing algorithm
- [Xo.TaskTree](https://www.nuget.org/packages/Xo.TaskTree/) - Workflow orchestration library

### Resources

- [Azure DevOps REST API Documentation](https://learn.microsoft.com/en-us/rest/api/azure/devops/)
- [WIQL Syntax Reference](https://learn.microsoft.com/en-us/azure/devops/boards/queries/wiql-syntax)
