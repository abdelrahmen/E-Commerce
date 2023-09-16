using E_Commerce.Data;
using E_Commerce.Models;
using E_Commerce.Repositories.Interfaces;

namespace E_Commerce.Repositories
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly AppDbContext context;

		public CategoryRepository(AppDbContext context)
		{
			this.context = context;
		}
		public Category AddCategory(Category category)
		{
			context.Category.Add(category);
			context.SaveChanges();
			return context.Category.FirstOrDefault(c => c.Id == category.Id);
		}

		public void Delete(int id)
		{
			var category = GetById(id);
			if (category != null)
			{
				context.Category.Remove(category);
				context.SaveChanges();
			}
		}

		public List<Category> GetAll()
		{
			return context.Category.ToList();
		}

		public Category? GetById(int id)
		{
			return context.Category.FirstOrDefault(c => c.Id == id);
		}

		public Category GetByName(string name)
		{
			throw new NotImplementedException();
		}

		public Category? Edit(Category category)
		{
			var catigoryToEdit = context.Category.FirstOrDefault(c => c.Id == category.Id);
			if (catigoryToEdit != null)
			{
				catigoryToEdit.Name = category.Name;
				catigoryToEdit.Description = category.Description;
				context.SaveChanges();
			}
			return catigoryToEdit;
		}
	}
}
