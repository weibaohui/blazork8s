using System.Threading.Tasks;

namespace BlazorApp.Service;

public interface ITranslateService
{
    Task<string> Translate(string text);
    Task         ProcessAll();
    Task         ProcessKubeExplains();
}
