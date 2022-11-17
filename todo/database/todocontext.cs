using System;
using Microsoft.EntityFrameworkCore;
using todo.Models;

namespace todo.database
{
	public class todocontext : DbContext
	{
		public todocontext(DbContextOptions<todocontext> options)
			:base(options)
		{
		}

		public DbSet<todolist> todo { get; set; }
	}
}

