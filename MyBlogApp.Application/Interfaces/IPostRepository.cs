﻿using MyBlogApp.Domain.Entities;

namespace MyBlogApp.Application.Interfaces;

public interface IPostRepository
{
    Task<Post> GetByIdAsync(int id);
    Task<IEnumerable<Post>> GetAllAsync();
    Task AddAsync(Post post);
    Task UpdateAsync(Post post);
    Task DeleteAsync(int id);
}