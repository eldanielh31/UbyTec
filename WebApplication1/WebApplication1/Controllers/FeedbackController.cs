using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using MongoDB.Bson;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/feedback")]
    public class FeedbackController : ControllerBase
    {
        private readonly ILogger<FeedbackController> _logger;

        private MongoClient client = new MongoClient("mongodb+srv://danigames:dani31@cluster0.mzjrx.mongodb.net/?retryWrites=true&w=majority");

        private IMongoCollection<Feedback> feedbackCollection;

        public FeedbackController(ILogger<FeedbackController> logger)
        {
            this.feedbackCollection = this.client.GetDatabase("UbyTEC").GetCollection<Feedback>("feedback");
            _logger = logger;
        }

        [HttpPost]
        public async Task<ObjectId> Post(Feedback obj){
            await this.feedbackCollection.InsertOneAsync(obj);
            return obj.id;
        }


        [HttpGet]
        public async Task<List<Feedback>> Get()
        {
            var allFeedback = await this.feedbackCollection.Find(Builders<Feedback>.Filter.Empty).ToListAsync();
            return allFeedback;
        }

        [HttpGet("{id_producto}")]
        public async Task<List<Feedback>> GetbyIdProducto(int id_producto)
        {
            var allFeedback = await this.feedbackCollection.Find(Builders<Feedback>.Filter.Eq("id_producto", id_producto)).ToListAsync();
            return allFeedback;
        }

        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete(string id)
        {
            var deleteFilter = Builders<Feedback>.Filter.Eq(feed=> feed.id, ObjectId.Parse(id));
            var result = await this.feedbackCollection.DeleteOneAsync(deleteFilter);
            return result;
        }
    }
}