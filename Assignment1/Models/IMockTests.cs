using System.Linq;

namespace Assignment1.Models
{
    public interface IMockTests
    {
        IQueryable<Test> Tests { get; }
        Test Save(Test Tests);
        void Delete(Test Tests);
        void Dispose();
    }
}
