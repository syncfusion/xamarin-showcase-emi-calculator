using System.IO;
using System.Threading.Tasks;

namespace LoanCalculator
{
    public interface ISave
    {
        void Save(string filename, string contentType, MemoryStream stream);
    
        Task SaveWindows(string filename, string contentType, MemoryStream stream);
    }
}