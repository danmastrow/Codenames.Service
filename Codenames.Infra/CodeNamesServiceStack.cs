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
            ResourceGroupName = resourceGroupName
        };

        var containerRegistry = new Registry(containerRegistryName, containerRegistryArgs);
    }
}
