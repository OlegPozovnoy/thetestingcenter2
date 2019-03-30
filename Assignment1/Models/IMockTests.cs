using System.Linq;

namespace Assignment1.Models
{
    public interface IMockTests
    {
        IQueryable<Test> Categories { get; }
        Test Save(Test category);
        void Delete(Test category);
        void Dispose();
    }
}
