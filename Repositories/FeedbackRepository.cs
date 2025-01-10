using backend.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace backend.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<Feedback> _feedbacks;
        public FeedbackRepository(IMongoClient client)
        {
            var database = client.GetDatabase("dbName");
            var collection = database.GetCollection<Feedback>(nameof(Feedback));

            _feedbacks = collection;
        }

        public async Task<string> Create(Feedback feedback)
        {
            await _feedbacks.InsertOneAsync(feedback);
            return feedback.Id!;
        }

        public async Task<bool> Delete(string objectId)
        {
            var filter = Builders<Feedback>.Filter.Eq(x => x.Id, objectId);
            var result = await _feedbacks.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        public Task<Feedback> Get(string objectId)
        {
            var filter = Builders<Feedback>.Filter.Eq(x => x.Id, objectId);
            var feedback = _feedbacks.Find(filter).FirstOrDefaultAsync();

            return feedback;
        }

        public Task<Feedback> Get(ObjectId objectId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Feedback>> GetAll()
        {
            var feedbacks = await _feedbacks.Find(_ => true).ToListAsync();

            return feedbacks;
        }

        public async Task<IEnumerable<Feedback>> GetByTracking(string trackingNum)
        {
            var filter = Builders<Feedback>.Filter.Eq(x => x.TrackingNum, trackingNum);
            var feedbacks = await _feedbacks.Find(filter).ToListAsync();

            return feedbacks;
        }
    }
}