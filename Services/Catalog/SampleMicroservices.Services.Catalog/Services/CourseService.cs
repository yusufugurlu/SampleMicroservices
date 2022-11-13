﻿using AutoMapper;
using MongoDB.Driver;
using SampleMicroservices.Services.Catalog.Dtos;
using SampleMicroservices.Services.Catalog.Models;
using SampleMicroservices.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Catalog.Services
{
    public class CourseService: ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CourseService(IMapper mapper, Settings.IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);
            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(course => true).ToListAsync();
            if (courses.Any())
                foreach (var course in courses)
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            else
                courses = new List<Course>();

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<List<CourseDto>>> GetAllByUserIdAsync(string userId)
        {
            var courses = await _courseCollection.Find(course => course.UserId == userId).ToListAsync();
            if (courses.Any())
                foreach (var course in courses)
                    course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
            else
                courses = new List<Course>();

            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> GetByIdAsync(string id)
        {
            var course = await _courseCollection.Find<Course>(x => x.Id == id).FirstOrDefaultAsync();
            if (course != null)
            {
                course.Category = await _categoryCollection.Find<Category>(x => x.Id == course.CategoryId).FirstOrDefaultAsync();
                return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
            }
            else
                return Response<CourseDto>.Fail("category not found", 404);
        }

        public async Task<Response<CourseDto>> CreateAsync(CourseCreateDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            course.CreatedTime = DateTime.Now;
            await _courseCollection.InsertOneAsync(course);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course), 200);
        }

        public async Task<Response<NoContent>> UpdateAsync(CourseUpdateDto courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var result = await _courseCollection.FindOneAndReplaceAsync(c => c.Id == course.Id, course);
            if (result == null)
                return Response<NoContent>.Fail("Fail not found", 404);

            return Response<NoContent>.Success(200);
        }

        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var result = await _courseCollection.DeleteOneAsync(c => c.Id == id);
            if (result.DeletedCount < 1)
                return Response<NoContent>.Fail("Fail not found", 404);

            return Response<NoContent>.Success(200);
        }
    }
}
