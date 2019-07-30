using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrossPlatformLibrary.Forms.Validation
{
    public interface IValidation
    {
        string[] PropertyNames { get; }

        Task<Dictionary<string, List<string>>> GetErrors();
    }
}