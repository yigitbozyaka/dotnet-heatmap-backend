using backend.Model;
using MongoDB.Bson;

namespace backend.Repositories
{
    public interface IFeedbackRepository
    {
        Task<string> Create(Feedback feedback);
        
        Task<Feedback> Get(ObjectId objectId);
        
        Task<IEnumerable<Feedback>> GetAll();

        Task<IEnumerable<Feedback>> GetByTracking(string trackingNum);

        Task<bool> Delete(string objectId);
    }
}