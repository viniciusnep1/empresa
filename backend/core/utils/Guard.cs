using System;
using System.Threading.Tasks;
using core.seedwork;


namespace core.services
{
    public static class Guard
    {
#pragma warning disable CC0091 // Use static method
        public static async Task<Response> ExecuteAsync(Func<Task<Response>> func)
#pragma warning restore CC0091 // Use static method
        {
            try
            {
                return await func?.Invoke();
            }
            catch (Exception ex)
            {
                return new Response(ex);
            }
        }
    }
}
