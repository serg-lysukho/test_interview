using System.Threading.Tasks;

namespace InterviewApp.DAL.UnitOfWork
{
    public interface IInterviewAppUnitOfWork
    {
        Task CommitAsync();
        void Commit();
        void Dispose();
    }
}