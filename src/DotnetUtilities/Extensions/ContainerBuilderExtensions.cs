using System.Reflection;
using Autofac;
using Autofac.Builder;
using Autofac.Features.Scanning;

namespace Oelze.Garrett.Utilities.Extensions;

public static class ContainerBuilderExtensions
{
    public static void RegisterByConvention(
        this ContainerBuilder builder,
        Assembly assembly,
        string suffix,
        Action<IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle>>? registerAction = null)
    {
        registerAction ??= DefaultRegisterAction;

        IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> regBuilder =
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => !t.IsInterface && !t.IsAbstract && t.Name.EndsWith(suffix));

        registerAction(regBuilder);
    }

    static void DefaultRegisterAction(IRegistrationBuilder<object, ScanningActivatorData, DynamicRegistrationStyle> registrationBuilder) =>
        registrationBuilder.AsImplementedInterfaces();
}
