using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace InfinityStoreAdmin.Api.Shared.FrameworkCustomizing.OperationGroup;

/// <summary>
/// This class is used to customize the controller name in swagger.
/// Use case is to group the controllers by common functionality.
/// </summary>
public class OperationGroupConvention : IControllerModelConvention
{
    public void Apply(ControllerModel controller)
    {
        var operationGroupAttribute = 
            controller.Attributes.OfType<OperationGroupAttribute>().FirstOrDefault();

        if (operationGroupAttribute != null)
        {
            controller.ControllerName = operationGroupAttribute.Name;
        }
    }
}