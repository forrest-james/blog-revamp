using Domain.Entities;
using Domain.Enums;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Persistence.Repositories
{
    public class PostRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public PostRepository(ApplicationDbContext dbContext) => _dbContext = dbContext;

        // Retrieve
        public IEnumerable<Post> GetAll() => _dbContext.Posts.AsNoTracking().Include("Comments").Include("Reactions").ToList();
        public IEnumerable<Post> GetByType(PostType type) => _dbContext.Posts.AsNoTracking().Where(post => post.Type == type).Include("Comments").Include("Reactions").ToList();
        public IEnumerable<Post> GetByDate(int month, int year) => _dbContext.Posts.AsNoTracking().Where(post => post.CreatedDate.Month == month && post.CreatedDate.Year == year).Include("Comments").Include("Reactions").ToList();
        public Post GetById(int id) => _dbContext.Posts.AsNoTracking().Include("Comments").Include("Reactions").FirstOrDefault(post => post.Id == id);
        public Post GetByTitle(string title) => _dbContext.Posts.AsNoTracking().Include("Comments").Include("Reactions").FirstOrDefault(post => post.EncodedTitle.Equals(title));

        // Save
        public async Task<bool> SaveChangesAsync() => await _dbContext.SaveChangesAsync() > 0;

        // Modify
        public void Remove(int id) => Remove(GetById(id));
        private void Remove(Post post) => _dbContext.Posts.Remove(post);
        public void Upsert(Post post)
        {
            Post tempPost;
            if (post.Id == 0)
            {
                tempPost = new Post()
                {
                    Title = post.Title,
                    Body = post.Body,
                    Type = post.Type,
                    EncodedTitle = ValidateTitle(post.Title.Encode()),
                    CreatedBy = post.CreatedBy,
                    CreatedDate = DateTime.UtcNow
                };
                _dbContext.Posts.Add(tempPost);
            }
            else
            {
                post.EncodedTitle = ValidateTitle(post.Title.Encode());
                post.LastModifiedDate = DateTime.UtcNow;
                _dbContext.Posts.Update(post);
            }
        }

        private string ValidateTitle(string encodedTitle)
        {
            int matches = _dbContext.Posts.Where(post => post.EncodedTitle == encodedTitle).Count();
            if (matches > 0)
                return encodedTitle += "-" + matches++;
            else
                return encodedTitle;
        }
    }
}