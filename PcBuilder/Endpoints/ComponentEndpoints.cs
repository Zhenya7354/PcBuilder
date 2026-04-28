using PcBuilder.Services;

namespace PcBuilder.Endpoints
{
    public static class ComponentEndpoints
    {
        public static WebApplication MapComponentEndpoints(this WebApplication webApplication)
        {
            var group = webApplication.MapGroup("/components");

            group.MapGet("/cpu", async (CpuService cpuService) =>
            {
                var cpus = await cpuService.GetAllCpuAsync();
                if (cpus is null) return Results.NotFound("Cpus not found");
                return Results.Ok(cpus);
            });

            return webApplication;
        }
    }
}
