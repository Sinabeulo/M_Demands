using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class SeedData
    {
        public static void Initialize(TodoContext context)
        {
            if (!context.TodoItems.Any())
            {
                context.TodoItems.AddRange(
                    new TodoItem
                    {
                        Name = "Work"
                    },
                    new TodoItem
                    {
                        Name = "Sleep"
                    });

                context.SaveChanges();
            }
        }
    }
}
