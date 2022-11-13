using AutoMapper;
using MongoDB.Driver;
using SampleMicroservices.Services.Catalog.Dtos;
using SampleMicroservices.Services.Catalog.Models;
using SampleMicroservices.Shared.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleMicroservices.Services.Catalog.Services
{
    public class CategoryService: ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, Settings.IDatabaseSettings databaseSettings)
        {
            var client= new MongoClient(databaseSettings.ConnectionString);
            var database=client.GetDatabase(databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<CategoryDto>>> GetAllAsync()
        {
            var categories = await _categoryCollection.Find(category => true).ToListAsync();
            return  Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories),200);
        }

        public async Task<Response<CategoryDto>> GetByIdAsync(string id)
        {
            var category= await _categoryCollection.Find<Category>(x=>x.Id==id).FirstOrDefaultAsync();
            if (category != null)
                return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
            else
                return Response<CategoryDto>.Fail("category not found", 404);

        
        }

        public async Task<Response<CategoryDto>> CreateAsync(CategoryDto categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
    }
}
