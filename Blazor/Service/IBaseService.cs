using System.Threading.Tasks;

namespace Blazor.Service
{
    public interface IBaseService
    {
        Task<T> GetFromJsonAsync<T>(string url);

        /// <summary>
        /// PostJson
        /// </summary>
        /// <param name="url"></param>
        /// <param name="t">请求参数</param>
        /// <typeparam name="T">请求参数类型</typeparam>
        /// <typeparam name="R">返回参数类型</typeparam>
        /// <returns></returns>
        Task<R> PostAsJsonAsync<T, R>(string url, T t);

        Task<string> GetStringAsync(string url);
        Task<bool>   DeleteAsync<T>(string url);
    }
}