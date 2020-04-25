using Pulumi;
using Pulumi.Azure.ContainerService;
using Pulumi.Azure.Core;

public class CodeNamesServiceStack : Stack
{
    public CodeNamesServiceStack()
    {
        var config = new Config();
        var resourceGroupName = config.Require("resourceGroup");
        var containerRegistryName = config.Require("containerRegistry");
        var resourceGroup = new ResourceGroup(resourceGroupName);

        var containerRegistryArgs = new RegistryArgs()
        {
            Name = containerRegistryName,
            ResourceGroupName = resourceGroup.Name,
            Sku = "Standard",
            AdminEnabled = true
        };

        var containerRegistry = new Registry(containerRegistryName, containerRegistryArgs);

        ResourceGroupName = resourceGroup.Name;
        ContainerRegistryName = containerRegistry.Name;
        ContainerRegistryAdminUsername = containerRegistry.AdminUsername;
    }

    [Output]
    public Output<string> ContainerRegistryAdminUsername { get; set; }

    [Output]
    public Output<string> ContainerRegistryName { get; set; }


    [Output]
    public Output<string> ResourceGroupName { get; set; }

}
