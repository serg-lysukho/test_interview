using System.Threading.Tasks;

namespace InterviewApp.DAL.UnitOfWork
{
    public class InterviewAppUnitOfWork : IInterviewAppUnitOfWork
    {
        private readonly InterviewAppDbContext _context;

        public InterviewAppUnitOfWork(InterviewAppDbContext context)
        {
            _context = context;
        }

        public Task CommitAsync()
        {
            return _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
