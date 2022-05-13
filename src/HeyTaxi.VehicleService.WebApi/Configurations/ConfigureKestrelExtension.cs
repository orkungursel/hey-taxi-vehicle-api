using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace HeyTaxi.VehicleService.WebApi.Configurations;

public static class ConfigureKestrelExtension {
    public static ConfigureWebHostBuilder ConfigureKestrel(this ConfigureWebHostBuilder builder) {
        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        builder.ConfigureKestrel(options =>
        {
            options.ListenAnyIP(8080, o => o.Protocols = HttpProtocols.Http1AndHttp2);
            options.ListenAnyIP(50052, o => o.Protocols = HttpProtocols.Http2);
        });

        return builder;
    }
}
